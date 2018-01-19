using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;

namespace ReloadingBench.Pages.Primers
{
    public class DeleteModel : PageModel
    {
        IPrimerRepository primerRepository;

        [BindProperty]
        public string ID { get; set; }

        public DeleteModel(IPrimerRepository bulletRepo)
        {
            this.primerRepository = bulletRepo;
        }


        [Route("{id}")]
        public IActionResult OnGet(string id)
        {
            ObjectId oId = new ObjectId(id);
            primerRepository.DeleteItem(oId);
            return RedirectToPage("Index");
        }
    }
}