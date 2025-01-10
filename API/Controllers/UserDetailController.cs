using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserDetailController(IUserDetailsRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserDetails>>> GetUserDetails()
    {
        return Ok(await repo.GetAsync()); 
    } 

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDetails>> GetUserDetail(int id)
    {
        var userDetail = await repo.GetByIdAsync(x => x.Id == id);

        if(userDetail == null) return NotFound();

        return userDetail;
    } 

    [HttpPost]
    public async Task<ActionResult<UserDetails>> CreateDetail(UserDetails userDetail)
    {
        repo.Add(userDetail);
        
        if(await repo.SaveChangesAsync())
        {
            return CreatedAtAction("GetUserDetail", new{ id = userDetail.Id},userDetail);
        }

        return BadRequest("Problem createing user detail");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateUserDetail(int id, UserDetails userDetail)
    {
        if(userDetail.Id != id  || !userDetailExists(id))
            return BadRequest("Cannot update this user detail");
        
        repo.Update(userDetail);

        if(await repo.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem updating the user detail");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUserDetail(int id)
    {
        var userDetail = await repo.GetByIdAsync(x => x.Id == id);

        if(userDetail == null) return NotFound();

        repo.Delete(userDetail);

        if(await repo.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem deleting the user detail");
        
    }

    [HttpGet("GetUserDetailByUserId")]
    public async Task<ActionResult<UserDetails>> GetUserDetailByUserId(string id)
    {
        Guid userId = Guid.Empty;
        userId  = new Guid(id);
        var userDetail = await repo.GetByIdAsync(x => x.UserId == userId);

        if(userDetail == null) return NotFound();

        return userDetail;
    } 
    private bool userDetailExists(int id)
    {
        return repo.IsExists(id);
    }
}
