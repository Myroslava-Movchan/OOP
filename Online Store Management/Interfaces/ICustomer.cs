using Online_Store_Management.Models;

namespace Online_Store_Management.Interfaces
{
    public interface ICustomer
    {
        void LogAction(string message);
        void SetCustomerLogFileStream(FileStream customerLogFileStream);
        void Dispose();
        Discount GetNewCustomer();
        Discount GetRegularCustomer();

    }
}
