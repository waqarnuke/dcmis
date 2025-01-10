using System;
using Core.Entities;
using Core.Interface;

namespace Infrastructure.Data;

public class CardDetailRepository(ApplicationContext context) : Repository<CardDetail>(context), ICardDetailRepository
{
    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0 ;
    }

    public bool IsExists(int id)
    {
        return context.CardDetails.Any(x => x.Id == id);
    }
}
