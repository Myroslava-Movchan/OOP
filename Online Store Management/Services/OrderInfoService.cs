using Online_Store_Management.Models;
using System.Collections;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Extensions;
using System.Text;
using System.Security.Cryptography;
namespace Online_Store_Management.Services
{
    public class OrderInfoService : IOrderInfo
    {
        private readonly ArrayList orders = new ArrayList(100);
        private HashSet<OrderInfo> orderTable = new HashSet<OrderInfo>();
        private readonly IRepository<OrderInfo> orderRepository;
        private readonly IConfiguration _configuration;
        private FileStream _transactionLogFileStream;
        public Func<OrderInfo, decimal>? CalculateTotal { get; set; }
        private static readonly string[] Gifts =
        [
            "Pin", "Sticker",
            "Candy", "Bracelet",
            "Hairclip", "Socks"
        ];

        public OrderInfoService(IRepository<OrderInfo> orderRepository, IConfiguration configuration)
        {
            this.orderRepository = orderRepository;
            this._configuration = configuration;
        }
        public static RSA ImportPublicKey(string publicKeyPem)
        {
            string keyBase64 = publicKeyPem
                .Replace("-----BEGIN PUBLIC KEY-----", "")
                .Replace("-----END PUBLIC KEY-----", "")
                .Replace("\n", "")
                .Replace("\r", "");

            byte[] keyBytes = Convert.FromBase64String(keyBase64);

            RSA rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(keyBytes, out _);
            return rsa;
        }
        public void SetOrderLogFileStream(FileStream orderLogFileStream)
        {
            this._transactionLogFileStream = orderLogFileStream;
        }
        public async Task LogActionAsync(OrderInfo order, string message, CancellationToken cancellationToken)
        {
            string encryptedMessage = EncryptMessage($"{DateTime.UtcNow}: {message}");
            byte[] messageBytes = Encoding.UTF8.GetBytes(encryptedMessage + "\n");
            await _transactionLogFileStream.WriteAsync(messageBytes, 0, messageBytes.Length);
            await Infrastructure.Logger.LogToConsole(order, cancellationToken);
        }
        public string DecryptMessage(string encryptedMessage)
        {
            string privateKey = _configuration["Encryption:PrivateKey"];
            if (string.IsNullOrEmpty(privateKey))
                throw new InvalidOperationException("Private key was not found.");

            using (RSA rsa = RSA.Create())
            {
                rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
                byte[] decryptedBytes = rsa.Decrypt(Convert.FromBase64String(encryptedMessage), RSAEncryptionPadding.Pkcs1);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
        private string EncryptMessage(string message)
        {
            string publicKey = _configuration["Encryption:PublicKey"];
            if (string.IsNullOrEmpty(publicKey))
                throw new InvalidOperationException("Public key was not found.");

            try
            {
                using (RSA rsa = RSA.Create())
                {
                    rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);

                    byte[] encryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(message), RSAEncryptionPadding.Pkcs1);

                    return Convert.ToBase64String(encryptedBytes);
                }
            }
            catch (CryptographicException ex)
            {
                throw new InvalidOperationException("Error occurred while encrypting the message.", ex);
            }
            catch (FormatException ex)
            {
                throw new InvalidOperationException("Public key format is invalid.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while encrypting the message.", ex);
            }
        }

        public async Task<OrderInfo> PostAsync(Product product, CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);
            var gifts = Gifts[Random.Shared.Next(Gifts.Length)];
            var orderInfo = new OrderInfo()
            {
                OrderNumber = Random.Shared.Next(1, 250),
                Gift = gifts,
                Product = product,
                Status = "Processing"
            };

            var delivery = OrderInfoExtension.EstimateDeliveryAsync(product, cancellationToken);
            object objOrder = orderInfo;
            orders.Add(objOrder);
            OrderInfo order = (OrderInfo)objOrder;
            return order;
        }

        public async Task<bool> CompareOrdersAsync(OrderInfo order, CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);
            bool compare = false;
            foreach (var existingOrder in orders)
            {
                if (order.Equals(existingOrder))
                {
                    compare = true;
                    break;
                }
            }
            return compare;
        }

        public async Task<bool> AddToTableAsync(OrderInfo order, CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);
            var orderHashCode = order.GetHashCode();
            foreach (var existingOrder in orders)
            {
                if (existingOrder.Equals(order))
                {
                    return false;
                }
            }
            orderTable.Add(order);
            return true;
        }
        public async Task<int> EstimateDeliveryAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);
            int delieverySum = 120;
            return delieverySum;
        }

        public decimal GetTotal(OrderInfo order)
        {
            return CalculateTotal(order);
        }

        public async Task<OrderInfo?> GetOrderByIdAsync(int orderNumber, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetByIdAsync(orderNumber, cancellationToken);
            if (order != null && order.Product == null)
            {
                order.Product = new Product();
            }
            return order;
        }

        public async Task AddOrderAsync(OrderInfo order, CancellationToken cancellationToken)
        {
            await orderRepository.AddAsync(order, cancellationToken);
            string message = $"Order with number {order.OrderNumber} was added.";
            await LogActionAsync(new OrderInfo { OrderNumber = order.OrderNumber, Product = order.Product, Status = order.Status }, message, cancellationToken);
        }

        public async Task UpdateAsync(OrderInfo order, CancellationToken cancellationToken)
        {
            await orderRepository.UpdateAsync(order, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await orderRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<OrderInfo>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await orderRepository.GetAllAsync(cancellationToken);
        }
    }
}
