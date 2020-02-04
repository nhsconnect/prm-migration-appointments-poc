using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public interface ITokenPayloadBuilder
    {
        JwtModel BuildPayload();
    }
}