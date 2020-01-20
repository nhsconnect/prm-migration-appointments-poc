using System;

namespace GPConnectAdaptor
{
    public interface ISlotUriGenerator
    {
        Uri GetSlotUri(DateTime start, DateTime end);
    }
}