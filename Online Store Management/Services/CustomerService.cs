﻿using Online_Store_Management.Models;
using Online_Store_Management.Interfaces;
using Online_Store_Management.DataAccess;
using Microsoft.EntityFrameworkCore;
namespace Online_Store_Management.Services
{
    public class CustomerService(IRepository<CustomerDbModel> customerRepository) : ICustomer
    {
        public delegate void CustomerUpdateHandler(CustomerDbModel customerUpdate);
        public event CustomerUpdateHandler? CustomerUpdate;
        private readonly IRepository<CustomerDbModel> customerRepository = customerRepository
            ?? throw new ArgumentNullException(nameof(customerRepository));
        private static readonly string[] LastNamesNew = 
        [
            "Snow", "Goth", "White", "Jeffry", "Smith", "Brown"
        ];
        private static readonly int[] PostIndexes = 
        [
            03115, 22567, 89088, 12345, 54321, 65678
        ];
        public async Task<CustomerDbModel?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await customerRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task AddCustomerAsync(CustomerDbModel customer, CancellationToken cancellationToken)
        {
            if (customer.Id < 1 || string.IsNullOrWhiteSpace(customer.LastName) || customer.PostIndex < 1)
            {
                throw new ArgumentException("Invalid customer data.");
            }

            await customerRepository.AddAsync(customer, cancellationToken);
        }

        public async Task UpdateAsync(CustomerDbModel customer, CancellationToken cancellationToken)
        {
            await customerRepository.UpdateAsync(customer, cancellationToken);
            var handler = CustomerUpdate;
            handler?.Invoke(customer);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID provided.");
            }
            await customerRepository.DeleteAsync(id, cancellationToken);
        }

        public IEnumerable<string> GetLastNamesStartingWithS()
        {
            IEnumerable<string> lastNameNewQuery =
                from name in LastNamesNew
                where name.StartsWith('S')
                select name;

            return lastNameNewQuery;
        }

        public IEnumerable<int> GetIndexesDesc()
        {
            IEnumerable<int> postIndexQuery =
                from index in PostIndexes
                orderby index descending
                select index;
            return postIndexQuery;
        }
    }
}
