using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;


namespace Notification
{
    public class NotificationContext
    {
        private readonly List<Notificacao> _notifications;
        public IReadOnlyCollection<Notificacao> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();

        public NotificationContext()
        {
            _notifications = new List<Notificacao>();
        }

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new Notificacao(key, message));
        }

        public void AddNotification(Notificacao notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IReadOnlyCollection<Notificacao> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(IList<Notificacao> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ICollection<Notificacao> notifications)
        {
            _notifications.AddRange(notifications);
        }


        public string GetErros()
        {
            var erros = new System.Text.StringBuilder();
            foreach (var item in Notifications)
            {
                erros.Append(item.Mensagem);
            }
            return erros.ToString();
        }

        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddNotification(error.ErrorCode, error.ErrorMessage);
            }
        }
    }
}
