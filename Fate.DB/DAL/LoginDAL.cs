using System;
using System.Linq;

namespace Fate.DB.DAL
{
    public class LoginDAL
    {
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

        public void CreateAccount(string accountName, string hashedPassword, string salt)
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
                    Salt = salt
                };
                db.webusers.Add(newUser);
                db.SaveChanges();
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
