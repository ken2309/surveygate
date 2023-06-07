using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using learningGate.Data;
using learningGate.ViewModels;
using learningGate.Models;

namespace learningGate.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;

        public DashboardController(ILogger<DashboardController> logger,
            UserManager<Employee> userManager, SignInManager<Employee> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        // [Authorize]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Dashboard");
            }

            if (User.IsInRole(UserRoles.User))
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        //
        // public IActionResult Register() 
        // {
        //     var response = new HomeUserCreateViewModel();
        //     return View(response);
        // }
        //
        // [HttpPost]
        // public async Task<IActionResult> Index(HomeViewModel homeVM)
        // {
        //     var createVM = homeVM.Register;
        //
        //     if (!ModelState.IsValid) return View(homeVM);
        //
        //     var user = await _userManager.FindByEmailAsync(createVM.Email);
        //     if (user != null)
        //     {
        //         ModelState.AddModelError("Register.Email", "This email address is already in use");
        //         return View(homeVM);
        //     }
        //
        //     var userLocation = await _locationService.GetCityByZipCode(createVM.ZipCode ?? 0);
        //
        //     if (userLocation == null)
        //     {
        //         ModelState.AddModelError("Register.ZipCode", "Could not find zip code!");
        //         return View(homeVM);
        //     }
        //
        //     var newUser = new AppUser
        //     {
        //         UserName = createVM.UserName,
        //         Email = createVM.Email,
        //         Address = new Address()
        //         {
        //             State = userLocation.StateCode,
        //             City = userLocation.CityName,
        //             ZipCode = createVM.ZipCode ?? 0,
        //         }
        //     };
        //
        //     var newUserResponse = await _userManager.CreateAsync(newUser, createVM.Password);
        //
        //     if (newUserResponse.Succeeded)
        //     {
        //         await _signInManager.SignInAsync(newUser, isPersistent: false);
        //         await _userManager.AddToRoleAsync(newUser, UserRoles.User);
        //     }
        //     return RedirectToAction("Index", "Club");
        // }
        //

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login([FromQuery] string? returnUrl)
        {
            var response = new LoginViewModel();
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(UserRoles.Admin))
                {
                    // User is already logged in, redirect to dashboard
                    return RedirectToAction("Index", "Dashboard");
                }

                return RedirectToAction("Index", "Home");
            }

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                // if(user.IsAdmin.Value)
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        // await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                        return RedirectToAction("Index", "Dashboard");
                    }
                }

                //Password is incorrect
                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginViewModel);
            }

            //User not found
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginViewModel);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}