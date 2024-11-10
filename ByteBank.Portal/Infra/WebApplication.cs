using ByteBank.Portal.Controller;
using System.Net;
using System.Reflection;
using System.Text;

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

    public void Iniciar()
    {
        while (true)
            ManipularRequisicao();
    }

    private void ManipularRequisicao()
    {
        var httpListener = new HttpListener();

        foreach (var prefixo in _prefixes)
            httpListener.Prefixes.Add(prefixo);

        httpListener.Start();

        var contexto = httpListener.GetContext();
        var requisicao = contexto.Request;
        var resposta = contexto.Response;

        var path = requisicao.Url.AbsolutePath;

        if (Utilidades.EhArquivo(path))
        {
            var manipulador = new ManipuladorRequisicaoArquivo();
            manipulador.Manipular(resposta, path);
        }
        else if (path == "/Cambio/MXN")
        {
            var controller = new CambioController();
            var paginaConteudo = controller.MXN();

            var buffer = Encoding.UTF8.GetBytes(paginaConteudo);
            
            resposta.StatusCode = 200;
            resposta.ContentType = "text/html; charset=utf-8";
            resposta.ContentLength64 = buffer.Length;

            resposta.OutputStream.Write(buffer, 0, buffer.Length);
            resposta.OutputStream.Close();
        }
        else if (path == "/Cambio/USD")
        {
            var controller = new CambioController();
            var paginaConteudo = controller.USD();

            var buffer = Encoding.UTF8.GetBytes(paginaConteudo);

            resposta.StatusCode = 200;
            resposta.ContentType = "text/html; charset=utf-8";
            resposta.ContentLength64 = buffer.Length;

            resposta.OutputStream.Write(buffer, 0, buffer.Length);
            resposta.OutputStream.Close();
        }

        httpListener.Stop();
        
    }
}
