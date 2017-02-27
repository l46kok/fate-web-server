using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Fate.Common.Data;
using Fate.DB.DAL;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;

namespace Fate.WebServiceLayer
{
    public class LoginSL : IUserMapper
    {
        private static readonly LoginSL _instance = new LoginSL();
        private static readonly LoginDAL _loginDal = LoginDAL.Instance;
        public static LoginSL Instance => _instance;
        private static readonly Dictionary<Guid, WebUserData> _userGuids = new Dictionary<Guid, WebUserData>();

        public LoginSL()
        {
            Console.WriteLine("Login Created");
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

        public bool IsUserAdmin(string accountName)
        {
            return _loginDal.IsUserAdmin(accountName);
        }

        public bool LoginWithCredentials(WebUserData userData, Guid guid)
        {
            if (_loginDal.IsLoginCredentialsCorrect(userData.UserName,
                GetSHA512HashValue(userData.Password + _loginDal.GetSaltForUser(userData.UserName))))
            {
                var existingUserGuid = _userGuids.FirstOrDefault(x => x.Value.UserName == userData.UserName);
                if (existingUserGuid.Key != default(Guid))
                {
                    _userGuids.Remove(existingUserGuid.Key);
                }

                _userGuids.Add(guid, userData);
                return true;
            }
            return false;
        }

        public bool IsAccountNameRegistered(string accountName)
        {
            return _loginDal.IsAccountNameRegistered(accountName);
        }

        public void CreateNewAccount(WebUserData data)
        {
            string salt = GenerateSalt();
            _loginDal.CreateAccount(data.UserName, GetSHA512HashValue(data.Password + salt), salt, data.Email);
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            WebUserData userData;
            if (_userGuids.TryGetValue(identifier, out userData))
            {
                return userData;
            }   
            return null;
        }
    }
}
