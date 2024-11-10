using Microsoft.AspNetCore.Http.HttpResults;
using Online_Store_Management.DataAccess;
using Online_Store_Management.Interfaces;
namespace Online_Store_Management.Services
{
    public class NotificationService : INotificationService
    {
        public void Notification(CustomerDbModel customerUpdate)
        {
            Console.WriteLine("Customer has been updated");
        }
    }
}
