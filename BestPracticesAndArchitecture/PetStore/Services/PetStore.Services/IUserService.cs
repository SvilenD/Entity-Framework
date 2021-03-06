﻿namespace PetStore.Services
{
    public interface IUserService
    {
        void Register(string userName, string email);

        int FindUser(string userName);

        bool Exists(int userId);

        bool Exists(string userNameOrEmail);

        void DeleteUser(int userId);
    }
}