using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IStudentService
    {
        void Create(Student student);
        Student GetById(int id);
        //Delete qalib yaz
    }
}
