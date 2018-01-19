using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ReloadingBench.Pages.Powders
{
    public class EditModel : PageModel
    {
        IPowderRepository PowderRepository;

        [BindProperty]
        public string ID { get; set; }

        [BindProperty]
        public Powder Powder { get; set; }

        public EditModel(IPowderRepository bulletRepo)
        {
            this.PowderRepository = bulletRepo;
        }


        [Route("{id}")]
        public IActionResult OnGet(string id)
        {
            ObjectId oId = new ObjectId(id);
            Powder = PowderRepository.GetItem(Builders<Powder>.Filter.Eq(b => b.ID, oId));
            return Page();
        }

        [Route("{id}")]
        public IActionResult OnPost(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Powder.ID = new ObjectId(id);
            var oId = PowderRepository.SaveItem(Powder);
            return RedirectToPage("Index");
        }
    }
}