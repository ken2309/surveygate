using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using learningGate.Data;
using learningGate.Interfaces;
using learningGate.ViewModels;
using learningGate.Models;
using Microsoft.EntityFrameworkCore;

namespace learningGate.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<Employee> _userManager;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserController(IUserRepository userRepository, UserManager<Employee> userManager,
            IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("users")]
        [Route("tai-khoan")]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(UserRoles.Admin))
            {
                var users = await _userRepository.GetAllUsers();
                List<UserViewModel> result = new List<UserViewModel>();

                foreach (var user in users)
                {
                    var userViewModel = new UserViewModel()
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        ProfileImageUrl = user.ProfileImageUrl ?? "/img/avatar-male-4.jpg",
                    };
                    result.Add(userViewModel);
                }

                return View(result);
            }

            var userD = await _userManager.GetUserAsync(User);

            if (userD == null)
            {
                return View("Error");
            }

            return RedirectToAction("Detail", "User", new { userD.Id });
        }
        [Route("tai-khoan/luu-tru")]
        public async Task<IActionResult> UserOrders()
        {
            var orders = await _userRepository.UserOrders();
            return View(orders);
        }
        //
        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "User");
            }

            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                // ProfileImageUrl = user.ProfileImageUrl ?? "/img/avatar-male-4.jpg",
            };
            return View(userDetailViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            var editMV = new EditProfileViewModel()
            {
                // City = user.City,
                // State = user.State,
                // Pace = user.Pace,
                // Mileage = user.Mileage,
                ProfileImageUrl = user.ProfileImageUrl
            };
            return View(editMV);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditProfile", editVM);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            if (editVM.Image != null) // only update profile image
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                if (photoResult.Error != null)
                {
                    ModelState.AddModelError("Image", "Failed to upload image");
                    return View("EditProfile", editVM);
                }

                if (!string.IsNullOrEmpty(user.ProfileImageUrl))
                {
                    _ = _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }

                user.ProfileImageUrl = photoResult.Url.ToString();
                editVM.ProfileImageUrl = user.ProfileImageUrl;

                await _userManager.UpdateAsync(user);

                return View(editVM);
            }

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Detail", "User", new { user.Id });
        }
        
    }
}