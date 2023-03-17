using System.Security.Claims;
using webapi.Models.DTO.UserDTO;
using webapi.Models;
using webapi.Services;
using webapi.Services.UserServices;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using webapi.Models.DTO.SetDTO;
using Microsoft.AspNetCore.Http;

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

        if (context.Session.TryGetValue("User", out var userBytes))
        {
            // Deserialize user object from session
            var user = JsonSerializer.Deserialize<User>(userBytes);
            context.Items["User"] = user;
        }
        else if (accessToken != null)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadJwtToken(accessToken);
                var userId = decodedToken.Subject;
                var userEmail = decodedToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

                // Check if user exists in the database
                var user = await _userService.GetById(userId, false);

                if (user == null)
                {
                    user = await _userService.Create(new User { Id = userId, Username = userEmail });
                }

                // Attach user to context for use in controllers
                context.Items["User"] = user;

                // Save user object to session
                userBytes = JsonSerializer.SerializeToUtf8Bytes(user);
                context.Session.Set("User", userBytes);
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
