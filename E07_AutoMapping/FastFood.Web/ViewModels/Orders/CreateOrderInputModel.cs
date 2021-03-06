﻿namespace FastFood.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    public class CreateOrderInputModel
    {
        [Required]
        [MinLength(3), MaxLength(50)]
        public string Customer { get; set; }

        public int ItemId { get; set; }

        public int EmployeeId { get; set; }

        [Range(1, 999)]
        public int Quantity { get; set; }

        public string OrderType { get; set; }
    }
}