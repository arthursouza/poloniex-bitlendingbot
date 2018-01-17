namespace Jojatekok.PoloniexAPI.General.EventArgs
{
    public class TrollboxMessageEventArgs : System.EventArgs
    {
        internal TrollboxMessageEventArgs(string senderName, uint? senderReputation, ulong messageNumber, string messageText)
        {
            SenderName = senderName;
            SenderReputation = senderReputation;
            MessageNumber = messageNumber;
            MessageText = messageText;
        }

        public string SenderName { get; }
        public uint? SenderReputation { get; }
        public ulong MessageNumber { get; }
        public string MessageText { get; }
    }
}