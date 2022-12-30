namespace UMan.DataAccess.Security.Common
{
    public interface IJwtTokenGenerator
    {
        string CreateToken(string username);
    }
}
