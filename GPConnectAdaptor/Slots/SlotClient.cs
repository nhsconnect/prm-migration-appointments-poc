using System;
using System.Net;
using System.Threading.Tasks;
using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public class SlotClient : ISlotClient
    {
        private readonly ISlotHttpClientWrapper _clientWrapper;
        private readonly ISlotResponseDeserializer _deserializer;

        public SlotClient(ISlotHttpClientWrapper clientWrapper, ISlotResponseDeserializer deserializer)
        {
            _clientWrapper = clientWrapper;
            _deserializer = deserializer;
        } 
        public async Task<SlotResponse> GetSlots(DateTime start, DateTime end)
        {
            string response = await _clientWrapper.GetAsync(start, end);
            return _deserializer.Deserialize(response);
        }
    }
}