using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Neptunium.Game.Bots;
using Neptunium.Messages;
using Neptunium.Messages.Outgoing;
using Neptunium.Util;
using Neptunium.Util.XmlUtils;
using Serilog;

namespace Neptunium.Network
{
    public class Connection
    {
        public readonly Bot Bot;
        private readonly ServerEnum _serverType;

        private readonly Socket _socket;
        private byte[] _dataBuffer;
        private string _receivedMessage = "";

        public Connection(Bot bot, string city)
        {
            Bot = bot;
            city = city.Replace(".timezero.ru", "");

            _serverType = city switch
            {
                "main" => ServerEnum.Main,
                "test" => ServerEnum.Main,
                "city1" => ServerEnum.Game,
                "city2" => ServerEnum.Game,
                "city3" => ServerEnum.Game,
                "city4" => ServerEnum.Game,
                "localhost" => ServerEnum.Game,
                "chat" => ServerEnum.Chat,
                "chat2" => ServerEnum.Chat,
                _ => ServerEnum.Main
            };

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(new IPEndPoint(Dns.GetHostAddresses(city + ".timezero.ru")[0], 5190));

            Start();
        }

        private void Start()
        {
            _dataBuffer = new byte[1024 * 32];

            WaitForData();

            Ping();
        }

        public void SendData(PacketOut packetOut)
        {
            Log.Verbose("[{0}] [{1}] [{2}] [{3}] {@attributes} {@child}", "OUT", _serverType, Bot?.Login,
                packetOut.Name,
                packetOut.Attributes, packetOut.Children);

            var packet = packetOut.GetXml();
            SendData(packet);
        }

        public void SendData(string data)
        {
            SendData(Encoding.UTF8.GetBytes(data + (char) 0x00));
        }

        private void SendData(byte[] data)
        {
            try
            {
                if (!_socket.Connected) return;

                _socket.SendAsync(data, SocketFlags.None);
            }
            catch (Exception e)
            {
                Log.Error(e, "Connection");
            }
        }

        private void WaitForData()
        {
            try
            {
                if (!_socket.Connected) return;

                var socketReceiveCompleted = new SocketAsyncEventArgs();
                socketReceiveCompleted.SetBuffer(_dataBuffer, 0, _dataBuffer.Length);
                socketReceiveCompleted.Completed += DataReceived;

                if (!_socket.ReceiveAsync(socketReceiveCompleted))
                {
                    DataReceived(null, socketReceiveCompleted);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Connection");
            }
        }

        private void DataReceived(object sender, SocketAsyncEventArgs socketAsyncEventArgs)
        {
            try
            {
                if (!_socket.Connected) return;

                var numReceivedBytes = socketAsyncEventArgs.BytesTransferred;

                if (numReceivedBytes > 0)
                {
                    var bytes = ByteUtil.ChompBytes(socketAsyncEventArgs.Buffer, 0, numReceivedBytes);

                    var data = Encoding.UTF8.GetString(bytes);

                    data = _serverType switch
                    {
                        ServerEnum.Chat => CompressionUtil.DecompressChat(data),
                        ServerEnum.Game => CompressionUtil.DecompressGame(data),
                        _ => data
                    };

                    _receivedMessage += data;

                    if (_receivedMessage.Contains((char) 0x00))
                    {
                        var messages = _receivedMessage.Split((char) 0x00);

                        foreach (var message in messages)
                        {
                            if (message.Length == 0) continue;

                            var packet = new XmlRead("<root>" + message + "</root>");

                            foreach (var incoming in packet.Children)
                            {
                                var packetType =
                                    Neptunium.PacketManager.GetPacketHandler(incoming.XmlName, _serverType);

                                if (packetType == null)
                                {
                                    Log.Debug("[{0}] [{1}] [{2}] [{3}] [{4}] {@attributes} {@child}", "IN", _serverType,
                                        Bot?.Login,
                                        Bot?.Login, incoming.XmlName,
                                        incoming.Attributes,
                                        incoming.Children);
                                    continue;
                                }

                                try
                                {
                                    Log.Verbose("[{0}] [{1}] [{2}] [{3}] [{4}] {@attributes} {@child}", "IN",
                                        _serverType,
                                        Bot?.Login,
                                        Bot?.Login, incoming.XmlName,
                                        incoming.Attributes,
                                        incoming.Children);
                                    var packetIn = (PacketIn) Activator.CreateInstance(packetType);
                                    packetIn.Read(this, incoming);
                                    packetIn.Handle();
                                }
                                catch (Exception ex)
                                {
                                    Log.Error(ex, "Packet Error");
                                }
                            }
                        }

                        _receivedMessage = messages[^1];
                    }

                    WaitForData();
                }

                else
                {
                    Shutdown();
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "packet");
            }
        }

        private void Shutdown()
        {
            if (Bot?.GameConnection == this)
            {
                Log.Error("[{0}] GAME DISCONNECT");
            }
            else if (Bot?.ChatConnection == this)
            {
                Log.Error("[{0}] CHAT DISCONNECT");
            }

            try
            {
                if (_socket.Connected) _socket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception e)
            {
                Log.Error(e, "Connection");
            }
            finally
            {
                _socket.Close();
            }
        }

        private async Task Ping()
        {
            while (_socket.Connected)
            {
                var delayTask = Task.Delay(45000);
                await delayTask;
                SendData(new NPacketOut());
            }
        }
    }
}