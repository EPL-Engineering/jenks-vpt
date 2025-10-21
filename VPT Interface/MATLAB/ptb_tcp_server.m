function ptb_tcp_server(ipEndPoint)

if nargin == 0
   ipEndPoint = '169.254.245.221:4926';
end

app.settings = [];
app.noiseLevel = 0;
app.noiseRunning = false;
app.imageFolder = fullfile(getenv('PUBLIC'), 'Documents', 'Jenks', 'VPT', 'Images');

% Starting logging
folder = fullfile(getenv('PUBLIC'), 'Documents', 'Jenks', 'VPT', 'Logs');
logger = epl.Logger(folder, 'PTBInterface');

% Start TCP server
parts = split(ipEndPoint, ':');
address = parts{1};
port = str2double(parts{2});

try
   tcpServer = tcpserver(address, port);
catch ex
   logger.LogError(sprintf('error creating TCP server: %s', ex.message));
end

logger.LogInfo(sprintf('Started TCP listener on %s', ipEndPoint));

while true
   logger.LogInfo('waiting for connection');
   while ~tcpServer.Connected
      pause(0.1);
   end

   n = tcpServer.read(1, 'int32');
   command = tcpServer.read(n, 'char');

   icolon = find(command == ':', 1);
   if ~isempty(icolon)
      message = command(1:icolon-1);
      data = command(icolon+1:end);
   else
      message = command;
      data = '';
   end

   logger.LogInfo(sprintf('Received message ''%s''', message));

   try
      result = 1;
      switch message
         case 'Open'
            app.settings = jsondecode(data);
            app = OpenPTB(app);

         case 'Close'
            ClosePTB();

         case 'InitializeMotion'
            settings = jsondecode(data);
            app = InitializeMotion(app, settings.frequency, settings.pre, settings.post);

         case 'Noise'
            if isequal(data, 'on')
               app = StartAudio(app);
            else
               app = StopAudio(app);
            end

         case 'Volume'
            app.noiseLevel = str2double(data);
            if app.noiseRunning
               app = StopAudio(app);
               app = StartAudio(app);
            end
            % SoundVolume(str2double(data));

         case 'DoRotation'
            settings = jsondecode(data);
            app = DoRotation(app, settings.delta, settings.direction);

         case 'DoLeftRight'
            settings = jsondecode(data);
            app = DoLeftRight(app, settings.delta, settings.direction);

         case 'DoUpDown'
            settings = jsondecode(data);
            app = DoUpDown(app, settings.delta, settings.direction);

         case 'DoZoom'
            settings = jsondecode(data);
            app = DoZoom(app, settings.delta, settings.direction);

         case 'Quit'
            ClosePTB();
            logger.Flush();
            break;
      end
   catch ex
      logger.LogError(ex.message);
      result = -1;
   end

   % send response
   tcpServer.write(result, 'int32');
   tcpServer.flush();

   while tcpServer.Connected
      pause(0.1);
   end
end

clear tcpServer;

end

%--------------------------------------------------------------------------
function app = OpenPTB(app)

PsychDefaultSetup(2);
Screen('Preference', 'SkipSyncTests', 1);
Screen('Preference', 'VisualDebugLevel', 3);
app.window = Screen('OpenWindow', app.settings.monitor, app.settings.backgroundColor);

Screen('BlendFunction', app.window, 'GL_SRC_ALPHA', 'GL_ONE_MINUS_SRC_ALPHA');
img = imread(fullfile(app.imageFolder, app.settings.imageName));
imgSize = size(img);
imageWidth = imgSize(2);
imageHeight = imgSize(1);
app.imageTexture = Screen('MakeTexture', app.window, img);

Screen('DrawTexture', app.window, app.imageTexture);
Screen('Flip', app.window);

app.frameRate = FrameRate(app.window);

resol = Screen('Resolution', app.settings.monitor);
xcenter = round(resol.width / 2);
ycenter = round(resol.height / 2);

left = xcenter - round(imageWidth / 2);
top = ycenter - round(imageHeight / 2);

app.projection.defaultDestRect = [left top left + imageWidth top + imageHeight];
app.projection.pixels_per_cm = resol.width / app.settings.width;

