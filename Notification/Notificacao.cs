using System;
using System.Collections.Generic;
using System.Text;

namespace Notification
{
    public class Notificacao
    {
        public string Key { get; private set; }
        public string Mensagem { get; }
        public Notificacao(string key, string mensagem)
        {
            Mensagem = mensagem;
            this.Key = key;
        }
    }
}
