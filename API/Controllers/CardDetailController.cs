using API.DTOs;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers ;
public class CardDetailController(
                                ICardDetailRepository repo, 
                                IPhotoService photoService,
                                IPhotoRepository photorepo) : BaseApiController
{
    [HttpGet("GetByUserId")]
    public async Task<ActionResult<CardDetailDto>> GetByUserId(string userId)
    {
        Guid id = Guid.Empty;
        id  = new Guid(userId);

        var userDetail = await repo.GetByIdAsync(x => x.UserId == id);

        var getAllphoto = await photorepo.GetByIdAsync(x => x.UserId == userId);

        if(userDetail == null) return null;
        
        var response =  new CardDetailDto {
            Id = userDetail.Id,
            UserId = userDetail.UserId.ToString(),
            Name = userDetail.Name,
            Title = userDetail.Title,
            Email = userDetail.Email,
            Phone = userDetail.Phone,
            Organization = userDetail.Organization,
            Weblink1 = userDetail.Weblink1,
            Weblink2 = userDetail.Weblink2,
            Weblink3 = userDetail.Weblink3,
            Weblink4 = userDetail.Weblink4,
            Address = userDetail.Address,
            City = userDetail.City,
            State = userDetail.State,
            ZipCode = userDetail.ZipCode,
            photo =  new PhotoDto {
                Id = getAllphoto != null ? getAllphoto.Id : 0,
                UserId = getAllphoto !=null ? getAllphoto.UserId : "",
                Url = getAllphoto !=null ? getAllphoto.Url : "",
                PublicId = getAllphoto !=null ? getAllphoto.PublicId : ""
            }
        };
        return response;
    } 

    [HttpPost]
    public async Task<ActionResult<CardDetail>> Create(CardDetailDto obj)
    {
        var request =  new CardDetail {
            UserId = new Guid(obj.UserId),
            Name = obj.Name,
            Title = obj.Title,
            Email = obj.Email,
            Phone = obj.Phone,
            Organization = obj.Organization,
            Weblink1 = obj.Weblink1,
            Weblink2 = obj.Weblink2,
            Weblink3 = obj.Weblink3,
            Weblink4 = obj.Weblink4,
            Address = obj.Address,
            City = obj.City,
            State = obj.State,
            ZipCode = obj.ZipCode,
            };

        repo.Add(request);
        
        if(await repo.SaveChangesAsync())
        {
            return CreatedAtAction("GetByUserId", new{ id = request.Id},request);
        }

        return BadRequest("Problem createing user detail");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, CardDetail obj)
    {
        if(obj.Id != id  || !IsExists(id))
            return BadRequest("Cannot update this user detail");
        
        repo.Update(obj);

        if(await repo.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem updating the user detail");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var userDetail = await repo.GetByIdAsync(x => x.Id == id);

        if(userDetail == null) return NotFound();
        
        if( userDetail != null )
        {
            var photo = await photorepo.GetByIdAsync(x => x.UserId == userDetail.UserId.ToString());

            if (photo.PublicId != null)
            {
                var result = await photoService.DeletePhoto(photo.PublicId);

                if (result.Error != null) return BadRequest(result.Error.Message);

                photorepo.Delete(photo);

                await photorepo.SaveChangesAsync();
            }
        }
        

        repo.Delete(userDetail);

        if(await repo.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem deleting the user detail");
        
    }
    
    [HttpPost("addphoto")]
    public async Task<ActionResult<IReadOnlyList<PhotoDto>>> AddPhoto(IFormFile file,string userId)
    {
        var result = await photoService.AddPhoto(file);

        if (result.Error != null) return BadRequest(result.Error.Message);

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId,
            UserId = userId
        };

        photorepo.Add(photo);

        if(await photorepo.SaveChangesAsync())
        {
            var getAllphoto = await photorepo.GetAsync(x => x.UserId == userId);
            var response = getAllphoto.Select(x => new PhotoDto {
                PublicId = x.PublicId,
                Url = x.Url,
                UserId = x.UserId,
            } ).ToList();

            return response;

        }

        return BadRequest();
    }
    
    [HttpDelete("deletephoto/{photoId:int}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
        var photo = await photorepo.GetByIdAsync(x => x.Id == photoId);

        if (photo == null) return BadRequest("This photo cannot be deleted");

        if (photo.PublicId != null)
        {
            var result = await photoService.DeletePhoto(photo.PublicId);
            if (result.Error != null) return BadRequest(result.Error.Message);
        }

        photorepo.Delete(photo);

        if(await photorepo.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem deleting photo");
    }
    private bool IsExists(int id)
    {
        return repo.IsExists(id);
    }
}
