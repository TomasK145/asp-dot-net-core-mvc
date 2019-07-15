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
        [Route("")] //--> ak nema byt k url pridane nic dalsie mozno pouzit empty string;; aplikovanie route atributu na action odstrani tuto action ako kandidata na matchnutie routes definovanych v Startup
        //ak je definovany route v atribute pre action, jediny sposob ako action vykonat je matchnut definovanu route v atribute
        public IActionResult Index()
        {
            var posts = new[]
            {
                new Post
                {
                    Title = "My blog post",
                    Posted = DateTime.Now,
                    Author = "Jess Chadwick",
                    Body = "This is a great blog post, don't you think?",
                },
                new Post
                {
                    Title = "My second blog post",
                    Posted = DateTime.Now,
                    Author = "Jess Chadwick",
                    Body = "This is ANOTHER great blog post, don't you think?",
                },
            };
            return View(posts);
        }

        [Route("{year:int}/{month:range(1,12)}/{key}")] //--> vyuzitie custom route pre action
        //public IActionResult Post(string id) //parameter ziuskany z query string (?Id=123) alebo Url (.../post/123) --> model binding ;;; mozno vyuzit nullable premenne alebo s default values
        public IActionResult Post(int year, int month, string key)
        {
            var post = new Post
            {
                Title = "My blog post",
                Posted = DateTime.Now,
                Author = "TK",
                Body = "This is a great blog post, don't you think?"
            };
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

            return View();
        }
    }
}
