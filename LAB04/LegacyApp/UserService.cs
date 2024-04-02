using System;
using System.Text.RegularExpressions;

namespace LegacyApp
{
    public class UserService
    {

        public bool IsBasicInfoNull(string firstName, string lastName)
        {
            return string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName);
        }
        public bool IsEmailValid(string email)
        {
            return email.Contains("@") || email.Contains(".");
        }

        public void ApplyCreditLimit(User user, Client client)
        {
            if (client.Type == "VeryImportantClient") user.HasCreditLimit = false;
            else
            {
                bool bClientType = (client.Type == "ImportantClient");
                user.HasCreditLimit = bClientType ? user.HasCreditLimit : true;
                using (UserCreditService userCreditService = new UserCreditService())
                {
                    user.CreditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth) * (bClientType ? 2 : 1);
                }
            }
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (IsBasicInfoNull(firstName, lastName)) return false;
            
            if (!IsEmailValid(email)) return false;
            
            DateTime now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            
            if (age < 21) return false;
            
            ClientRepository clientRepository = new ClientRepository();
            Client client = clientRepository.GetById(clientId);

            User user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            ApplyCreditLimit(user, client);

            if (user.HasCreditLimit && user.CreditLimit < 500) return false;
            
            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
