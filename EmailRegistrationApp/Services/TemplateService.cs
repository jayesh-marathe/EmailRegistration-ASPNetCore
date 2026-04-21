using EmailRegistrationApp.Models;
using System.IO;
using System.Threading.Tasks;


namespace EmailRegistrationApp.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly string _templatePath = "wwwroot/templates/RegistrationTemplate.html";

        public async Task<string> GetTemplateAsync()
        {
            if (File.Exists(_templatePath))
            {
                return await File.ReadAllTextAsync(_templatePath);
            }
            return GetDefaultTemplate();
        }

        public string PopulateTemplate(string template, RegistrationModel registration, string loginUrl)
        {
            return template
                .Replace("{{FirstName}}", registration.FirstName)
                .Replace("{{LastName}}", registration.LastName)
                .Replace("{{Email}}", registration.Email)
                .Replace("{{TemporaryPassword}}", registration.TemporaryPassword)
                .Replace("{{LoginLink}}", loginUrl)
                .Replace("{{LoginUrl}}", loginUrl);
        }

        private string GetDefaultTemplate()
        {
            return @"
<!DOCTYPE html>
<html>
<head>
    <style>
        body { font-family: Arial, sans-serif; }
        .container { max-width: 600px; margin: 0 auto; padding: 20px; }
        .header { background-color: #4CAF50; color: white; padding: 10px; text-align: center; }
        .content { padding: 20px; border: 1px solid #ddd; }
        .button { background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; display: inline-block; margin: 10px 0; }
        .footer { text-align: center; padding: 10px; font-size: 12px; color: #666; }
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h2>Welcome to Our Platform!</h2>
        </div>
        <div class='content'>
            <h3>Hello {{FirstName}} {{LastName}},</h3>
            <p>Your account has been successfully created.</p>
            <p><strong>Email:</strong> {{Email}}</p>
            <p><strong>Temporary Password:</strong> {{TemporaryPassword}}</p>
            <p>Please click the button below to login and change your password:</p>
            <p><a href='{{LoginLink}}' class='button'>Login to Your Account</a></p>
            <p>Or copy this link: {{LoginUrl}}</p>
            <p>For security reasons, please change your password after first login.</p>
        </div>
        <div class='footer'>
            <p>This is an automated message, please do not reply.</p>
        </div>
    </div>
</body>
</html>";
        }
    }
}