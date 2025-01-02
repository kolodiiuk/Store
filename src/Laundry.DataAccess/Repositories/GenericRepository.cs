using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly LaundryDbContext _context;

    public GenericRepository(LaundryDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IQueryable<T>>> GetAllAsync()
    {
        try
        {
            var queryable = _context.Set<T>().AsNoTracking();
            return Result.Success(queryable);
        }
        catch (Exception ex)
        {
            return Result.Fail<IQueryable<T>>(ex.Message);
        }
    }

    public async Task<Result<T>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<int>> CreateAsync(T entity)
    {
        throw new NotImplementedException();
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Result> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<Result> DeleteAsync(int id)
    {
        throw new NotImplementedException();
        // _context.Remove();
        await _context.SaveChangesAsync();
    }
}