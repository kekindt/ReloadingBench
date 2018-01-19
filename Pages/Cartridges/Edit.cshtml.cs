using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ReloadingBench.Pages.Cartridges
{
    public class EditModel : PageModel
    {
        IBulletRepository bulletRepository;
        IPrimerRepository primerRepository;
        IPowderRepository powderRepository;
        ICartridgeRepository cartridgeRepository;

        public List<Bullet> bulletTypes { get; set; }
        public List<Powder> powderTypes { get; set; }
        public List<Primer> primerTypes { get; set; }

        [BindProperty]
        public string ID { get; set; }

        [BindProperty]
        public Cartridge NewCartridge { get; set; }

        public EditModel(IBulletRepository bulletRepo, IPrimerRepository primerRepo, IPowderRepository powderRepo, ICartridgeRepository cartridgeRepo)
        {
            bulletRepository = bulletRepo;
            primerRepository = primerRepo;
            powderRepository = powderRepo;
            cartridgeRepository = cartridgeRepo;

            NewCartridge = new Cartridge();
        }


        [Route("{id}")]
        public IActionResult OnGet(string id)
        {
            ObjectId oId = new ObjectId(id);
            NewCartridge = cartridgeRepository.GetItem(Builders<Cartridge>.Filter.Eq(b => b.ID, oId));
            bulletTypes = bulletRepository.GetItems(new Dictionary<string, object>());
            primerTypes = primerRepository.GetItems(new Dictionary<string, object>());
            powderTypes = powderRepository.GetItems(new Dictionary<string, object>());
            NewCartridge.Bullet = bulletTypes.Where(a => a.ID.ToString() == NewCartridge.BulletId).FirstOrDefault();
            NewCartridge.Primer = primerTypes.Where(a => a.ID.ToString() == NewCartridge.PrimerId).FirstOrDefault();
            NewCartridge.Powder = powderTypes.Where(a => a.ID.ToString() == NewCartridge.PowderId).FirstOrDefault();
            return Page();
        }

        [Route("{id}")]
        public IActionResult OnPost(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            NewCartridge.ID = new ObjectId(id);
            var oId = cartridgeRepository.SaveItem(NewCartridge);
            return RedirectToPage("Index");
        }
    }
}