using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ReloadingBench.Pages.Primers
{
    public class IndexModel : PageModel
    {
        IPrimerRepository primerRepository;
        public List<Primer> primerTypes { get; set; }

        [BindProperty]
        public Primer NewPrimer { get; set; }

        public IndexModel(IPrimerRepository primerRepo)
        {
            this.primerRepository = primerRepo;
            NewPrimer = new Primer();
        }

        public void OnGet()
        {
            primerTypes = primerRepository.GetItems(new Dictionary<string, object>());
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oId = primerRepository.SaveItem(NewPrimer);

            primerTypes = primerRepository.GetItems(new Dictionary<string, object>());
            return RedirectToPage("Index");
        }

        [Route("{id}")]
        public IActionResult OnPostEditLoad(string id)
        {
            ObjectId oId = new ObjectId(id);
            var primer = primerRepository.GetItem(Builders<Primer>.Filter.Eq(b => b.ID, oId));
            primerTypes = primerRepository.GetItems(new Dictionary<string, object>());
            NewPrimer = primer;
            RouteData.Values.Clear();
            return Page();
        }

        public IActionResult OnPostEditSave()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oId = primerRepository.SaveItem(NewPrimer);
            return RedirectToPage("Index");
        }
    }
}