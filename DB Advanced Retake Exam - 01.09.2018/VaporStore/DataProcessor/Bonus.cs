namespace VaporStore.DataProcessor
{
    using System;
    using System.Linq;

    using Data;
    using static OutputConstants;

	public static class Bonus
	{
		public static string UpdateEmail(VaporStoreDbContext context, string username, string newEmail)
		{
            var users = context.Users.ToHashSet();
            var user = users
                .FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return String.Format(UserNotFoundMsg, username);
            }
            else if (users.Any(u=>u.Email == newEmail))
            {
                return String.Format(EmailIsTakenMsg, newEmail); 
            }
            else
            {
                user.Email = newEmail;
                context.SaveChanges();
                return String.Format(EmailChangedSuccessfully, username); 
            }
		}
	}
}
