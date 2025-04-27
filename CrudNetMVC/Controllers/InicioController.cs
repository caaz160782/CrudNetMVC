using System.Diagnostics;
using CrudNetMVC.Data;
using CrudNetMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudNetMVC.Controllers
{
    public class InicioController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InicioController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contacto.ToListAsync());
        }
        
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                contacto.FechaCreacion = DateTime.Now;
                _context.Contacto.Add(contacto);
                await  _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var contacto = _context.Contacto.FirstOrDefault(c => c.Id == id);
            if (contacto == null)
            {
                return NotFound();
            }
            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                var contactoExistente = _context.Contacto.FirstOrDefault(c => c.Id == contacto.Id);
                if (contactoExistente == null)
                {
                    return NotFound();
                }

                // Actualizamos los campos
                contactoExistente.Nombre = contacto.Nombre;
                contactoExistente.Telefono = contacto.Telefono;
                contactoExistente.Celular = contacto.Celular;
                contactoExistente.Email = contacto.Email;

                _context.Update(contactoExistente);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Inicio"); // Regresa a la lista principal
            }

            return View(contacto); // Si hay error, regresa a la vista
        }

        // GET: Mostrar vista para confirmar eliminación
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var contacto = _context.Contacto.FirstOrDefault(c => c.Id == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // POST: Confirmar y eliminar
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarConfirmado(int id)
        {
            var contacto = _context.Contacto.FirstOrDefault(c => c.Id == id);
            if (contacto == null)
            {
                return NotFound();
            }

            _context.Contacto.Remove(contacto);
            _context.SaveChanges();

            return RedirectToAction("Index", "Inicio");
        }

        [HttpGet]
        public IActionResult Detalle(int id)
        {
            var contacto = _context.Contacto.FirstOrDefault(c => c.Id == id);
            if (contacto == null)
            {
                return NotFound();
            }
            return View(contacto);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
