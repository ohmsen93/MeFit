using System.Security.Claims;
using webapi.Models.DTO.UserDTO;
using webapi.Models;
using webapi.Services;
using webapi.Services.UserServices;
using AutoMapper;

namespace webapi.Middleware;

public class UserVerificationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public UserVerificationMiddleware(RequestDelegate next, IUserService service, IMapper mapper)
    {
        _next = next;
        _service = service;
        _mapper = mapper;
    }


    public async Task InvokeAsync(HttpContext context, IUserService userService, UserCreateDto userCreateDto)
    {
        // Extract user ID from JWT token
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Check if user exists in database
        var user = await userService.GetById(userId);

        if (user == null)
        {
            user = _mapper.Map<User>(userCreateDto);
            await _service.Create(user);


            // Create new user in database
            await userService.Create(user);
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }
}
