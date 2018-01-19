using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;

namespace ReloadingBench.Pages.Powders
{
    public class DeleteModel : PageModel
    {
        IPowderRepository PowderRepository;

        [BindProperty]
        public string ID { get; set; }

        public DeleteModel(IPowderRepository bulletRepo)
        {
            this.PowderRepository = bulletRepo;
        }


        [Route("{id}")]
        public IActionResult OnGet(string id)
        {
            ObjectId oId = new ObjectId(id);
            PowderRepository.DeleteItem(oId);
            return RedirectToPage("Index");
        }
    }
}