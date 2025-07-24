dt = 1 / 100;

freq = 1;
delta_x = 2;

x0 = 0;

w = 2 * pi * freq;
A = delta_x * w^2 / (2*pi^2 + 1);
C = x0 - A / w^2;

T = 1 / freq;

t = 0:dt:T;
x = A * (t.^2/2 + cos(w.*t) / w^2) + C;

figure(1);
clf
hold on;
plot(t, x, 'LineWidth', 2);
plot(t(1:end-1), diff(x)/dt, 'LineWidth', 2);
plot(t(1:end-2), diff(diff(x))/dt^2, 'LineWidth', 2);
xlabel('Time (s)');
yline(delta_x);
legend({'Position', 'Velocity', 'Acceleration'});
