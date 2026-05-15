using System;

namespace WhatsappDesktop.Features.Infraestrutura
{
    public abstract class BaseRepository
    {
        protected void ExecutarComLog(Action acao, string nomeMetodo)
        {
            try
            {
                acao();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] {nomeMetodo}: {ex.Message}");
                throw;
            }
        }

        protected T ExecutarComLog<T>(Func<T> funcao, string nomeMetodo)
        {
            try
            {
                return funcao();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] {nomeMetodo}: {ex.Message}");
                throw;
            }
        }
    }
}
