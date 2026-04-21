
A complete ASP.NET Core MVC web application that provides user registration functionality with email verification using SMTP. The application supports both plain text and HTML email templates with dynamic placeholder replacement.

## ✨ Features

- ✅ **Complete MVC Architecture** - Proper separation of concerns following MVC pattern
- ✅ **User Registration Form** - Collects First Name, Last Name, Email, and Temporary Password
- ✅ **Strong Password Validation** - Enforces password security rules:
  - Minimum 10 characters
  - At least 1 uppercase letter
  - At least 1 lowercase letter
  - At least 1 numeric digit
  - At least 1 special character
- ✅ **Dual Email Options**:
  - Plain text email registration
  - HTML template-based email registration
- ✅ **SMTP Email Integration** - Send emails via any SMTP provider (Gmail, Outlook, SendGrid, etc.)
- ✅ **HTML Template Support** - Dynamic placeholder replacement ({{FirstName}}, {{LastName}}, etc.)
- ✅ **Server-Side Validation** - Comprehensive validation for all form fields
- ✅ **Exception Handling** - Robust error handling for email sending failures
- ✅ **Responsive Design** - Mobile-friendly registration form
- ✅ **Modular Architecture** - Clean, maintainable, and extensible code structure

## 🚀 Technologies Used

- **Backend Framework**: ASP.NET Core MVC 8.0
- **Email Service**: MailKit & MimeKit
- **Frontend**: HTML5, CSS3, JavaScript
- **Validation**: jQuery Validation Unobtrusive
- **Architecture**: MVC Pattern with Dependency Injection

## 📋 Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or later / [VS Code](https://code.visualstudio.com/)
- SMTP server access (Gmail, Outlook, or any SMTP provider)

