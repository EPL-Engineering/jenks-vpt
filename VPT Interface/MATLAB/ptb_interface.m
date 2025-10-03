function ptb_interface()

if ~isdeployed
    javaaddpath('./TCPThread_KeepOpen.jar');
end

ipAddress = 'localhost';

fprintf('Listening on %s...\n', ipAddress);
tcp_thread = TCPThread_KeepOpen(ipAddress, 4926);
start(tcp_thread);

pause(1);

try
   while (true)
%       java.lang.Thread.sleep(2);
      pause(0.002);
      msg = char(tcp_thread.getMessage());
      
      if ~isempty(msg)

         if length(msg) > 4 && strcmpi(msg(1:4), 'init')
            [dist, screenID, diodeSize, diodeLocation, background] = parse_init_options(msg(6:end));
            ptb = ptb_InitializeGratingDisplay(dist, screenID, diodeSize, diodeLocation, background);
            
         elseif length(msg) > 6 && strcmpi(msg(1:6), 'buffer')
            grating = InitializeGrating(msg(8:end), ptb);
            
         elseif strcmpi(msg, 'draw')
            log = Display(grating, ptb, tcp_thread, log);
            grating.type = 'OFF';
            
         elseif strcmpi(msg, 'release')
            Screen('CloseAll');
            
         elseif strcmp(msg, 'Quit')
            Screen('CloseAll');
            break;
         end
         disp(msg);
         
      end
   end
   
catch ex
   Screen('CloseAll');
   tcp_thread.setError();
   rethrow(ex);
end
%--------------------------------------------------------------------------
function log = Display(grating, ptb, tcp, log, tmsg)  

if nargin < 5, tmsg = NaN; end

try
   while true
      if strcmpi(grating.type, 'off')
         vbl = Screen('Flip', ptb.win);
         log = LogOffTime(log);
         return;
      end

      phase = grating.phase_cycles;
      
      numFrames = round(1e-3 * grating.duration_ms / ptb.ifi);
      if numFrames == 0, numFrames = Inf; end
      
      newGrating = false;
      
      frameNum = 0;

      log = LogDrawTime(log);
      
      while frameNum < numFrames
         % Draw the grating, centered on the screen, with given rotation 'angle'
         Screen('DrawTexture', ptb.win, ptb.gratingtex, [], [], 360-grating.orientation_degrees, ...
            [], [], [], [], ptb.rotateMode, ...
            [phase, grating.cyclesPerPixel, grating.contrast, grating.typeInt ...
            grating.background 0 0 0] ...
            );
         
         if ptb.useDiodeRect
            Screen('FillRect', ptb.win, 255*[1 1 1 0], ptb.diodeRect);
         end
         
         phase = phase + grating.phaseIncrement;
         
         % Show it at next retrace:
         if frameNum == 0
            vbl = Screen('Flip', ptb.win);
            log = LogOnTime(log);
         else
            vbl = Screen('Flip', ptb.win, vbl + 0.5 * ptb.ifi);
         end
         frameNum = frameNum + 1;
         
         msg = char(tcp.getMessage());
         
         if length(msg) > 6 && strcmpi(msg(1:6), 'buffer')
            grating = InitializeGrating(msg(8:end), ptb);
            newGrating = true;
         end
         
         if isequal(msg, 'Draw') || isequal(msg, 'Clear'), break; end
      end
      
      if ~newGrating, break; end
   end
   
   if numFrames > 1
     vbl = Screen('Flip', ptb.win, vbl + 0.5 * ptb.ifi);
     log = LogOffTime(log);
   end
   
catch ex
   Screen('CloseAll');
   tcp.setError();
   rethrow(ex);
end

%--------------------------------------------------------------------------
function [D, ID, SZ, LOC, BG] = parse_init_options(init_string)

A = sscanf(init_string, '%f:%d:%f:%d:%s%s');
D = A(1);
ID = A(2);
BG = A(3);
SZ = A(4);
LOC = lower(char(A(5:end)'));

%--------------------------------------------------------------------------
% END OF PTB_INTERFACE.M
%--------------------------------------------------------------------------
