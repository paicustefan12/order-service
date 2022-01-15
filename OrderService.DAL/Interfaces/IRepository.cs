using EquipmentService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.DAL.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
