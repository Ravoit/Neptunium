namespace Neptunium.Util
{
    public static class CompressionUtil
    {
        private static string[] _replaceChat =
        {
            "\"/>", "\x01", "<A t=\"", "\x02", "<R t=\"", "\x03", "<D t=\"", "\x04", "<S t=\"", "\x05", "<Z t=\"",
            "\x06"
        };

        private static string[] _replaceGame =
        {
            "t=\"2\"/></USER><USER login=\"", "\x01^", "<TURN><USER login=\"", "\x01r", "></USER><USER login=\"",
            "\x01W", "\"/><MAP v=\"", "\x01f", "\" t=\"2\"/><a sf=\"", "\x01M", "\" t=\"1\" direct=\"", "\x01N",
            "><a sf=\"0\" t=\"", "\x01`", "\" t=\"5\" xy=\"", "\x01c", ".1\" slot=\"", "\x01j", "\" quality=\"",
            "\x01l", "\" massa=\"1", "\x01m", "\" maxquality=\"", "\x01n", "><a sf=\"6\" t=\"2\"/><a ", "\x01B",
            "/><a sf=\"6\" t=\"", "\x01o", "\" damage=\"S", "\x01p", "\" made=\"AR$\" ", "\x01q", "\" nskill=\"",
            "\x01s", "\" st=\"G,H\" ", "\x01t", "\" type=\"1\"", "\x01u", "section=\"0\" damage=\"", "\x01~",
            "\" section=\"", "\x01", "=\"1\" type=\"", "\x01A", "protect=\"S", "\x01C", " ODratio=\"1\" loc_time=\"",
            "\x01V", "\"/>\n</O>\n<O id=\"", "\x01D", "\"/>\n<O id=\"", "\x01E", "level=", "\x01F", " min=\"", "\x01H",
            " txt=\"ammo ", "\x01I", " txt=\"BankCell Key (copy) #", "\x01J", "\" txt=\"Coins\" massa=\"1\" ", "\x01K",
            " cost=\"0\" ", "\x01L",
            ".1\" name=\"b1-g2\" txt=\"Boulder\" massa=\"5\" st=\"G,H\" made=\"AR$\" section=\"0\" damage=\"S2-5\" shot=\"7-1\" nskill=\"4\" OD=\"1\" type=\"9.1\"/>",
            "\x01S", " psy=\"0\" man=\"1\" maxHP=\"", "\x01T", " freeexchange=\"1\" ", "\x01U",
            "\" virus=\"0\" login=\"", "\x01Y", "\" ne=\",,,,,\" ne2=\",,,,,\" nark=\"0\" gluk=\"0\" ", "\x01Z",
            "\" max_count=\"", "\x01[", "\" calibre=\"", "\x01\\", "\" count=\"", "\x01]", "\" build_in=\"", "\x01O",
            "\" shot=\"", "\x01_", "\" range=\"", "\x01a", ".1\" slot=\"A\" name=\"b", "\x01b",
            ".1\" slot=\"B\" name=\"b", "\x01d", ".1\" slot=\"C\" name=\"b", "\x01h", ".1\" slot=\"D\" name=\"b",
            "\x01e", ".1\" slot=\"E\" name=\"b", "\x01g", ".1\" slot=\"F\" name=\"b", "\x01{",
            ".1\" slot=\"GH\" name=\"b", "\x01v", "\" slot=\"", "\x01X",
            " psy=\"0\" man=\"3\" maxPsy=\"0\" ODratio=\"1\" img=\"rat\" group=\"2\" battleid=\"", "\x01i",
            ".1\" name=\"b2-s5\" txt=\"Silicon\" massa=\"50\" ", "\x01k",
            ".1\" name=\"b2-s8\" txt=\"Venom\" massa=\"70\" ", "\x01Q",
            ".1\" name=\"b2-s4\" txt=\"Organic\" massa=\"30\" ", "\x01w",
            ".1\" name=\"b2-s2\" txt=\"Precious metals\" massa=\"500\" ", "\x01x",
            ".1\" name=\"b2-s7\" txt=\"Gems\" massa=\"80\" ", "\x01y",
            ".1\" name=\"b2-s6\" txt=\"Radioactive materials\" massa=\"800\" ", "\x01z",
            ".1\" name=\"b2-s3\" txt=\"Polymers\" massa=\"30\" ", "\x01|",
            "<BATTLE t=\"45\" t2=\"45\" turn=\"1\" cl=\"0\" ", "\x01}", "\" ODratio=\"1\" ", "\x01P",
            "\" p=\"\"/></L><L X=\"", "\x01R", "zzzzzz", "\x01G", "\"/><a sf=\"", "\x02", ">\n<O id=\"", "\x03",
            "><O id=\"", "\x04", "      ", "\x05", "00\" ", "\x06", " txt=\"", "\x07", " name=\"b", "\b"
        };

        public static string DecompressChat(string data)
        {
            for (var i = _replaceChat.Length - 1; i >= 0; i -= 2)
            {
                data = data.Replace(_replaceChat[i], _replaceChat[i - 1]);
            }

            return data;
        }

        public static string DecompressGame(string data)
        {
            for (var i = _replaceChat.Length - 1; i >= 0; i -= 2)
            {
                data = data.Replace(_replaceGame[i], _replaceGame[i - 1]);
            }

            return data;
        }
    }
}