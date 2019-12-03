﻿namespace VaporStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VaporStore.Data.Models.Enums;

    using static ModelConstants;

    public class Card
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^([\d]{4}) ([\d]{4}) ([\d]{4}) ([\d]{4})$")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^([\d]{3})$")]
        public string Cvc { get; set; }

        [Required]
        public CardType Type { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Purchase> Purchases { get; set; } = new HashSet<Purchase>();
    }
}