namespace PetStore.Web.Models.Pet
{
    using System;
    using System.Collections.Generic;
    using PetStore.Services.Models.Pet;

    public class AllPetsViewModel
    {
        private const int PetsPerPage = 20;

        public AllPetsViewModel(int page = 1)
        {
            this.CurrentPage = page;
        }
        public IEnumerable<PetListingServiceModel> Pets { get; set; }

        public int Total { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage - 1;

        public int NextPage => this.CurrentPage + 1;

        public bool PreviousDisabled => this.CurrentPage == 1;

        public bool NextDisabled
        {
            get
            {
                var maxPage = Math.Ceiling((double)this.Total / PetsPerPage);

                return maxPage == this.CurrentPage;
            }
        }
    }
}