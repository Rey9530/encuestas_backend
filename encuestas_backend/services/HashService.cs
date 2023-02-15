using System.Security.Cryptography;
using encuestas_backend.TDOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace encuestas_backend.services
{
    public class HashService
    {
        public ResultadoHash Hash(string textoPlano){
            var sal = new byte[16];
            using (var ramdom = RandomNumberGenerator.Create()){
                ramdom.GetBytes(sal);
            }
            return Hash(textoPlano, sal);
        }

        public ResultadoHash Hash(string textoPlano, byte[] sal){
            var llaveDerivada = KeyDerivation.Pbkdf2(password:textoPlano, salt:sal,prf:KeyDerivationPrf.HMACSHA1, iterationCount:10000, numBytesRequested:32);
            var hash = Convert.ToBase64String(llaveDerivada);
            return new ResultadoHash(){
                hash=hash,
                salt=sal
            };
        }
    }
}