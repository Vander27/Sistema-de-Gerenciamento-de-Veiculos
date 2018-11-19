using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Projeto.Util
{
    public class Criptografia
    {
        public static string EncriptarSenha(string senha)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(senha));

            return BitConverter.ToString(hash)
                    .Replace("-", string.Empty)
                    .ToUpper();
        }
    }
}
