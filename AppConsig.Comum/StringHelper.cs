using System;
using System.Security.Cryptography;

namespace AppConsig.Comum
{
    public class StringHelper
    {
        public static string ObterTextoAleatorio(int tamanho = 10)
        {
            // Apenas para manter um padrão de tamanho mínimo para as senhas.
            if (tamanho < 10)
            {
                throw new InvalidOperationException("tamanho não pode ser menor que 10");
            }

            string valor;

            using (var geradorNumeroAleatorio = new RNGCryptoServiceProvider())
            {
                var numeroAleatorio = new byte[64];
                geradorNumeroAleatorio.GetBytes(numeroAleatorio);

                valor = Convert.ToBase64String(numeroAleatorio);
            }

            valor = valor.Substring(0, tamanho);

            return valor;
        }
    }
}