using System;
using Core.Entities;

namespace Core.Interface;

public interface IQRCoreRepository : IRepository<QRCode>
{
    bool IsExists(int id);
    Task<bool> SaveChangesAsync();
}
