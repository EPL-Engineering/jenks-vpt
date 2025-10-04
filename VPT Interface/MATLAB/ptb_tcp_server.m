function ptb_tcp_server

app.settings = [];

tcpServer = tcpserver("127.0.0.1", 4926);

while true
   fprintf('%s Received message ''%s''\n', datestr(now, 'YYYY-mm-dd HH:MM:SS.fff'), 'wating for connection');
   while ~tcpServer.Connected
      pause(0.1);
   end

   n = tcpServer.read(1, 'int32');  
   command = tcpServer.read(n, 'char');

   tcpServer.write(1, 'int32');
   tcpServer.flush();

   icolon = find(command == ':', 1);
   if ~isempty(icolon)
      message = command(1:icolon-1);
      data = command(icolon+1:end);
   else
      message = command;
      data = '';
   end

   fprintf('%s Received message ''%s''\n', datestr(now, 'YYYY-mm-dd HH:MM:SS.fff'), message);

   switch message
      case 'Open'
         app.settings = jsondecode(data);
         app = OpenPTB(app);

      case 'Close'
         ClosePTB();

      case 'InitializeMotion'
         settings = jsondecode(data);
         app = InitializeMotion(app, settings.frequency);

      case 'DoRotation'
         settings = jsondecode(data);
         app = DoRotation(app, settings.delta, settings.direction);

      case 'Quit'
         break;
   end

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
img = imread(fullfile(app.settings.imageFolder, app.settings.imageName));
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
function app = InitializeMotion(app, freq)

T = 1 / freq;
dt = 1 / app.frameRate;

t = 0:dt:T;

app.position_vs_time = freq * (t - sin(2*pi*freq*t)/(2*pi*freq));

app.lastPosition = 0;

end

%--------------------------------------------------------------------------
function app = DoRotation(app, delta, direction)

x = direction * delta * app.position_vs_time + app.lastPosition;

for k = 1:length(x)
   Screen('DrawTexture', app.window, app.imageTexture, [], [], x(k));
   Screen('Flip', app.window);
end

app.lastPosition = x(end);

end

%--------------------------------------------------------------------------
% END
%--------------------------------------------------------------------------
