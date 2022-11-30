#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto_Final.Models;

namespace Projeto_Final.Controllers
{
    public class CartaoCreditoController : Controller
    {
        private readonly MyDbContext _context;

        public CartaoCreditoController(MyDbContext context)
        {
            _context = context;
        }

        // GET: CartaoCredito
        public async Task<IActionResult> Index()
        {
            return View(await _context.CartaoCredito.ToListAsync());
        }

        // GET: CartaoCredito/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartaoCredito = await _context.CartaoCredito
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartaoCredito == null)
            {
                return NotFound();
            }

            return View(cartaoCredito);
        }

        // GET: CartaoCredito/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CartaoCredito/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,numero")] CartaoCredito cartaoCredito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartaoCredito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cartaoCredito);
        }

        // GET: CartaoCredito/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartaoCredito = await _context.CartaoCredito.FindAsync(id);
            if (cartaoCredito == null)
            {
                return NotFound();
            }
            return View(cartaoCredito);
        }

        // POST: CartaoCredito/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,numero")] CartaoCredito cartaoCredito)
        {
            if (id != cartaoCredito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartaoCredito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartaoCreditoExists(cartaoCredito.Id))
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
            return View(cartaoCredito);
        }

        // GET: CartaoCredito/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartaoCredito = await _context.CartaoCredito
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartaoCredito == null)
            {
                return NotFound();
            }

            return View(cartaoCredito);
        }

        // POST: CartaoCredito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartaoCredito = await _context.CartaoCredito.FindAsync(id);
            _context.CartaoCredito.Remove(cartaoCredito);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartaoCreditoExists(int id)
        {
            return _context.CartaoCredito.Any(e => e.Id == id);
        }
    }
}
