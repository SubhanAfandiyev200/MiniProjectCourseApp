using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T:BaseEntity
    {
        void Create(T data);
        void Delete(T data);
        public T GetById(int id);
        public List<T> GetAll();
    }
}
