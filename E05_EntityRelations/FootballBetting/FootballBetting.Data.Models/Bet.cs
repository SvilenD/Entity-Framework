namespace P03_FootballBetting.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static P03_FootballBetting.Data.Models.Configurations.ValidationSettings.Bet;

    public class Bet
    {
        public int BetId { get; set; }

        public decimal Amount { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [MaxLength(PredictionMaxLength)]
        public string Prediction { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}