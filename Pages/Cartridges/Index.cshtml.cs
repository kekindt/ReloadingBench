using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ReloadingBench.Pages.Cartridges
{
    public class IndexModel : PageModel
    {
        IBulletRepository bulletRepository;
        IPrimerRepository primerRepository;
        IPowderRepository powderRepository;
        ICartridgeRepository cartridgeRepository;

        public List<Bullet> bulletTypes { get; set; }
        public List<Powder> powderTypes { get; set; }
        public List<Primer> primerTypes { get; set; }
        public List<Cartridge> cartridgeTypes { get; set; }

        [BindProperty]
        public Cartridge NewCartridge { get; set; }

        public IndexModel(IBulletRepository bulletRepo, IPrimerRepository primerRepo, IPowderRepository powderRepo, ICartridgeRepository cartridgeRepo)
        {
            bulletRepository = bulletRepo;
            primerRepository = primerRepo;
            powderRepository = powderRepo;
            cartridgeRepository = cartridgeRepo;

            NewCartridge = new Cartridge();
        }

        public void OnGet()
        {
            bulletTypes = bulletRepository.GetItems(new Dictionary<string, object>());
            primerTypes = primerRepository.GetItems(new Dictionary<string, object>());
            powderTypes = powderRepository.GetItems(new Dictionary<string, object>());
            cartridgeTypes = cartridgeRepository.GetItems(new Dictionary<string, object>());
            foreach(var round in cartridgeTypes)
            {
                round.Bullet = bulletTypes.Where(a => a.ID.ToString() == round.BulletId).FirstOrDefault();
                round.Primer = primerTypes.Where(a => a.ID.ToString() == round.PrimerId).FirstOrDefault();
                round.Powder = powderTypes.Where(a => a.ID.ToString() == round.PowderId).FirstOrDefault();
            }
            NewCartridge = new Cartridge();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oId = cartridgeRepository.SaveItem(NewCartridge);

            bulletTypes = bulletRepository.GetItems(new Dictionary<string, object>());
            return RedirectToPage("Index");
        }

        [Route("{id}")]
        public IActionResult OnPostEditLoad(string id)
        {
            ObjectId oId = new ObjectId(id);
            var cartridge = cartridgeRepository.GetItem(Builders<Cartridge>.Filter.Eq(b => b.ID, oId));
            bulletTypes = bulletRepository.GetItems(new Dictionary<string, object>());
            NewCartridge = cartridge;
            RouteData.Values.Clear();
            return Page();
        }

        public IActionResult OnPostEditSave()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oId = cartridgeRepository.SaveItem(NewCartridge);
            return RedirectToPage("Index");
        }
    }
}