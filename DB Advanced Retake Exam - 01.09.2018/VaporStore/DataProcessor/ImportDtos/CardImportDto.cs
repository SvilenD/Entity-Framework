namespace VaporStore.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    public class CardImportDto
    {
        [Required]
        [RegularExpression(@"^([\d]{4}) ([\d]{4}) ([\d]{4}) ([\d]{4})$")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^([\d]{3})$")]
        public string Cvc { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
