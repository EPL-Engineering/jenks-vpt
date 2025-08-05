d = 100;
w = 1:25;

delta_h = NaN(size(w));

for k = 1:length(w)
   theta1 = atan(w(k) / d) * 180 / pi;
   H1 = d * tan(theta1 * pi/180);

   delta_theta = 2;
   theta2 = theta1 + delta_theta;

   delta_h(k) = d * tan(theta2 * pi/180) - H1;
end

figure
plot(w, delta_h, 'o-');