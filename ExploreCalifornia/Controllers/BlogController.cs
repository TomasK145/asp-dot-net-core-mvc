using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogDataContext _db;
        public BlogController(BlogDataContext db)
        {
            _db = db;
        }

        [Route("")] //--> ak nema byt k url pridane nic dalsie mozno pouzit empty string;; aplikovanie route atributu na action odstrani tuto action ako kandidata na matchnutie routes definovanych v Startup
        //ak je definovany route v atribute pre action, jediny sposob ako action vykonat je matchnut definovanu route v atribute
        public IActionResult Index()
        {
            var posts = _db.Posts.OrderByDescending(x => x.Posted).Take(5).ToArray();
            return View(posts);
        }

        [Route("{year:int}/{month:range(1,12)}/{key}")] //--> vyuzitie custom route pre action
        //public IActionResult Post(string id) //parameter ziuskany z query string (?Id=123) alebo Url (.../post/123) --> model binding ;;; mozno vyuzit nullable premenne alebo s default values
        public IActionResult Post(int year, int month, string key)
        {
            var post = _db.Posts.FirstOrDefault(x => x.Key == key);
            return View(post);
        }

        [HttpGet, Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, Route("create")]
        //public IActionResult Create([Bind("Title", "Body")]Post post) //--> Bind definuje ktore properties mozu byt naplnene uzivatelom z formu
        public IActionResult Create(Post post) //mozne namiesto Post pouzit novy objekt (pr. CreatePostRequest), ktory obsahuje len property naplnene vo forme 
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            //popr je mozne overridnut hodnoty pre properties ktore mohol uzivatel zadat, no nemali by byt samotnym uzivatelom menene
            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;

            //EF pracuje na principe Unit of work patternu
            _db.Posts.Add(post); //informuje co je treba urobit --> pridat entitu 
            _db.SaveChanges(); // vykonanie pozadovanych zmien  --> ulozenie do DB

            return RedirectToAction("Post", "Blog", new
            {
                year = post.Posted.Year,
                month = post.Posted.Month,
                key = post.Key
            });
        }
    }
}
