using System.Threading.Tasks;

namespace LicensingSolution.Models.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}