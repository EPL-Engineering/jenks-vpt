function test_tcp_server

tcpServer = tcpserver("127.0.0.1", 4926);

while true
   while ~tcpServer.Connected
      pause(0.1);
   end

   n = tcpServer.read(1, 'int32');
   command = tcpServer.read(n, 'char');

   tcpServer.write(1, 'int32');

   icolon = find(command == ':', 1);
   if ~isempty(icolon)
      message = command(1:icolon-1);
      data = command(icolon+1:end);
   else
      message = command;
      data = '';
   end

   fprintf('Received message ''%s''\n', message);

   switch message
      case 'Open'
         settings = jsondecode(data);
         disp(settings);
         
      case 'Quit'
         break;
   end
end

clear tcpServer;
