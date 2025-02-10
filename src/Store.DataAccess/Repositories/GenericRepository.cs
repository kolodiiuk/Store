using System.Linq.Expressions;
using Store.Domain.Contracts.Repositories;
using Store.Domain.Entities;
using Store.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Store.DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
    where T : BaseEntity
{
    protected readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<T>>> GetAllAsync()
    {
        try
        {
            var list = await _context.Set<T>().AsNoTracking().ToListAsync();

            return Result.Success((IEnumerable<T>) list);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<T>>($"Error fetching {typeof(T)}: {ex.Message}");
        }
    }

    public async Task<Result<T>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _context.Set<T>().FindAsync(id);
            
            return Result.Success(entity);
        }
        catch (Exception e)
        {
            return Result.Fail<T>(e.Message);
        }
    }

    public async Task<Result<IEnumerable<T>>> GetListByConditionAsync(
        Expression<Func<T, bool>> condition)
    {
        try
        {
            var filteredEntities = await _context.Set<T>()
                .Where(condition)
                .ToListAsync();
            
            return Result.Success<IEnumerable<T>>(filteredEntities);
        }
        catch (Exception e)
        {
            return Result
                .Fail<IEnumerable<T>>($"Failure retrieving data.: {e.Message}");
        }
    }

    public async Task<Result<T>> GetByConditionAsync(Expression<Func<T, bool>> condition)
    {
        try
        {
            var item = await _context.Set<T>()
                .Where(condition)
                .FirstOrDefaultAsync();
            
            if (item != null)
            {
                return Result.Success(item);
            }
            else
            {
                return Result.Fail<T>("Item not found.");
            }
        }
        catch (Exception e)
        {
            return Result.Fail<T>($"Failure retrieving data.: {e.Message}");
        }
    }
    
    public async Task<Result<int>> CreateAsync(T entity)
    {
        try
        {
            var entityEntry = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            
            return Result.Success(entityEntry.Entity.Id);
        }
        catch (Exception e)
        {
            return Result.Fail<int>(e.Message);
        }
    }

    public async Task<Result> UpdateAsync(T entity)
    {
        try
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.Fail(
                $"Entity of type {typeof(T).Name} with ID {entity.Id} does not exist or has been modified by another user.");
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return Result.Fail($"Entity of type {typeof(T).Name} with ID {id} does not exist.");
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}
