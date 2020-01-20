using System;
using System.Threading.Tasks;

namespace GPConnectAdaptor
{
    public interface ISlotHttpClientWrapper
    {
        Task<string> GetAsync(DateTime start, DateTime end);
    }
}