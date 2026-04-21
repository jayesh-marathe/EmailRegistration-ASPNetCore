using EmailRegistrationApp.Models;
using System.Threading.Tasks;

namespace EmailRegistrationApp.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailRequest emailRequest);
    }
}
