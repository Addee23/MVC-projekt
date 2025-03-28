using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages; //för PageModel
using Northwind.EntityModels; 

namespace Northwind.Web.Pages
{
    public class SuppliersModel : PageModel
    {
        private NorthwindDatabaseContext db;
        public SuppliersModel(NorthwindDatabaseContext injectedContext)
        {
            db = injectedContext;
        }
        public IEnumerable<Supplier>? Suppliers { get; set; }

        [BindProperty]
        public Supplier? Supplier { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Northwind App - Suppliers";

            Suppliers = db.Suppliers.OrderBy(c => c.Country)
                .ThenBy(c => c.CompanyName);
        }

        public IActionResult OnPost()
        {
            if ((Supplier is not null) && ModelState.IsValid)
            {
                db.Suppliers.Add(Supplier);
                db.SaveChanges();
                return RedirectToPage("/suppliers");
            }
            else
            {
                return Page(); //returnerar ursprungliga sidan
            }

        }
    }
}
