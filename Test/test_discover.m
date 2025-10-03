uSender = udpport;
uSender.Timeout = 1;
write(uSender, 'VPT.INTERFACE', "uint8", "234.5.6.7", 10000);

startTime = tic;
while (uSender.NumBytesAvailable == 0 && toc(startTime) < 2)
end
bytes = read(uSender, uSender.NumBytesAvailable, 'char');

