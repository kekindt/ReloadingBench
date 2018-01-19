using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ReloadingBench.Pages.Bullets
{
    public class IndexModel : PageModel
    {
        IBulletRepository bulletRepository;
        public List<Bullet> bulletTypes { get; set; }

        [BindProperty]
        public Bullet NewBullet { get; set; }

        public IndexModel(IBulletRepository bulletRepo)
        {
            this.bulletRepository = bulletRepo;
            NewBullet = new Bullet();
        }

        public void OnGet()
        {
            bulletTypes = bulletRepository.GetItems(new Dictionary<string, object>());
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oId = bulletRepository.SaveItem(NewBullet);

            bulletTypes = bulletRepository.GetItems(new Dictionary<string, object>());
            return RedirectToPage("Index");
        }

        [Route("{id}")]
        public IActionResult OnPostEditLoad(string id)
        {
            ObjectId oId = new ObjectId(id);
            var bullet = bulletRepository.GetItem(Builders<Bullet>.Filter.Eq(b => b.ID, oId));
            bulletTypes = bulletRepository.GetItems(new Dictionary<string, object>());
            NewBullet = bullet;
            RouteData.Values.Clear();
            return Page();
        }

        public IActionResult OnPostEditSave()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oId = bulletRepository.SaveItem(NewBullet);
            return RedirectToPage("Index");
        }
    }
}