using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;

namespace ReloadingBench.Pages
{
    public class ReloadingLogModel : PageModel
    {
        public List<Lot> lots { get; set; }
        public List<Cartridge> cartridgeTypes { get; set; }

        [BindProperty]
        public Lot NewLot { get; set; }

        ILotRepository lotRepository;
        ICartridgeRepository cartridgeRepository;

        public ReloadingLogModel(ILotRepository LotRepo, ICartridgeRepository cartridgeRepo)
        {
            lotRepository = LotRepo;
            cartridgeRepository = cartridgeRepo;
        }

        public void OnGet()
        {
            lots = lotRepository.GetItems(new Dictionary<string, object>());
            cartridgeTypes = cartridgeRepository.GetItems(new Dictionary<string, object>());
            foreach(var lot in lots)
            {
                lot.Cartridge = cartridgeTypes.Where(a => a.ID.ToString() == lot.CartridgeId).FirstOrDefault();
            }
            NewLot = new Lot();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oId = lotRepository.SaveItem(NewLot);

            return RedirectToPage("/Index");
        }
    }
}