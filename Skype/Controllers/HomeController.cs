﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Skype.Models;

namespace Skype.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {

       // SkypeContext context = new SkypeContext();

        User ivan = new User { NickName = "Ivan", Password = "sdfg" };

        IHostingEnvironment _env;

        private readonly SkypeContext db;

        public HomeController(IHostingEnvironment env, SkypeContext context)
        {
            _env = env;
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [EnableCors("AllowAllOrigin")]
        [HttpPost]
        public IActionResult PostUser([FromBody]User user)
        {
           
           // return new PhysicalFileResult(Path.Combine(env.WebRootPath, "index.html"), "text/html");

            var checkResult = CheckUser(user);

            return Json(true);           

        }


        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public bool CheckUser(User user)
        {
            
           if (user.Equals(ivan))
            {
                return true;
            }
            else return false;
        }
    }
    
    
}