using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Fate.DB.DAL;

namespace Fate.WebServiceLayer
{
    public class LoginSL
    {
        private static readonly LoginSL _instance = new LoginSL();
        private static readonly LoginDAL _loginDal= new LoginDAL();
        public static LoginSL Instance => _instance;

        private LoginSL()
        {
            
        }

        private string GetSHA512HashValue(string val)
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                byte[] hashedValue = shaM.ComputeHash(Encoding.UTF8.GetBytes(val));
                return Encoding.UTF8.GetString(hashedValue);
            }
        }

        private string GenerateSalt()
        {
            using (RNGCryptoServiceProvider rngSaltProvider = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[64];
                rngSaltProvider.GetNonZeroBytes(saltBytes);
                return Encoding.UTF8.GetString(saltBytes);
            }
        }

        public bool LoginWithCredentials(string accountName, string password)
        {
            return _loginDal.IsLoginCredentialsCorrect(accountName, GetSHA512HashValue(password + _loginDal.GetSaltForUser(accountName)));
        }

        public bool IsAccountNameRegistered(string accountName)
        {
            return _loginDal.IsAccountNameRegistered(accountName);
        }

        public void CreateNewAccount(string accountName, string password)
        {
            string salt = GenerateSalt();
            _loginDal.CreateAccount(accountName, GetSHA512HashValue(password + salt), salt);
        }
    }
}
