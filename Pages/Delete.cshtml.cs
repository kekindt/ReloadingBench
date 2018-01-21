using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;

namespace ReloadingBench.Pages
{
    public class DeleteModel : PageModel
    {
        ILotRepository lotRepository;

        [BindProperty]
        public string ID { get; set; }

        public DeleteModel(ILotRepository bulletRepo)
        {
            this.lotRepository = bulletRepo;
        }


        [Route("{id}")]
        public IActionResult OnGet(string id)
        {
            ObjectId oId = new ObjectId(id);
            lotRepository.DeleteItem(oId);
            return RedirectToPage("Index");
        }
    }
}