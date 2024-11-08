using Online_Store_Management.DataAccess;
using Online_Store_Management.Services;

namespace Online_Store_Management.Interfaces
{
    public interface INotificationService
    {
        public void Notification(CustomerDbModel customerUpdate);
    }
}
