namespace PetStore.Services.Implementations
{
    using System;
    using System.Linq;
    using PetStore.Data;
    using PetStore.Data.Models;

    using static Validator;

    public class UserService : IUserService
    {
        private readonly PetStoreDbContext data;

        public UserService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public void DeleteUser(int userId)
        {
            if (this.Exists(userId))
            {
                this.data.Users.FirstOrDefault(u => u.Id == userId).IsDeleted = true;
                this.data.SaveChanges();
            }
        }

        public bool Exists(int userId) 
            => this.data.Users
            .Where(u=>u.IsDeleted == false)
            .Any(u => u.Id == userId);

        public bool Exists(string userNameOrEmail)
            => this.data.Users
            .Any(u => u.UserName == userNameOrEmail || u.Email == userNameOrEmail);

        public int FindUser(string userName) 
            => this.data.Users
            .FirstOrDefault(u => u.UserName == userName).Id;

        public void Register(string userName, string email)
        {
            if (this.Exists(userName)) 
            {
                throw new InvalidOperationException(String.Format(OutputMessages.UserNameAlreadyExists, userName));
            }
            else if (this.Exists(email))
            {
                throw new InvalidOperationException(String.Format(OutputMessages.UserEmailAlreadyExists, email));
            }

            var newUser = new User()
            {
                UserName = userName,
                Email = email
            };

            if (IsValid(newUser) == false)
            {
                throw new InvalidOperationException(OutputMessages.InvalidUser);
            }

            this.data.Users.Add(newUser);
            this.data.SaveChanges();
        }
    }
}