app.projection.imageWidth_cm = imageWidth / app.projection.pixels_per_cm;
app.projection.theta = atan(app.projection.imageWidth_cm / app.settings.width) * 180 / pi;
app.projection.aspectRatio = imageHeight / imageWidth;

end

%--------------------------------------------------------------------------
function ClosePTB()

Screen('CloseAll');

end

%--------------------------------------------------------------------------
function app = InitializeMotion(app, freq, pre, post)

T = 1 / freq;
dt = 1 / app.frameRate;

npre = round(pre * app.frameRate);
npost = round(post * app.frameRate);

t = 0:dt:T;

y = (t - sin(2*pi*freq*t)/(2*pi*freq));
app.position_vs_time = [zeros(1, npre) y y(end)*ones(1, npost)];
app.noiseDur = length(app.position_vs_time) / app.frameRate;

app.lastPosition = 0;

end

%--------------------------------------------------------------------------
function app = DoRotation(app, delta, direction)

x = direction * delta * app.position_vs_time + app.lastPosition;

whitenoise = app.noiseLevel * normrnd(0, 0.2, round(44100 * app.noiseDur), 1);
player = audioplayer(whitenoise, 44100);
play(player);

for k = 1:length(x)
   Screen('DrawTexture', app.window, app.imageTexture, [], [], x(k));
   Screen('Flip', app.window);
end

app.lastPosition = x(end);

end

%--------------------------------------------------------------------
function app = DoLeftRight(app, delta_position, direction)

x = direction * delta_position * app.position_vs_time + app.lastPosition;

whitenoise = app.noiseLevel * normrnd(0, 0.2, round(44100 * app.noiseDur), 1);
player = audioplayer(whitenoise, 44100);
play(player);

for k = 1:length(x)
   Screen('DrawTexture', app.window, app.imageTexture, [], app.projection.defaultDestRect + x(k) * app.projection.pixels_per_cm * [1 0 1 0]);
   Screen('Flip', app.window);
end
app.lastPosition = x(end);
end

%--------------------------------------------------------------------
function app = DoUpDown(app, delta_position, direction)

y = -direction * delta_position * app.position_vs_time + app.lastPosition;

whitenoise = app.noiseLevel * normrnd(0, 0.2, round(44100 * app.noiseDur), 1);
player = audioplayer(whitenoise, 44100);
play(player);

for k = 1:length(y)
   Screen('DrawTexture', app.window, app.imageTexture, [], app.projection.defaultDestRect + y(k) * app.projection.pixels_per_cm * [0 1 0 1]);
   Screen('Flip', app.window);
end
app.lastPosition = y(end);
end

%--------------------------------------------------------------------
function app = DoZoom(app, delta_position, direction)

delta_x_cm = app.data.settings.screenDistance * tan((app.projection.theta + direction * delta_position) * pi/180) - app.projection.imageWidth_cm;
delta_x_pixels = delta_x_cm * app.projection.pixels_per_cm;

x = delta_x_pixels * app.position_vs_time + app.lastPosition;

whitenoise = app.noiseLevel * normrnd(0, 0.2, round(44100 * app.noiseDur), 1);
player = audioplayer(whitenoise, 44100);
play(player);

for k = 1:length(x)
   destRect = app.projection.defaultDestRect + x(k) * [-1 -app.projection.aspectRatio 1 app.projection.aspectRatio];

   Screen('DrawTexture', app.window, app.imageTexture, [], destRect);
   Screen('Flip', app.window);
end

app.lastPosition = x(end);
end

%--------------------------------------------------------------------
function app = StartAudio(app)
Fs = 44100;
T = 30;
nsamples = round(Fs * T);
whitenoise = app.noiseLevel * normrnd(0, 0.2, nsamples, 1);
app.audioPlayer = audioplayer(whitenoise, Fs);
app.audioPlayer.StopFcn = @AudioPlayerStopFcn;
play(app.audioPlayer);
app.noiseRunning = true;
end

%--------------------------------------------------------------------
function app = StopAudio(app)
app.audioPlayer.StopFcn = [];
stop(app.audioPlayer);
app.noiseRunning = false;
end

%--------------------------------------------------------------------
function AudioPlayerStopFcn(player)
play(player);
end

%--------------------------------------------------------------------------
% END
%--------------------------------------------------------------------------
