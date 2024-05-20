using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawsAndEars.Models;

namespace PawsAndEars.EF.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Save(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(string id);
        void Update(string id, T entity);
    }
}
