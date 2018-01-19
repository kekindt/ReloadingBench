using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ReloadingBench.Pages.Primers
{
    public class EditModel : PageModel
    {
        IPrimerRepository primerRepository;

        [BindProperty]
        public string ID { get; set; }

        [BindProperty]
        public Primer Primer { get; set; }

        public EditModel(IPrimerRepository bulletRepo)
        {
            this.primerRepository = bulletRepo;
        }


        [Route("{id}")]
        public IActionResult OnGet(string id)
        {
            ObjectId oId = new ObjectId(id);
            Primer = primerRepository.GetItem(Builders<Primer>.Filter.Eq(b => b.ID, oId));
            return Page();
        }

        [Route("{id}")]
        public IActionResult OnPost(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Primer.ID = new ObjectId(id);
            var oId = primerRepository.SaveItem(Primer);
            return RedirectToPage("Index");
        }
    }
}