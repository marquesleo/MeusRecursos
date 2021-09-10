using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Utils
{
	public  class Criptografia
	{
		byte[] salt = new byte[128 / 8];
		private const string senha = "thug$2Pac";


		public Byte[] CriptografarSenha(string valor)
		{
			var chave = new Rfc2898DeriveBytes(senha, salt);
			var algoritmo = new RijndaelManaged();

			algoritmo.Key = chave.GetBytes(16);

			var fonteBytes = new UnicodeEncoding().GetBytes(valor);

            using (MemoryStream streamFonte = new MemoryStream(fonteBytes))
            {
                using (MemoryStream streamDestino = new MemoryStream())
                {
					using (CryptoStream crypto = new CryptoStream(streamFonte, algoritmo.CreateEncryptor(), CryptoStreamMode.Read))
					{
						moveBytes(crypto, streamDestino);
						return streamDestino.ToArray();
					}
                }
            }
	
		}

		private void moveBytes(Stream fonte , Stream destino)
        {
			byte[] bytes = new byte[2049];
			var contador = fonte.Read(bytes, 0, bytes.Length - 1);
            while (0 != contador)
            {
				destino.Write(bytes, 0, contador);

				contador = fonte.Read(bytes, 0, bytes.Length - 1);

			}

		}
		public string Decriptografar(Byte[] valor)
        {
			if(valor == null)
            {
				throw new Exception("Os dados não estão criptografados!");
			}

			var chave = new Rfc2898DeriveBytes(senha, salt);

			var algoritmo = new RijndaelManaged();
			algoritmo.Key = chave.GetBytes(16);
			algoritmo.IV = chave.GetBytes(16);

            using (MemoryStream StreamFonte = new MemoryStream(valor))
            {
                using (MemoryStream StreamDestino = new MemoryStream())
                {
                    using (CryptoStream crypto = new CryptoStream(StreamFonte, algoritmo.CreateDecryptor(), CryptoStreamMode.Read))
                    {
						moveBytes(crypto, StreamDestino);
						Byte[] bytesDescriptografados = StreamDestino.ToArray();
						var mensagemDescriptografada = new UnicodeEncoding().GetString(bytesDescriptografados);
						return mensagemDescriptografada;
					}
                }
            }  
		}
		
		
	}
}
