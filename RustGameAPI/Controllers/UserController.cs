// Decompiled with JetBrains decompiler
// Type: UserController
// Assembly: RustGameAPI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A41AE7D8-5859-4B72-B5E4-69C2CC620AD6
// Assembly location: C:\Users\Admin\source\repos\api\RustGameAPI.dll

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RustGameAPI.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#nullable enable
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context) => this._context = context;

    [HttpPost("AddUser")]
    public async Task<IActionResult> AddUser(RustGameAPI.Models.User user)
    {
        UserController userController = this;
        userController._context.Users.Add(user);
        int num = await userController._context.SaveChangesAsync();
        return (IActionResult)userController.Ok((object)user);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        UserController userController = this;
        RustGameAPI.Models.User async = await userController._context.Users.FindAsync((object)id);
        return async == null ? (IActionResult)userController.NotFound() : (IActionResult)userController.Ok((object)async);
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        UserController userController = this;
        return (IActionResult)userController.Ok((object)userController._context.Users.ToList<RustGameAPI.Models.User>());
    }

    [HttpGet("LookForUser")]
    public async Task<IActionResult> LookForUser(string username)
    {
        UserController userController = this;
        RustGameAPI.Models.User user = await userController._context.Users.FirstOrDefaultAsync<RustGameAPI.Models.User>((Expression<Func<RustGameAPI.Models.User, bool>>)(u => u.Username == username));
        return user == null ? (IActionResult)userController.NotFound() : (IActionResult)userController.Ok((object)user);
    }

    [HttpPost("{Username}/{Password}")]
    public async Task<IActionResult> AddUser(string Username, string Password)
    {
        UserController userController = this;
        RustGameAPI.Models.User user = new RustGameAPI.Models.User()
        {
            Username = Username,
            Password = Password
        };
        userController._context.Users.Add(user);
        int num = await userController._context.SaveChangesAsync();
        IActionResult actionResult = (IActionResult)userController.Ok((object)user);
        user = (RustGameAPI.Models.User)null;
        return actionResult;
    }
}
