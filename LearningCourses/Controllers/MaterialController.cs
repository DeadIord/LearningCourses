using LearningCourses.Data;
using LearningCourses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace LearningCourses.Controllers
{
    public class MaterialController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaterialController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var materials = await _context.Material.ToListAsync();
            return View(materials);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Material material, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        material.File = memoryStream.ToArray();
                    }
                }

                _context.Material.Add(material);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(material);
        }

        [HttpGet]
        public async Task<IActionResult> Download(int id)
        {
            var material = await _context.Material.FindAsync(id);
            if (material == null || material.File == null)
            {
                return NotFound();
            }

            return File(material.File, "application/pdf", $"material_{id}.pdf");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var material = await _context.Material.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.Material.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            _context.Material.Remove(material);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
