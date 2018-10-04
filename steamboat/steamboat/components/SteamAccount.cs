using steamboat.Utils;
using System;
using System.Security;
using SQLite;

namespace steamboat.components
{
    public class SteamAccount
    {
        public string Alias { get; set; }
        [PrimaryKey]
        public string Username { get; set; }

        [Ignore]
        public byte[] Iv { get; set; }
        [Ignore]
        public SecureString SecurePassword { get; set; }

        public string EncodedIv { get; set; }
        public string EncodedPassword { get; set; }

        public SteamAccount() { }

        /// <summary>
        /// Creates a SteamAccount, not specifying an account alias.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public SteamAccount(string username, SecureString password)
        {
            Alias = username;
            Username = username;
            SecurePassword = password;
            EncryptCredentials();
        }

        /// <summary>
        /// Creates a SteamAccount with a specified alias.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public SteamAccount(string name, string username, SecureString password)
        {
            Alias = name;
            Username = username;
            SecurePassword = password;
            EncryptCredentials();
        }

        /// <summary>
        /// Sets up encryption; yields IV and encrypted password.
        /// </summary>
        void EncryptCredentials()
        {
            Iv = Crypto.GetNewEntropy();
            EncodedIv = Convert.ToBase64String(Iv);
            EncodedPassword = Crypto.EncryptString(SecurePassword, Iv);
        }

        /// <summary>
        /// Example of decrypting password.
        /// </summary>
        public string DecryptedPassword
        {
            get
            {
                // use the original iv to decrypt the password
                return Crypto.DecryptString(EncodedPassword, Iv);
            }
        }
    }
}
