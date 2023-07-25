using System;
using System.Security.Cryptography;
using System.Text;

namespace PaymentSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order(123, 12000);

            IPaymentSystem system1 = new PaymentSystem1();
            string link1 = system1.GetPayingLink(order);
            Console.WriteLine(link1);

            IPaymentSystem system2 = new PaymentSystem2();
            string link2 = system2.GetPayingLink(order);
            Console.WriteLine(link2);

            IPaymentSystem system3 = new PaymentSystem3();
            string link3 = system3.GetPayingLink(order);
            Console.WriteLine(link3);
        }
    }

    public class Order
    {
        public readonly int Id;
        public readonly int Amount;

        public Order(int id, int amount) => (Id, Amount) = (id, amount);
    }

    public interface IPaymentSystem
    {
        public string GetPayingLink(Order order);
    }

    public class PaymentSystem1 : IPaymentSystem
    {
        public string GetPayingLink(Order order)
        {
            string hash = CalculateMD5Hash(order.Id.ToString());
            return $"pay.system1.ru/order?amount={order.Amount}RUB&hash={hash}";
        }

        private string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }

    public class PaymentSystem2 : IPaymentSystem
    {
        public string GetPayingLink(Order order)
        {
            string hash = CalculateMD5Hash(order.Id.ToString() + order.Amount.ToString());
            return $"order.system2.ru/pay?hash={hash}";
        }

        private string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }

    public class PaymentSystem3 : IPaymentSystem
    {
        public string GetPayingLink(Order order)
        {
            string hash = CalculateSHA1Hash(order.Amount.ToString() + order.Id.ToString() + "secretKey");
            return $"system3.com/pay?amount={order.Amount}&curency=RUB&hash={hash}";
        }

        private string CalculateSHA1Hash(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}