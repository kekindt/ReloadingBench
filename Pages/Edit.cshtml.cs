using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ReloadingBench.Pages
{
    public class EditModel : PageModel
    {
        
        public List<Cartridge> cartridgeTypes { get; set; }

        [BindProperty]
        public Lot NewLot { get; set; }

        ILotRepository lotRepository;
        ICartridgeRepository cartridgeRepository;

        [BindProperty]
        public string ID { get; set; }

        public EditModel(ILotRepository lotRepo, ICartridgeRepository cartridgeRepo)
        {
            lotRepository = lotRepo;
            cartridgeRepository = cartridgeRepo;

            NewLot = new Lot();
        }


        [Route("{id}")]
        public IActionResult OnGet(string id)
        {
            ObjectId oId = new ObjectId(id);
            cartridgeTypes = cartridgeRepository.GetItems(new Dictionary<string, object>());
            NewLot = lotRepository.GetItem(Builders<Lot>.Filter.Eq(b => b.ID, oId));
            NewLot.Cartridge = cartridgeTypes.Where(a => a.ID.ToString() == id).FirstOrDefault();
            return Page();
        }

        [Route("{id}")]
        public IActionResult OnPost(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            NewLot.ID = new ObjectId(id);
            var oId = lotRepository.SaveItem(NewLot);
            return RedirectToPage("Index");
        }
    }
}