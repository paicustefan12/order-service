using EquipmentService.DAL.Entities;
using EquipmentService.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext context;
        private DbSet<T> entities;
        public Repository(AppDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public async Task<List<T>> GetAll()
        {
            return await entities.ToListAsync();
        }
        public async Task<T> Get(int id)
        {
            return await entities.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await context.SaveChangesAsync();
        }
        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
