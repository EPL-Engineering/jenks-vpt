function result = SendMessage(address, port, message, data)

result = -1;

try
   t = tcpclient(address, port, "Timeout", 5, "ConnectTimeout", 10);

   command = message;
   if nargin > 3
      command = [command ':' data];
   end

   nbytes = int32(length(command));
   t.write([typecast(nbytes, 'uint8') uint8(command)]);
   t.flush();

   result = t.read(1, 'int32');

   clear t;
catch ex
   disp(ex.message);
end