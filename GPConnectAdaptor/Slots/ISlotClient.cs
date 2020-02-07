using System;
using System.Threading.Tasks;
using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public interface ISlotClient
    {
        Task<SlotResponse> GetSlots(DateTime start, DateTime end);
    }
}