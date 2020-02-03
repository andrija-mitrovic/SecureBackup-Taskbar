using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupApp.Library.Service
{
    public class Notification : INotification
    {
        private List<INotification> _notifications = new List<INotification>();

        public void Add(INotification notify)
        {
            this._notifications.Add(notify);
        }

        public void Send()
        {
            foreach(var _notification in _notifications)
                _notification.Send();
        }
    }
}
