using EmailRegistrationApp.Controllers;
using EmailRegistrationApp.Models;
using EmailRegistrationApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailRegistrationApp.Controllers
{
    public class RegistrationController : Controller
    {
          private readonly IEmailService _emailService;
        private readonly ITemplateService _templateService;
        private readonly IConfiguration _configuration;

        public RegistrationController(
            IEmailService emailService,
            ITemplateService templateService,
            IConfiguration configuration)
        {
            _emailService = emailService;
            _templateService = templateService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new RegistrationModel());
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var loginUrl = _configuration["AppSettings:LoginUrl"];
            
            var emailBody = $@"
            Hello {model.FirstName} {model.LastName},

            Welcome to our platform! Your account has been created successfully.

            First Name: {model.FirstName}
            Last Name: {model.LastName}
            Email: {model.Email}
            Temporary Password: {model.TemporaryPassword}

            Login Link: {loginUrl}

            For security reasons, please change your password after first login.

            Thank you for registering!
            This is an automated message, please do not reply.";

            var emailRequest = new EmailRequest
            {
                ToEmail = model.Email,
                Subject = $"Welcome, {model.FirstName}! Your Account is Ready",
                Body = emailBody,
                IsHtml = false
            };

            var result = await _emailService.SendEmailAsync(emailRequest);

            if (result)
            {
                TempData["SuccessMessage"] = "Registration successful! An email has been sent to your address.";
                return RedirectToAction("Success");
            }
            else
            {
                ModelState.AddModelError("", "Failed to send email. Please try again later.");
                return View("Index", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserViaTemplate(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var loginUrl = _configuration["AppSettings:LoginUrl"];
            
            var template = await _templateService.GetTemplateAsync();
            var emailBody = _templateService.PopulateTemplate(template, model, loginUrl);

            var emailRequest = new EmailRequest
            {
                ToEmail = model.Email,
                Subject = $"Welcome to the Community, {model.FirstName}!",
                Body = emailBody,
                IsHtml = true
            };

            var result = await _emailService.SendEmailAsync(emailRequest);

            if (result)
            {
                TempData["SuccessMessage"] = "Registration successful! An email with HTML template has been sent to your address.";
                return RedirectToAction("Success");
            }
            else
            {
                ModelState.AddModelError("", "Failed to send email. Please try again later.");
                return View("Index", model);
            }
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}