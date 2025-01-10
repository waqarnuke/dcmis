using System;
using Core.Entities;

namespace Core.Interface;

public interface IPhotoRepository :IRepository<Core.Entities.Photo>
{
    Task<bool> SaveChangesAsync();
}
