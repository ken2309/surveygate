using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using learningGate.Data;
using learningGate.Interfaces;
using learningGate.Models;
using learningGate.ViewModels;

namespace learningGate.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly learningGateDbContext _context;

        public OrdersController(learningGateDbContext context,IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            
            var ssDbContext = await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderDetail)
                .ThenInclude(o => o.Product)
                .ThenInclude(o => o.Images)
                .Include(o => o.OrderDetail)
                .ThenInclude(o => o.Product)
                .ThenInclude(o => o.ProductType).ToListAsync();

            // var res = ssDbContext.Select( order => new Orders {
            //     
            //     Id = order.Id,
            //     UserId = order.UserId,
            //     CreateDate = order.CreateDate,
            //     OrderStatus = order.OrderStatus,
            //     OrderDetail = order.OrderDetail,
            //     IsDeleted = order.IsDeleted
            // });
            return View(ssDbContext);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatus, "Id", "StatusName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CreateDate,OrderStatusId,IsDeleted")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatus, "Id", "StatusName", order.OrderStatusId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatus, "Id", "StatusName", order.OrderStatusId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,UserId,CreateDate,OrderStatusId,IsDeleted")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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

            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatus, "Id", "StatusName", order.OrderStatusId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ssDbContext.Orders'  is null.");
            }

            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}