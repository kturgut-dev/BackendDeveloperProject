using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackendDeveloperProject.Core.Helpers.Security
{
    public class SecurePasswordHasher
    {    /// </summary>
        private const int SaltSize = 16;
        public string UserSalt { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Size of hash.
        /// </summary>
        private const int HashSize = 20;

        /// <summary>
        /// Creates a hash from a password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="iterations">Number of iterations.</param>
        /// <returns>The hash.</returns>
        public string Hash(string password, int iterations)
        {
            // Create salt
            byte[] salt;
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // Create hash
            var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            // Combine salt and hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            // Format hash with extra information
            //return string.Format("{0}${1}", iterations, base64Hash);
            return string.Format("{0}{1}${2}", UserSalt.ToString(), iterations, base64Hash);
        }

        /// <summary>
        /// Creates a hash from a password with 10000 iterations
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>The hash.</returns>
        public string Hash(string password)
        {
            Random _random = new Random();
            return Hash(ComputeSha256Hash(password), _random.Next(1500));
        }

        /// <summary>
        /// Checks if hash is supported.
        /// </summary>
        /// <param name="hashString">The hash.</param>
        /// <returns>Is supported?</returns>
        public bool IsHashSupported(string hashString)
        {
            return hashString.Contains(UserSalt);
        }

        /// <summary>
        /// Verifies a password against a hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hash.</param>
        /// <returns>Could be verified?</returns>
        public bool Verify(string password, string hashedPassword)
        {
            password = ComputeSha256Hash(password);
            // Check hash
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }

            // Extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace(UserSalt, "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Create hash with given salt
            var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Get result
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
