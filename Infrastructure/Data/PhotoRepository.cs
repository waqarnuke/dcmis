using System;
using Core.Interface;

namespace Infrastructure.Data;

public class PhotoRepository(ApplicationContext context) : Repository<Core.Entities.Photo>(context), IPhotoRepository
{
    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
