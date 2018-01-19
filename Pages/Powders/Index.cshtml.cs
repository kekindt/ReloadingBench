using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ReloadingBench.Pages.Powders
{
    public class IndexModel : PageModel
    {
        IPowderRepository PowderRepository;
        public List<Powder> PowderTypes { get; set; }

        [BindProperty]
        public Powder NewPowder { get; set; }

        public IndexModel(IPowderRepository PowderRepo)
        {
            this.PowderRepository = PowderRepo;
            NewPowder = new Powder();
        }

        public void OnGet()
        {
            PowderTypes = PowderRepository.GetItems(new Dictionary<string, object>());
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oId = PowderRepository.SaveItem(NewPowder);

            PowderTypes = PowderRepository.GetItems(new Dictionary<string, object>());
            return RedirectToPage("Index");
        }

        [Route("{id}")]
        public IActionResult OnPostEditLoad(string id)
        {
            ObjectId oId = new ObjectId(id);
            var Powder = PowderRepository.GetItem(Builders<Powder>.Filter.Eq(b => b.ID, oId));
            PowderTypes = PowderRepository.GetItems(new Dictionary<string, object>());
            NewPowder = Powder;
            RouteData.Values.Clear();
            return Page();
        }

        public IActionResult OnPostEditSave()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oId = PowderRepository.SaveItem(NewPowder);
            return RedirectToPage("Index");
        }
    }
}