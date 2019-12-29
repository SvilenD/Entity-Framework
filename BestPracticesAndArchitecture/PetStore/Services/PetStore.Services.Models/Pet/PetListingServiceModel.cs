﻿namespace PetStore.Services.Models.Pet
{
    public class PetListingServiceModel
    {
        public int Id { get; set; }

        public string Gender { get; set; }

        public string DateOfBirth { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}