using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public interface ISlotResponseDeserializer
    {
        public SlotResponse Deserialize(string response);
    }
}