function screenNumber = get_app_screen_number(UIFig)

% Get the local graphics environment
ge = java.awt.GraphicsEnvironment.getLocalGraphicsEnvironment();

% Get all available screen devices
screenDevices = ge.getScreenDevices();

appFigurePosition = UIFig.Position; % Get the position of your app's UI figure

% Iterate through screen devices to find which screen the app is on
screenNumber = -1; % Initialize with a value indicating not found
for i = 1:numel(screenDevices)
    % Get the bounds of the current screen device
    currentScreenBounds = screenDevices(i).getDefaultConfiguration().getBounds();

    % Check if the app's figure position overlaps with the current screen's bounds
    % This is a simplified check; a more robust check would consider full containment
    if (appFigurePosition(1) >= currentScreenBounds.getX() && ...
        appFigurePosition(1) < (currentScreenBounds.getX() + currentScreenBounds.getWidth()) && ...
        appFigurePosition(2) >= currentScreenBounds.getY() && ...
        appFigurePosition(2) < (currentScreenBounds.getY() + currentScreenBounds.getHeight()))
        screenNumber = i;
        break; % Found the screen, exit loop
    end
end
