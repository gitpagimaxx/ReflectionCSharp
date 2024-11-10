using ByteBank.Portal.Infra;
using ByteBank.Service;
using ByteBank.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Controller;

public class CambioController
{
    private readonly ICambioService _cambioService;

    public CambioController()
    {
        _cambioService = new CambioTesteService();
    }

    public string MXN()
    {
        var valorFinal = _cambioService.Calcular("MXN", "BRL", 1);
        var nomeCompletoAssembly = "ByteBank.Portal.View.Cambio.MXN.html";
        var assembly = Assembly.GetExecutingAssembly();
        var streamRecurso = assembly.GetManifestResourceStream(nomeCompletoAssembly);

        if (streamRecurso == null)
            return "Erro durante a leitura do arquivo";

        var streamLeitor = new StreamReader(streamRecurso);
        var textoPagina = streamLeitor.ReadToEnd();

        var textoResultado = textoPagina.Replace("VALOR_EM_REAIS", valorFinal.ToString());

        return textoResultado;
    }

    public string USD()
    {
        var valorFinal = _cambioService.Calcular("USD", "BRL", 1);
        var nomeCompletoAssembly = "ByteBank.Portal.View.Cambio.USD.html";
        var assembly = Assembly.GetExecutingAssembly();
        var streamRecurso = assembly.GetManifestResourceStream(nomeCompletoAssembly);

        if (streamRecurso == null)
            return "Erro durante a leitura do arquivo";

        var streamLeitor = new StreamReader(streamRecurso);
        var textoPagina = streamLeitor.ReadToEnd();

        var textoResultado = textoPagina.Replace("VALOR_EM_REAIS", valorFinal.ToString());

        return textoResultado;
    }
}
