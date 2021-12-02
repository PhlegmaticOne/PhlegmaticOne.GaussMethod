//var client = new PhlegmaticOne.GaussMethod.Clients.TCP.TcpClient("127.0.0.1", 8080);
//await client.ConnectAsync();
//await client.SendMessage("Hello");

//using System.Net.Sockets;
//using System.Text;

//using var client = new TcpClient();

//var hostname = "webcode.me";
//client.Connect(hostname, 80);

//using NetworkStream networkStream = client.GetStream();
//networkStream.ReadTimeout = 2000;

//using var writer = new StreamWriter(networkStream);

//var message = "HEAD / HTTP/1.1\r\nHost: webcode.me\r\nUser-Agent: C# program\r\n"
//              + "Connection: close\r\nAccept: text/html\r\n\r\n";

//Console.WriteLine(message);

//using var reader = new StreamReader(networkStream, Encoding.UTF8);

//byte[] bytes = Encoding.UTF8.GetBytes(message);
//networkStream.Write(bytes, 0, bytes.Length);

//Console.WriteLine(reader.ReadToEnd());


using PhlegmaticOne.GaussMethod.Servers.Extensions;

var from = "Action: ";
var to = "\n";
var source = "Controller: Gauss\nAction: Solve\nData: adsasdasda";
var firstIndex = source.IndexOf(from);
var secondIndex = source.IndexOf(to, firstIndex);
var length = secondIndex - firstIndex;
var s = length < 0 ? source.Substring(firstIndex + from.Length) :
    source.Substring(firstIndex + from.Length, length - from.Length);


Console.WriteLine(s);
