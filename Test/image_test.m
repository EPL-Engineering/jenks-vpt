% slightly modified boilerplate -- non-fullscreen by default,
% and set the background color (no need for all the FillRects)
PsychDefaultSetup(2); 
Screen('Preference', 'SkipSyncTests', 0);
screenNum = max(Screen('Screens')) - 1; % set screen 
Screen('Preference', 'VisualDebugLevel', 3);
w = Screen('OpenWindow', screenNum);

Screen('BlendFunction', w, 'GL_SRC_ALPHA', 'GL_ONE_MINUS_SRC_ALPHA');

% from http://pngimg.com/upload/cat_PNG100.png
[img, ~, alpha] = imread('test_pattern.png');
size(img)

texture1 = Screen('MakeTexture', w, img);

Screen('DrawTexture', w, texture1);
Screen('Flip', w);

pause(1);

Screen('DrawTexture', w, texture1, [], [], 45);
Screen('Flip', w);

pause(5);
Screen('CloseAll');