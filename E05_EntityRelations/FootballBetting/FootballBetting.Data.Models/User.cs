namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static P03_FootballBetting.Data.Models.Configurations.ValidationSettings.User;

    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(User_Pass_EmailMaxLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength(User_Pass_EmailMaxLength)]
        public string Password { get; set; }

        [Required]
        [MaxLength(User_Pass_EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public ICollection<Bet> Bets { get; set; } 
            = new HashSet<Bet>();
    }
}