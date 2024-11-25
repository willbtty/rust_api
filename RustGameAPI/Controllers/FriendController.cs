// Controllers/FriendController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RustGameAPI.Data;
using RustGameAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class FriendController : ControllerBase
{
    private readonly AppDbContext _context;

    public FriendController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("AddFriend")]
    public async Task<IActionResult> AddFriend(FriendDTO friendDto)
    {
        var exists = await _context.Friends
            .AnyAsync(f =>
                (f.UserID == friendDto.UserID && f.FriendUserID == friendDto.FriendUserID) ||
                (f.UserID == friendDto.FriendUserID && f.FriendUserID == friendDto.UserID));

        if (exists)
        {
            return BadRequest("Friendship already exists.");
        }

        var friend = new Friend
        {
            UserID = friendDto.UserID,
            FriendUserID = friendDto.FriendUserID
        };

        var reciprocalFriend = new Friend
        {
            UserID = friendDto.FriendUserID,
            FriendUserID = friendDto.UserID
        };

        _context.Friends.AddRange(friend, reciprocalFriend);
        await _context.SaveChangesAsync();
        return Ok(new { friend, reciprocalFriend });
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetFriends(int userId)
    {
        var friends = _context.Friends.Where(f => f.UserID == userId).ToList();
        return Ok(friends);
    }

    [HttpGet("GetAllFriends/{userId}")]
    public async Task<IActionResult> GetAllFriends(int userId)
    {
        var friends = _context.Friends
            .Where(f => f.UserID == userId)
            .Include(f => f.User)
            .Include(f => f.FriendUser)
            .Select(f => f.UserID == userId
                ? f.FriendUser.Username // If the current user is the "User", return FriendUser's username
                : f.User.Username)      // Otherwise, return the User's username
            .Distinct() // Remove duplicates in case of reciprocal friendships
            .ToList();
        return Ok(friends);
    }
}
