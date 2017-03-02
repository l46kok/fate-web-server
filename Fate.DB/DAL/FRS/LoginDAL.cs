using System;
using System.Linq;

namespace Fate.DB.DAL.FRS
{
    public class LoginDAL
    {
        public static LoginDAL Instance { get; } = new LoginDAL();

        public bool IsAccountNameRegistered(string accountName)
        {
            using (var db = frsDatabase.Create())
            {
                return db.webusers.Any(x => x.UserName.Equals(accountName, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public string GetSaltForUser(string accountName)
        {
            using (var db = frsDatabase.Create())
            {
                return
                    db.webusers.Single(x => x.UserName.Equals(accountName, StringComparison.InvariantCultureIgnoreCase))
                        .Salt;
            }
        }

        public void CreateAccount(string accountName, string hashedPassword, string salt, string emailAddress)
        {
            using (var db = frsDatabase.Create())
            {
                webusers newUser = new webusers
                {
                    UserName = accountName,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    IsAdmin = false,
                    Password = hashedPassword,
                    Salt = salt,
                    EmailAddress = emailAddress
                };
                db.webusers.Add(newUser);
                db.SaveChanges();
            }
        }

        public bool IsUserAdmin(string accountName)
        {
            using (var db = frsDatabase.Create())
            {
                return
                    db.webusers.Single(x => x.UserName.Equals(accountName, StringComparison.InvariantCultureIgnoreCase))
                        .IsAdmin;
            }
        }

        public bool IsLoginCredentialsCorrect(string accountName, string hashedPassword)
        {
            using (var db = frsDatabase.Create())
            {
                return
                    db.webusers.Any(
                        x =>
                            x.UserName.Equals(accountName, StringComparison.InvariantCultureIgnoreCase) &&
                            x.Password == hashedPassword);
            }
        }
    }
}
