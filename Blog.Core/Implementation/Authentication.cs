using AutoMapper;
using Blog.Core.Abstraction;
using Blog.Models;
using Blog.Models.Dtos.Request;
using Blog.Models.Dtos.Response;
using Blog.Models.settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Implementation
{
    public class Authentication : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtConfig _jwtConfig;

        public Authentication(IServiceProvider serviceProvider, IOptions<JwtConfig> options)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _jwtConfig = options.Value;
        }

        public async Task<Response<RegisterResDto>> Register(RegisterReqDto registerreqdto)
        {
            Response<RegisterResDto> response = new Response<RegisterResDto>();

            var existingUser = await _userManager.FindByEmailAsync(registerreqdto.Email);

            if (existingUser != null)
            {
                response.Message = "Invalid Credentials";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Success = false;
                return response;
            }

            var newUser = _mapper.Map<RegisterReqDto, AppUser>(registerreqdto);

            IdentityResult result = await _userManager.CreateAsync(newUser, registerreqdto.Password);

            if (!result.Succeeded)
            {
                response.Message = "Invalid Credentials";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Success = false;
                return response;
            }

            RegisterResDto registeResDto = new RegisterResDto
            {
                Id = newUser.Id,
                Username = newUser.UserName

            };

            response.StatusCode = (int)HttpStatusCode.Created;
            response.Message = "Registration successful";
            response.Success = true;
            response.Data = registeResDto;
            return response;
        }

        public async Task<Response<LoginResDto>> Login(LoginReqDto loginReqDto)
        {
            Response<LoginResDto> response = new Response<LoginResDto>();

            var user = await _userManager.FindByEmailAsync(loginReqDto.Email);

            if (user == null)
            {
                response.Message = "Invalid Credentials";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Success = false;
                return response;
            }

            
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginReqDto.Password);

            if (!passwordCheck)
            {

                response.Message = "Invalid Credentials";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Success = false;
                return response;
            }

            var token = await GenerateToken(user);

            LoginResDto loginresponse = new LoginResDto
            {
                Token = token,
                UserId = user.Id
            };


            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = loginresponse;
            response.Message = "Login successful";
            response.Success = true;

            return response;
        }



        public async  Task<string> GenerateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

       
    }
}
