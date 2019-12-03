namespace VaporStore.DataProcessor.ImportDtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    using static Data.ModelConstants;

    public class GameImportDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string ReleaseDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Developer { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Genre { get; set; }

        [MinLength(1)]
        public ICollection<string> Tags { get; set; } = new HashSet<string>();
    }
}