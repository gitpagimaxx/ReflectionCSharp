using ByteBank.Portal.Infra;

var prefixes = new string[] { "http://localhost:5341/" };

var webApplication = new WebApplication(prefixes);

webApplication.Start();