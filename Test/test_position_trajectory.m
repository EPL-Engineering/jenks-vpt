delta = 180;
Vmax = 20;
Amax = 100;

% T = 2 * delta / Vmax;
T = sqrt(2*pi*delta / Amax);

f = 1 / T;
Vmax = Amax / (pi * f);

Fs = 1000;
dt = 1 / Fs;

t = 0:dt:T;

v = Vmax / 2 * (1 - cos(2*pi*f*t));
theta = cumsum(v) * dt;

a = diff(v) / dt;

figure
subplot(311);
plot(t, theta);
ylabel('Position (degrees)');

subplot(312);
plot(t, v);
ylabel('Velocity (deg/s)');

subplot(313);
plot(t(1:end-1), a);
ylabel('Acceleration (deg/s^2)');

