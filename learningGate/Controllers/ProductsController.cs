using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ssSystem.Data;
using ssSystem.Interfaces;
using ssSystem.Models;
using ssSystem.ViewModels;

namespace ssSystem.Views
{
    public class ProductsController : Controller
    {
        private readonly ssDbContext _context;
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly IPhotoService _photoService;


        public ProductsController(UserManager<Employee> userManager,
            SignInManager<Employee> signInManager, ssDbContext context, IPhotoService photoService)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _photoService = photoService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var ssDbContext = _context.Products.Include(p => p.ProductType).Include(p => p.Images).ToList();
                var viewModels = ssDbContext.Select(product => new ProductViewModel
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ShortDescription = product.ShortDescription,
                    Price = product.Price,
                    Stock = product.Stock,
                    ProductTypeId = product.ProductTypeId,
                    IsSerivce = product.IsSerivce,
                    Status = product.Status,
                    ProductType = product.ProductType,
                    Images = product.Images.Select(i => new Image()
                    {
                        ProductId = i.Id,
                        ImageCloud = i.ImageCloud
                        // Map other image properties
                    }).ToList()
                });
                return View(viewModels);
            }

            return RedirectToAction("Login", "Account");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || _context.Products == null)
                {
                    return NotFound();
                }

                var product = await _context.Products
                    .Include(p => p.Images)
                    .Include(p => p.ProductType)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                var proView = new ProductDetailViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ShortDescription = product.ShortDescription,
                    Price = product.Price,
                    Stock = product.Stock,
                    ProductType = product.ProductType,
                    IsSerivce = product.IsSerivce,
                    Images = product.Images.ToList(),
                    Status = product.Status
                };
                return View(proView);
            }

            return RedirectToAction("Login", "Account");
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account");
                }

                ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name");
                return View();
            }

            return RedirectToAction("Login", "Account");
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,Description,ShortDescription,Price,Stock,ProductTypeId,ImageId,IsSerivce,Status, Image")]
            ProductCreateViewModel product)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account");
                }

                // var currentUser = await _userManager.GetUserAsync(User);
                // var isInRole = await _userManager.IsInRoleAsync(currentUser, "Admin");

                if (!User.IsInRole(UserRoles.Root) && !User.IsInRole(UserRoles.Admin) &&
                    !User.IsInRole(UserRoles.Employee))
                {
                    return RedirectToAction("Index", "Products");
                }

                if (ModelState.IsValid)
                {
                    ImageUploadResult result = null;
                    Image image = null;


                    var pro = new Product()
                    {
                        Name = product.Name,
                        Description = product.Description,
                        ShortDescription = product.ShortDescription,
                        Price = product.Price,
                        Stock = product.Stock,
                        ProductTypeId = product.ProductTypeId,
                        IsSerivce = product.IsSerivce,
                        Status = product.Status
                    };
                    _context.Add(pro);
                    await _context.SaveChangesAsync();
                    if (product.Image != null)
                    {
                        foreach (var imageFile in product.Image)
                        {
                            result = await _photoService.AddPhotoAsync(imageFile);
                            image = new Image
                            {
                                ImageCloud = result.Url.ToString() ?? null,
                                ProductId = pro.Id
                            };
                            _context.Images.Add(image);
                        }

                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction(nameof(Index));
                }

                ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Id", product.ProductType);
                return View(product);
            }

            return RedirectToAction("Login", "Account");
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account");
                }

                // var currentUser = await _userManager.GetUserAsync(User);
                // var isInRole = await _userManager.IsInRoleAsync(currentUser, UserRoles.Root);
                if (!User.IsInRole(UserRoles.Root) && !User.IsInRole(UserRoles.Admin) &&
                    !User.IsInRole(UserRoles.Employee))
                {
                    return RedirectToAction("Index", "Products");
                }

                if (id == null || _context.Products == null)
                {
                    return NotFound();
                }

                var product = _context.Products
                    .Include(p => p.Images)
                    .FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                IFormFile image = null;
                // if (product.Image != null)
                // {
                //     image = await _photoService.GetFormFileFromImageUrl(product.ImageCloud);
                // }

                var pro = new ProductEditViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ShortDescription = product.ShortDescription,
                    Price = product.Price,
                    Stock = product.Stock,
                    ProductType = product.ProductType,
                    ProductTypeId = product.ProductTypeId,
                    IsSerivce = product.IsSerivce,
                    Images = product.Images.ToList(),
                    Status = product.Status,
                };

                ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name", product.ProductType);
                return View(pro);
            }

            return RedirectToAction("Login", "Account");
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            ProductEditViewModel product)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account");
                }

                // var currentUser = await _userManager.GetUserAsync(User);
                // var isInRole = await _userManager.IsInRoleAsync(currentUser, "Admin");
                ImageUploadResult result = null;
                if (product.Image != null)
                {
                    foreach (var imageFile in product.Image)
                    {
                        result = await _photoService.AddPhotoAsync(imageFile);
                        
                        var image = new Image
                        {
                            ImageCloud = result.Url.ToString() ?? null,
                            ProductId = product.Id
                        };

                        _context.Images.Add(image);
                    }

                    List<Image> imageArr = _context.Images.Where(i => i.ProductId == product.Id).ToList();
                    foreach (var image in imageArr)
                    {
                        _context.Images.Remove(image);
                    }

                    _context.SaveChanges();
                }

                if (!User.IsInRole(UserRoles.Root) && !User.IsInRole(UserRoles.Admin) &&
                    !User.IsInRole(UserRoles.Employee))
                {
                    return RedirectToAction("Index", "Products");
                }

                if (id != product.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var pro = new Product()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ShortDescription = product.ShortDescription,
                        Price = product.Price,
                        Stock = product.Stock,
                        ProductTypeId = product.ProductTypeId,
                        IsSerivce = product.IsSerivce,
                        Status = product.Status
                    };
                    try
                    {
                        _context.Update(pro);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.Id))
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

                ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name", product.ProductType);
                return View(product);
            }

            return RedirectToAction("Login", "Account");
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account");
                }

                // var currentUser = await _userManager.GetUserAsync(User);
                // var isInRole = await _userManager.IsInRoleAsync(currentUser, "Admin");
                if (!User.IsInRole(UserRoles.Root) && !User.IsInRole(UserRoles.Admin) &&
                    !User.IsInRole(UserRoles.Employee))
                {
                    return RedirectToAction("Index", "Products");
                }

                if (id == null || _context.Products == null)
                {
                    return NotFound();
                }

                var product = await _context.Products
                    .Include(p => p.Images)
                    .Include(p => p.ProductType)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }

            return RedirectToAction("Login", "Account");
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (_context.Products == null)
                {
                    return Problem("Entity set 'ssDbContext.Products'  is null.");
                }

                // var currentUser = await _userManager.GetUserAsync(User);
                // var isInRole = await _userManager.IsInRoleAsync(currentUser, "Admin");
                if (!User.IsInRole(UserRoles.Root) && !User.IsInRole(UserRoles.Admin) &&
                    !User.IsInRole(UserRoles.Employee))
                {
                    return RedirectToAction("Index", "Products");
                }

                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Login", "Account");
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}