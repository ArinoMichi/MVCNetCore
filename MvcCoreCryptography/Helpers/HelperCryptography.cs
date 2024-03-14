using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace MvcCoreCryptography.Helpers
{
    public class HelperCryptography
    {
        //TENDREMOS UN METODO PARA CIFRAR EL PASSWORD
        //VAMOS A RECIBIR EL PASSWORD (STRING) Y EL SALT (STRING)
        //Y DEVOLVEREMOS EL ARRAY DE BYTES[] DEL RESULTADO CIFRADO
        public static byte[] EncryptPassword(string password, string salt)
        {
            string contenido = password + salt;
            SHA512 sha = SHA512.Create();
            //CONVERTIMOS contenido A BYTES[]
            byte[] salida = Encoding.UTF8.GetBytes(contenido);
            //CREAMOS LAS ITERACIONES
            for (int i = 1; i <= 114; i++)
            {
                salida = sha.ComputeHash(salida);
            }
            sha.Clear();
            return salida;
        }
    }
}
