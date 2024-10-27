using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infra;

public class WebApplication
{
    private readonly string[] _prefixes;

    public WebApplication(string[] prefixes)
    {
        if (prefixes == null)
            throw new ArgumentNullException(nameof(prefixes));

        _prefixes = prefixes;
    }

    public void Start()
    {
        Console.WriteLine("Starting Web Application!");

        var httpListener = new HttpListener();

        foreach (var prefix in _prefixes)
            httpListener.Prefixes.Add(prefix);

        httpListener.Start();

        var context = httpListener.GetContext();
        var response = context.Response;
        var request = context.Request;

        var path = request.Url.AbsolutePath;

        if (path == "/Assets/css/styles.css")
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "styles.css";
            var researchStream = assembly.GetManifestResourceStream(resourceName);
            var bytesResource = new byte[researchStream.Length];

            researchStream.Read(bytesResource, 0, bytesResource.Length);

        }

        if (path == "/")
            path = "/index.html";

        var responseString = "<html><body>Hello World!</body></html>";
        var responseBytes = Encoding.UTF8.GetBytes(responseString);

        response.ContentType = "text/html; charset=utf-8";
        response.StatusCode = 200;
        response.ContentLength64 = responseBytes.Length;

        response.OutputStream.Write(responseBytes, 0, responseBytes.Length);

        response.OutputStream.Close();

        httpListener.Stop();

        Console.WriteLine("Web Application started!");
    }
}
