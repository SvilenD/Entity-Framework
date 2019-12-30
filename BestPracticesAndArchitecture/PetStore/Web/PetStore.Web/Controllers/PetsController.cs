namespace PetStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PetStore.Services;
    using PetStore.Web.Models.Pet;

    public class PetsController : Controller
    {
        private readonly IPetService pets;

        public PetsController(IPetService pets)
        {
            this.pets = pets;
        }

        // /Pets/All
        public IActionResult All(int page = 1)
        {
            var allPets = this.pets.All(page);

            var model = new AllPetsViewModel(page)
            {
                Pets = allPets,
                CurrentPage = page,
                Total = this.pets.Total(),
            };

            return View(model);
        }
    }
}