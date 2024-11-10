using System.Net;
using System.Reflection;
using System.Text;

namespace ByteBank.Portal.Infra;

public class ManipuladorRequicaoController
{
    public void Manipular(HttpListenerResponse response, string path)
    {
        var partes = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

        var controllerNome = partes[0];
        var actionNome = partes[1];

        var controllerNomeCompleto = $"ByteBank.Portal.Controller.{controllerNome}Controller";

        var controllerWrapper = Assembly.GetExecutingAssembly().CreateInstance(controllerNomeCompleto);

        var methodInfo = controllerWrapper.GetType().GetMethod(actionNome);

        var retornoAction = methodInfo.Invoke(controllerWrapper, null) as string;

        var buffer = Encoding.UTF8.GetBytes(retornoAction);

        response.StatusCode = 200;
        response.ContentType = "text/html; charset=utf-8";
        response.ContentLength64 = buffer.Length;

        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
    }
}
