[address, port] = Discover('VPT.INTERFACE');
t = tcpclient(address, port, "Timeout", 1, "ConnectTimeout", 10);

data = 'Ping';
nbytes = int32(length(data));
t.write([typecast(nbytes, 'uint8') uint8(data)]);
t.flush();

result = t.read(1, 'int32');

clear t;