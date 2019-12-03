namespace VaporStore.DataProcessor
{
	using System;
    using System.Linq;
    using Data;

	public static class Bonus
	{
		public static string UpdateEmail(VaporStoreDbContext context, string username, string newEmail)
		{
            var users = context.Users.ToHashSet();
            var user = users
                .FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return $"User {username} not found";
            }
            else if (users.Any(u=>u.Email == newEmail))
            {
                return $"Email {newEmail} is already taken";
            }
            else
            {
                user.Email = newEmail;
                context.SaveChanges();
                return $"Changed {username}'s email successfully";
            }
		}
	}
}
