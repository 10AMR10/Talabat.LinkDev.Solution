using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.service.contract;

namespace Talabat.APIs.Controllers
{
	public class AccountController : BaseController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokentService _tokentService;
		private readonly IMapper _mapper;

		public AccountController(UserManager<AppUser> userManager
			,SignInManager<AppUser> signInManager,ITokentService tokentService,IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokentService = tokentService;
			_mapper = mapper;
		}
		//register
		[HttpPost("Register")]
		public async Task<ActionResult<UserToReturnDto>> Register( RegisterDto input)
		{
			if (CheckEmailExist(input.Email).Result.Value)
				return BadRequest(new ApiResponse(400, "Duplicated Email"));
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
		// get Current user
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		
		[HttpGet("GetUser")]
		public async Task<ActionResult<UserToReturnDto>> GetUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);// search in current user about email
			var user = await _userManager.FindByEmailAsync(email);
			return Ok(new UserToReturnDto()
			{
				Email = user.Email,
				DisplayName = user.DisplayName,
				Token = await _tokentService.CreateTokenAsync(user, _userManager)
			});

		}
		// get address from current user
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet("GetAddress")]
		public async Task<ActionResult<Address>> GetUserAddress()
		{
			//var email = User.FindFirstValue(ClaimTypes.Email);
			//var user = await _userManager.FindByEmailAsync(email);
			var user =await _userManager.FindEmailWithAddressAsync(User);
			

			return Ok(new AddressDto
			{
				FirstName=user.address.FirstName,
				LastName=user.address.LastName,
				City=user.address.City,
				Street=user.address.Street,
				Country=user.address.Country,

			});
		}
		// update user address
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpPut("UpdateAddress")]
		public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
		{
			var user =await _userManager.FindEmailWithAddressAsync(User);
			var mapped=_mapper.Map<AddressDto, Address>(address);
			mapped.Id = user.address.Id;
			user.address = mapped;
			var res=await _userManager.UpdateAsync(user);
			if(!res.Succeeded)
				return BadRequest(new ApiResponse(400));
			return Ok(address);
		}
		// Check Email Is Exist
		[HttpGet("EmailExist")]
		public async Task<ActionResult<bool>> CheckEmailExist(string email)
		{
			return await _userManager.FindByEmailAsync(email) is not null;
		}
	}
}
