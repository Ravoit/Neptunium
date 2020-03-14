using System;
using System.Collections.Generic;
using Neptunium.Game;
using Neptunium.Messages.Incoming.CHAT;
using Neptunium.Messages.Incoming.Game;
using Neptunium.Messages.Incoming.Main;
using Neptunium.Network;

namespace Neptunium.Messages
{
    public class PacketManager : Manager
    {
        private Dictionary<string, Type> _mainRequestHandlers;
        private Dictionary<string, Type> _gameRequestHandlers;
        private Dictionary<string, Type> _chatRequestHandlers;

        protected override void Load()
        {
            _mainRequestHandlers = new Dictionary<string, Type>
            {
                {"LIST", typeof(ListPacketIn)},
            };
            _chatRequestHandlers = new Dictionary<string, Type>
            {
                {"A", typeof(APacketIn)},
                {"D", typeof(DPacketIn)},
                {"RADIOLIST", typeof(RadioListPacketIn)},
                {"R", typeof(RPacketIn)},
                {"S", typeof(SPacketIn)},
                {"Z", typeof(ZPacketIn)},
            };

            _gameRequestHandlers = new Dictionary<string, Type>
            {
                {"KEY", typeof(KeyPacketIn)},
                {"OK", typeof(OkPacketIn)},
                {"CHAT", typeof(ChatPacketIn)},
                {"ERROR", typeof(ErrorPacketIn)},
            };
        }

        protected override void Unload()
        {
            _chatRequestHandlers.Clear();
            _gameRequestHandlers.Clear();
            _mainRequestHandlers.Clear();
        }

        public Type GetPacketHandler(string packet, ServerEnum serverType)
        {
            return serverType switch
            {
                ServerEnum.Main => (_mainRequestHandlers.ContainsKey(packet) ? _mainRequestHandlers[packet] : null),
                ServerEnum.Game => (_gameRequestHandlers.ContainsKey(packet) ? _gameRequestHandlers[packet] : null),
                ServerEnum.Chat => (_chatRequestHandlers.ContainsKey(packet) ? _chatRequestHandlers[packet] : null),
                _ => null
            };
        }
    }
}