#nullable disable
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Final.Models;

namespace Projeto_Final.Controllers
{
    public class ConsumidorController : Controller
    {
        private readonly MyDbContext _context;
        private readonly ILogger<HomeController> _logger;
        [TempData]
        public string UserName { get; set; }

        public ConsumidorController(MyDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Consumidor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consumidor.ToListAsync());
        }

        // GET: Consumidor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            id = HttpContext.Session.GetInt32("UserId");
            if (id == null)
            {
                return RedirectToAction("Login", "Consumidor");
            }

            var consumidor = await _context.Consumidor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumidor == null)
            {
                return NotFound();
            }

            return View(consumidor);
        }

        // GET: Consumidor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consumidor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Senha")] Consumidor consumidor)
        {
            consumidor.Carrinho = new Carrinho();
            if (ModelState.IsValid)
            {
                _context.Add(consumidor);
                await _context.SaveChangesAsync();
                var consumidorPosCreate = await _context.Consumidor.FirstOrDefaultAsync(
                    m => m.Email == consumidor.Email
                );
                var carrinho = await _context.Carrinho.FirstOrDefaultAsync(
                    m => m.ConsumidorId == consumidorPosCreate.Id
                );
                HttpContext.Session.SetString("Email", consumidorPosCreate.Email);
                HttpContext.Session.SetInt32("UserId", consumidorPosCreate.Id);
                HttpContext.Session.SetInt32("CartId", carrinho.Id);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Consumidor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumidor = await _context.Consumidor.FindAsync(id);
            if (consumidor == null)
            {
                return NotFound();
            }
            return View(consumidor);
        }

        // POST: Consumidor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Senha")] Consumidor consumidor)
        {
            if (id != consumidor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumidor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumidorExists(consumidor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(consumidor);
        }

        // GET: Consumidor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumidor = await _context.Consumidor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumidor == null)
            {
                return NotFound();
            }

            return View(consumidor);
        }

        // POST: Consumidor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumidor = await _context.Consumidor.FindAsync(id);
            _context.Consumidor.Remove(consumidor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsumidorExists(int id)
        {
            return _context.Consumidor.Any(e => e.Id == id);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (returnUrl == null)
            {
                return View();
            }
            return RedirectToPage(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Senha")] Consumidor model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var consumidor = await _context.Consumidor.FirstOrDefaultAsync(
                m => m.Email == model.Email & m.Senha == model.Senha
            );
            if (consumidor == null)
            {
                ModelState.AddModelError(string.Empty, "Us??rio ou senha incorretos.");
                ViewData["error"] = "Us??rio ou senha incorretos.";
                return View(model);
            }
            var carrinho = await _context.Carrinho.FirstOrDefaultAsync(
                m => m.ConsumidorId == consumidor.Id
            );
            HttpContext.Session.SetString("Email", consumidor.Email);
            HttpContext.Session.SetInt32("UserId", consumidor.Id);
            HttpContext.Session.SetInt32("CartId", carrinho.Id);
            UserName = consumidor.Nome;
            return RedirectToAction("Index", "Home");
        }
    }
}
