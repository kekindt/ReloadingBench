using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;

namespace ReloadingBench.Pages.Bullets
{
    public class DeleteModel : PageModel
    {
        IBulletRepository bulletRepository;

        [BindProperty]
        public string ID { get; set; }

        public DeleteModel(IBulletRepository bulletRepo)
        {
            this.bulletRepository = bulletRepo;
        }


        [Route("{id}")]
        public IActionResult OnGet(string id)
        {
            ObjectId oId = new ObjectId(id);
            bulletRepository.DeleteItem(oId);
            return RedirectToPage("Index");
        }
    }
}