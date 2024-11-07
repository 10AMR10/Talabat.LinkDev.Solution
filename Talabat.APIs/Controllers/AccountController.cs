using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Identity;
using Talabat.Core.service.contract;

namespace Talabat.APIs.Controllers
{

	public class AccountController : BaseController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokentService _tokentService;

		public AccountController(UserManager<AppUser> userManager
			,SignInManager<AppUser> signInManager,ITokentService tokentService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokentService = tokentService;
		}
		//register
		[HttpPost("Register")]
		public async Task<ActionResult<UserToReturnDto>> Register( RegisterDto input)
		{
			var user = new AppUser()
			{
				DisplayName = input.DisplayName,
				Email = input.Email,
				PhoneNumber = input.PhoneNumber,
				UserName = input.Email.Split('@')[0]
			};
			var res=await _userManager.CreateAsync(user, input.Password);
			if (!res.Succeeded)
			{
				foreach (var error in res.Errors)
				{
					Console.WriteLine($"Error: {error.Code} - {error.Description}");
				}
				return BadRequest(new ApiResponse(400));
			}
			return Ok(new UserToReturnDto()
			{
				DisplayName = input.DisplayName,
				Email = input.Email,
				Token = await _tokentService.CreateTokenAsync(user, _userManager)
			});
		}


		//login
		[HttpPost("Login")]
		public async Task<ActionResult<UserToReturnDto>> Login(LoginDto input)
		{
			var user=await _userManager.FindByEmailAsync(input.Email);
			if (user is null)
				return Unauthorized(new ApiResponse(401));
			var res = await _signInManager.CheckPasswordSignInAsync(user, input.Password,false);
			if (!res.Succeeded)
				return Unauthorized(new ApiResponse(401));
			return Ok(new UserToReturnDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _tokentService.CreateTokenAsync(user, _userManager)
			});
		}
	}
}
