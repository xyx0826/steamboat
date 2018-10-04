using SQLite;
using steamboat.components;
using System;
using System.Collections.Generic;

namespace steamboat.Utils
{
    class Database : IDisposable
    {
        bool _disposed = false;
        SQLiteConnection _dbConn;

        /// <summary>
        /// Initializes database connection and creates account table on first run.
        /// </summary>
        public Database()
        {
            _dbConn = new SQLiteConnection("Steamboat.db");
            _dbConn.CreateTable<SteamAccount>();
        }

        public bool AddAccount(SteamAccount account)
        {
            if (_dbConn.Find<SteamAccount>(account.Username) != null)
            {
                return false;
            }
            else
            {
                _dbConn.Insert(account);
                return true;
            }
        }

        public bool UpdateAccount(SteamAccount account)
        {
            if (_dbConn.Find<SteamAccount>(account.Username) == null)
            {
                return false;
            }
            else
            {
                _dbConn.Update(account);
                return true;
            }
        }

        public List<SteamAccount> GetAllAccounts()
        {
            return _dbConn.Query<SteamAccount>("SELECT * FROM SteamAccount");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _dbConn.Close();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
