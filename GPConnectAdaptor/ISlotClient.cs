using System;
using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public interface ISlotClient
    {
        SlotResponse GetSlots(DateTime start, DateTime end);
    }
}