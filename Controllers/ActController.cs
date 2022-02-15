using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IT2166_Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace IT2166_Assignment.Controllers
{
    [Authorize]
    public class Act2Controller : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly AppDBContext _context;
        public Act2Controller(AppDBContext context,IWebHostEnvironment webHost)
        {
            _context = context;
            webHostEnvironment = webHost;

        }
        public IActionResult Notindex(int pg=1)
        {
            const int pageSize = 5;
            if (pg < 1)
                pg = 1;
            int recsCount = _context.Products.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
           

            List<Product> products = _context.Products.Skip(recSkip).Take(pager.Pagesize).ToList();
            this.ViewBag.Pager = pager;
            return View(products);
        }
        public IActionResult Details(string Id)
        {
            Product product = _context.Products.Where(p => p.Id == Id).FirstOrDefault();
            return View(product);
        }
        [HttpGet]
        public IActionResult Edit(string Id)
        {
            Product product = _context.Products.Where(p => p.Id == Id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            string uniqueFileName = Uploadfile(product);
            product.ImageUrl = uniqueFileName;
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Notindex");
        }
        [HttpGet]
        public IActionResult Delete(string Id)
        {
            Product product = _context.Products.Where(p => p.Id == Id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Notindex");
        }
        [HttpGet]
        public IActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        private string Uploadfile(Product product)
        {
            string uniqueFileName = null;
            if (product.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream=new FileStream(filePath, FileMode.Create))
                {
                    product.ImageFile.CopyTo(fileStream);
                }
                
            }
            return uniqueFileName;
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            var productid = _context.Products.Max(PId => PId.Id);
            long productNo;
            Int64.TryParse(productid.ToString() ,out productNo);
            if (productNo > 0)
            {
                productNo = productNo + 1;
              

                
            }
            string uniqueFileName = Uploadfile(product);
            product.ImageUrl = uniqueFileName;
            product.Id = productNo.ToString();
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Added;
            _context.SaveChanges();
            return RedirectToAction("Notindex");
        }

        }
}
