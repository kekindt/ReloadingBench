using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;

namespace ReloadingBench.Pages.Cartridges
{
    public class DeleteModel : PageModel
    {
        ICartridgeRepository cartridgeRepository;

        [BindProperty]
        public string ID { get; set; }

        public DeleteModel(ICartridgeRepository bulletRepo)
        {
            this.cartridgeRepository = bulletRepo;
        }


        [Route("{id}")]
        public IActionResult OnGet(string id)
        {
            ObjectId oId = new ObjectId(id);
            cartridgeRepository.DeleteItem(oId);
            return RedirectToPage("Index");
        }
    }
}