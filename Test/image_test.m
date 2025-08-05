% slightly modified boilerplate -- non-fullscreen by default,
% and set the background color (no need for all the FillRects)
PsychDefaultSetup(2); 
Screen('Preference', 'SkipSyncTests', 1);
screenNum = 1; % set screen 
Screen('Preference', 'VisualDebugLevel', 3);
w = Screen('OpenWindow', screenNum);

Screen('BlendFunction', w, 'GL_SRC_ALPHA', 'GL_ONE_MINUS_SRC_ALPHA');

% from http://pngimg.com/upload/cat_PNG100.png
[img, ~, alpha] = imread('test_pattern.png');

texture1 = Screen('MakeTexture', w, img);

Screen('DrawTexture', w, texture1);
Screen('Flip', w);

fr = FrameRate(w);

imgSize = size(img);
imageWidth = imgSize(2);
imageHeight = imgSize(1);

resol = Screen('Resolution', screenNum);
xcenter = round(resol.width / 2);
ycenter = round(resol.height / 2);

left = xcenter - round(imageWidth / 2);
top = ycenter - round(imageHeight / 2);

% left = left + 1000;

pause(1);

% Screen('DrawTexture', w, texture1, [], [left top left + imageWidth top + imageHeight]);
Screen('DrawTexture', w, texture1, [], [left top left + imageWidth top + imageHeight] + 100*[-1 -1 1 1]);
Screen('Flip', w);

pause(5);

fprintf('Frame rate = %f fps\n', fr);
Screen('CloseAll');