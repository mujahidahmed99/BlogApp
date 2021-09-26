using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BlogController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult MyBlogs()
        {
            var username = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Blog> objList = _db.Blogs.Where(u => u.User.Username == username);
            return View(objList);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
    }
}
