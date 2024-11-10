namespace ByteBank.Service;

public interface ICambioService
{
    decimal Calcular(string origem, string destino, decimal valor);
}
