Fs = 100000;
dt = 1 / Fs;

freq = 1;
delta_x = 2;

x0 = 0;

T = 1 / freq;

t = 0:dt:T;
x = delta_x * freq * (t - sin(2*pi*freq*t)/(2*pi*freq));

figure(1);
clf
hold on;
plot(t, x, 'LineWidth', 2);
plot(t(1:end-1), diff(x)/dt, 'LineWidth', 2);
plot(t(1:end-2), diff(diff(x))/dt^2, 'LineWidth', 2);
xlabel('Time (s)');
yline(delta_x);
legend({'Position', 'Velocity', 'Acceleration'});
