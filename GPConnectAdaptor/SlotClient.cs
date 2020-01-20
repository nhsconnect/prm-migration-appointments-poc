using System;
using System.Net;
using System.Threading.Tasks;
using GPConnectAdaptor.Models;
using Newtonsoft.Json;

namespace GPConnectAdaptor
{
    public class SlotClient : ISlotClient
    {
        private readonly ISlotHttpClientWrapper _clientWrapper;

        public SlotClient(ISlotHttpClientWrapper clientWrapper)
        {
            _clientWrapper = clientWrapper;
        } 
        public async Task<SlotResponse> GetSlots(DateTime start, DateTime end)
        {
            string response = await _clientWrapper.GetAsync(start, end);
            return JsonConvert.DeserializeObject<SlotResponse>(response);
        }
    }
}