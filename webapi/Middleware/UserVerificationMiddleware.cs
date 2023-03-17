using System.Security.Claims;
using webapi.Models.DTO.UserDTO;
using webapi.Models;
using webapi.Services;
using webapi.Services.UserServices;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using webapi.Models.DTO.SetDTO;

namespace webapi.Middleware;

public class UserVerificationMiddleware : IMiddleware
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserVerificationMiddleware(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;

    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var accessToken = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");



        if (accessToken != null)
        {
            try
            {

                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadJwtToken(accessToken);
                var userId = decodedToken.Subject;
                


                // Check if user exists in the database
                var user = _mapper.Map<UserReadDto>(await _userService.GetById(userId, false));


                if (user == null)
                {
                    var userCreateDto = new UserCreateDto();

                    var newUser = _mapper.Map<User>(userCreateDto);

                    newUser.Id = userId;
                    newUser.Username = decodedToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

                    await _userService.Create(newUser);
                    return;
                }

                // Attach user to context for use in controllers
                context.Items["User"] = user;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid token");
                return;
            }
        }

        await next(context);
    }


}
