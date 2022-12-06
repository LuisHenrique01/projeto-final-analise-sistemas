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
    public class CarrinhoController : Controller
    {
        private readonly MyDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public CarrinhoController(MyDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Carrinho/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinho = await _context.Carrinho
                .Include(c => c.Consumidor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrinho == null)
            {
                return NotFound();
            }

            return View(carrinho);
        }

        // GET: Carrinho/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            int? CartId = HttpContext.Session.GetInt32("CartId");
            _logger.LogInformation($"Entrou === {CartId} {id}");
            if (CartId == null)
            {
                return RedirectToAction("Login", "Consumidor");
            }
            var carrinho = await _context.Carrinho.FindAsync(CartId);
            var prod = await _context.Produto.FindAsync(id);
            if (carrinho == null || prod == null)
            {
                return NotFound();
            }
            if (carrinho.Produtos == null)
            {
                ICollection<Produto> produtos = new List<Produto>();
                produtos.Add(prod);
                carrinho.Produtos = produtos;
            }
            else
            {
                carrinho.Produtos.Add(prod);
            }
            _context.Update(carrinho);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Remove(int? id)
        {
            int? CartId = HttpContext.Session.GetInt32("CartId");
            if (CartId == null)
            {
                return RedirectToAction("Login", "Consumidor");
            }
            var carrinho = await _context.Carrinho.FindAsync(CartId);
            var prod = await _context.Produto.FindAsync(id);
            if (carrinho == null || prod == null)
            {
                return NotFound();
            }
            if (carrinho.Produtos == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                carrinho.Produtos.Remove(prod);
            }
            _context.Update(carrinho);
            await _context.SaveChangesAsync();
            if (carrinho.Produtos.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Create", "Pedido");
        }

    }
}
