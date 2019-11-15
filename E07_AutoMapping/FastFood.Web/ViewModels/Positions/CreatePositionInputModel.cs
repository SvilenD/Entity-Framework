namespace FastFood.Web.ViewModels.Positions
{
    using System.ComponentModel.DataAnnotations;

    public class CreatePositionInputModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Position Length must be between 3 and 30 characters long.")]
        public string PositionName { get; set; }
    }
}