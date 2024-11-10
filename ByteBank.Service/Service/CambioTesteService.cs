namespace ByteBank.Service.Service;

public class CambioTesteService : ICambioService
{
    private readonly Random _random = new Random();

    public decimal Calcular(string origem, string destino, decimal valor)
    {
        return valor + (decimal)_random.NextDouble();
    }
}
