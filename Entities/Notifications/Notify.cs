using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Notifications
{
    public class Notify
    {
        public Notify()
        {
            Notitycoes = new List<Notify>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; }
        [NotMapped]
        public string mensagem { get; set; }

        public List<Notify> Notitycoes;

        public bool ValidarPropriedadeString(string valor, string nomePropriedade)
        {

            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notitycoes.Add(new Notify
                {
                    mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;
        }

        public bool ValidarPropriedadeInt(int valor, string nomePropriedade)
        {

            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notitycoes.Add(new Notify
                {
                    mensagem = "Campo Obrigatório deve ser maior que 0",
                    NomePropriedade = "nomePropriedade"
                });

                return false;
            }

            return true;
        }

        public bool ValidarPropriedadeDecimal(decimal valor, string nomePropriedade)
        {

            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notitycoes.Add(new Notify
                {
                    mensagem = "Campo Obrigatório deve ser maior 0",
                    NomePropriedade = "nomePropriedade"
                });

                return false;
            }

            return true;

        }


    }
}
