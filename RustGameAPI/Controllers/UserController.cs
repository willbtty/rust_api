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
    public async Task<IActionResult> AddUser(string username, string password)
    {
        var user = new RustGameAPI.Models.User
        {
            Username = username,
            Password = password
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
        var users = await _context.Users.ToListAsync();
        return Ok(users);
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
        var user = _context.Users
        .Where(x => x.Username == username)
        .ToList();
        return user == null ? NotFound() : Ok(user);
    }

    [HttpGet("GetIDfromUsername/{Username}")]
    public async Task<IActionResult> GetIDfromUsername(string Username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == Username);
        return user == null ? NotFound() : Ok(user.UserID);
    }

    [HttpGet ("GetScoresDescending")]
    public async Task<IActionResult> GetScoresDescending()
    {
        var users_scores = _context.Users
            .Where(x => x.HighScore > 0)
            .OrderByDescending(x => x.HighScore)
            .Select(x => new
            {
                x.UserID,
                x.Username,
                x.Password,
                x.HighScore
            })
            .ToList();
        return Ok(users_scores);
    }

    [HttpPut("UpdateHighScore")]
    public async Task<IActionResult> UpdateHighScore(int UserID, int HighScore)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == UserID);
        if (user == null)
        {
            return NotFound();
        }
        user.HighScore = HighScore;
        await _context.SaveChangesAsync();
        return Ok(user);
    }

    [HttpGet("GetHighScore/{UserID}")]
    public async Task<IActionResult> GetHighScore(int UserID)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == UserID);
        return user == null ? NotFound() : Ok(user.HighScore);
    }

    [HttpPut("ChangeUsername")]
    public async Task<IActionResult> ChangeUsername(int UserID, string NewUsername)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == UserID);
        if (user == null)
        {
            return NotFound();
        }
        user.Username = NewUsername;
        await _context.SaveChangesAsync();
        return Ok(user);
    }

    [HttpPut("ChangePassword")]
    public async Task<IActionResult> ChangePassword(int UserID, string NewPassword)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == UserID);
        if (user == null)
        {
            return NotFound();
        }
        user.Password = NewPassword;
        await _context.SaveChangesAsync();
        return Ok(user);
    }
}
