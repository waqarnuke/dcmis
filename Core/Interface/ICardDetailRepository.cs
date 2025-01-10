using System;
using Core.Entities;

namespace Core.Interface;

public interface ICardDetailRepository : IRepository<CardDetail>
{
    bool IsExists(int id);
    Task<bool> SaveChangesAsync();
}
