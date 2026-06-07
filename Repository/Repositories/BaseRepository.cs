using Domain.Common;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public static int idCount = 1;
        public void Create(T data)
        {
            data.Id = idCount++;
            AppDbContext<T>.datas.Add(data);
        }
        public void Delete(T data)
        {
            AppDbContext<T>.datas.Remove(data);
        }
        public T GetById(int id)
        {
            return AppDbContext<T>.datas.FirstOrDefault(m => m.Id == id);
        }
        public List<T> GetAll()
        {
            return AppDbContext<T>.datas;
        }

        public IEnumerable<T> GetAllWithCondition(Func<T, bool> predicate)
        {
            var result = AppDbContext<T>.datas;
            return result.Where(predicate);
        }
        public void Update(int id, T data)
        {
            var result = AppDbContext<T>.datas.FirstOrDefault(m => m.Id == id);
        }
    }
}
