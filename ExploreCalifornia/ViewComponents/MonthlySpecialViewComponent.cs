using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.ViewComponents
{
    //rozpoznanie ze sa jena o view component
    //nazov triedy musi koncit na ViewComponent
    //alebo musi mat trieda atribut "[Microsoft.AspNetCore.Mvc.ViewComponent]"
    //alebo musi trieda dedit z triedy "ViewComponent"
    //[Microsoft.AspNetCore.Mvc.ViewComponent]
    public class MonthlySpecialViewComponent : ViewComponent
    {
        private readonly BlogDataContext _db;
        public MonthlySpecialViewComponent(BlogDataContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke() //musi existovat metoda Invoke()
        {
            var specials = _db.MonthlySpecials.ToArray();
            return View(specials);
        }
    }
}
