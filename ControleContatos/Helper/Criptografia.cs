using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ControleContatos.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string senha)
        {
            var hash = SHA1.Create();
            var encodeing = new ASCIIEncoding();
            var array = encodeing.GetBytes(senha);

            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach(var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}
