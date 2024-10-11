# Event Registration System

## Overview
The Event Registration System is a web application built with .NET 7 that allows users to register for events. It includes email confirmation functionality using Mailjet.

## Prerequisites
- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [Mailjet Account](https://www.mailjet.com/)

## Setup Instructions

### 1. Clone the Repository
```bash
git clone https://github.com/Alz3bi/EventRegistrationSystem.git cd EventRegistrationSystem
```

### 2. Configure Mailjet Integration
1. Sign up for a Mailjet account and obtain your API key and secret.
2. Open the `appsettings.json` file and add your Mailjet credentials:
    ```json
    {
        "Mailjet": {
            "ApiKey": "your-mailjet-api-key",
            "ApiSecret": "your-mailjet-api-secret",
            "FromEmail": "your-email@example.com"
        }
    }
	```


### 3. Build the Project
Open the solution in Visual Studio and build the project to restore the dependencies and ensure everything is set up correctly.

### 4. Update Database
If your project uses a database, update it using Entity Framework migrations:
```bash
dotnet ef database update
```


## Running the Application
1. Open the solution in Visual Studio.
2. Set the startup project to `EventRegistrationSystem`.
3. Press `F5` or click on the `Run` button to start the application.

## Testing the Registration and Email Functionality
1. Navigate to the registration page in your browser.
2. Fill out the registration form with the event details and submit.
3. Check the email inbox of the recipient email address provided in the form. You should receive a confirmation email from the Event Registration System.

## Troubleshooting
- Ensure that your Mailjet API key and secret are correctly configured in the `appsettings.json` file.
- Check the application logs for any errors related to email sending.

## Contributing
Contributions are welcome! Please open an issue or submit a pull request for any improvements or bug fixes.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
