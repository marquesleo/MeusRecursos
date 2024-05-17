using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.AutoMapper
{
    public class UtilMapper
    {
        public static string getImagembase64String(byte[] imagem)
        {
            if (imagem != null && imagem.Length > 0)
            {
                string base64String = Convert.ToBase64String(imagem, 0, imagem.Length);
                return "data:image/png;base64," + base64String;
            }
            return null;
        }
    }
}
