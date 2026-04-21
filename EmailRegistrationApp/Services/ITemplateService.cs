using EmailRegistrationApp.Models;

namespace EmailRegistrationApp.Services
{
    public interface ITemplateService
    {
        Task<string> GetTemplateAsync();
        string PopulateTemplate(string template, RegistrationModel registration, string loginUrl);
    }
}
