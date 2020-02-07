using GPConnectAdaptor.Models;
using Newtonsoft.Json;

namespace GPConnectAdaptor
{
    public class SlotResponseDeserializer : ISlotResponseDeserializer
    {
        public SlotResponse Deserialize(string response)
        {
            var deserializedResponse = JsonConvert.DeserializeObject<SlotResponse>(response);
            return deserializedResponse;
        }
    }
}