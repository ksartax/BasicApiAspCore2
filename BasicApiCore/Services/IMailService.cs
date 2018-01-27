namespace BasicApiCore.Services
{
    public interface IMailService
    {
        void Send(string subjec, string message);
    }
}
