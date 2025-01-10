using System;
using Core.Entities;
using Core.Interface;

namespace Infrastructure.Data;

public class QRCoreRepository(ApplicationContext context) : Repository<QRCode>(context), IQRCoreRepository
{
    public bool IsExists(int id)
    {
        return context.CardDetails.Any(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0 ;
    }
}
