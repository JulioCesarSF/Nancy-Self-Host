using NancyHostMySql.Log;
using System;
using System.Security.Cryptography;
using System.Text;

namespace NancyHostMySql.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private string password;
        public string Password
        {
            get => password;
            set => password = SetPassword(value);
        }

        private string SetPassword(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                using (var sha256 = new SHA256Managed())
                {
                    var textArrayBytes = Encoding.UTF8.GetBytes(password);
                    var hashCompute = sha256.ComputeHash(textArrayBytes);
                    return BitConverter.ToString(hashCompute).Replace("-", String.Empty);
                }
            }
            throw new ArgumentNullException(LogMsgs.EmptyPassword);
        }

        private void BuildHash()
        {
            using (var sha256 = new SHA256Managed())
            {
                var textArrayBytes = Encoding.UTF8.GetBytes(Password);
                var hashCompute = sha256.ComputeHash(textArrayBytes);
                Password = BitConverter.ToString(hashCompute).Replace("-", String.Empty);
            }
        }
    }
}
