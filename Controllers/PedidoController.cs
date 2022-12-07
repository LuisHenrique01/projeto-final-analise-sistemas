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
    public class PedidoController : Controller
    {
        private readonly MyDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public PedidoController(MyDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Pedido
        public async Task<IActionResult> Index()
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            if (id == null)
            {
                return RedirectToAction("Login", "Consumidor");
            }
            Consumidor consumidor = await _context.Consumidor.FindAsync(id);

            return View(await _context.Pedido.Where(m => m.Consumidor == consumidor).Include("Produtos")
                              .ToListAsync());
        }

        // GET: Pedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedido/Create
        public IActionResult Create()
        {
            int? id = HttpContext.Session.GetInt32("CartId");
            if (id == null)
            {
                return RedirectToAction("Login", "Consumidor");
            }
            var carrinho = _context.Carrinho.Where(m => m.Id == id).Include("Produtos").FirstOrDefault();
            return View(carrinho.Produtos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            int? id = HttpContext.Session.GetInt32("CartId");
            if (id == null)
            {
                return RedirectToAction("Login", "Consumidor");
            }
            Carrinho carrinho = _context.Carrinho.Where(m => m.Id == id).Include("Produtos")
                                .Include("Consumidor").FirstOrDefault();

            Pedido pedido = new Pedido();
            Pagamento pagamento = new Pagamento();
            pedido.Produtos = carrinho.Produtos;
            pedido.Consumidor = carrinho.Consumidor;
            _context.Add(pedido);
            await _context.SaveChangesAsync();

            if (form["formaPagamento"] == "cartao")
            {

                CartaoCredito cartaoCredito = new CartaoCredito()
                {
                    numero = form["numero"]
                };
                _context.Add(cartaoCredito);
                await _context.SaveChangesAsync();
                pagamento.CartaoCredito = cartaoCredito;
            }
            else
            {
                Random rnd = new Random();
                Boleto boleto = new Boleto()
                {
                    codigo = $"{rnd.Next()} {rnd.Next()} {rnd.Next()} {rnd.Next()}"
                };
                _context.Add(boleto);
                await _context.SaveChangesAsync();
                pagamento.Boleto = boleto;
            }
            pagamento.Pedido = pedido;
            pagamento.Valor = pedido.getValorPedidos();
            _context.Add(pagamento);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Pagamento", new { id = pagamento.Id });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: Pedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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
            return View(pedido);
        }

        // GET: Pedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.Id == id);
        }
    }
}
