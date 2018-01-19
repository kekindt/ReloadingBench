using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ReloadingBench.Pages.Bullets
{
    public class EditModel : PageModel
    {
        IBulletRepository bulletRepository;

        [BindProperty]
        public string ID { get; set; }

        [BindProperty]
        public Bullet Bullet { get; set; }

        public EditModel(IBulletRepository bulletRepo)
        {
            this.bulletRepository = bulletRepo;
        }


        [Route("{id}")]
        public IActionResult OnGet(string id)
        {
            ObjectId oId = new ObjectId(id);
            Bullet = bulletRepository.GetItem(Builders<Bullet>.Filter.Eq(b => b.ID, oId));
            return Page();
        }

        [Route("{id}")]
        public IActionResult OnPost(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Bullet.ID = new ObjectId(id);
            var oId = bulletRepository.SaveItem(Bullet);
            return RedirectToPage("Index");
        }
    }
}