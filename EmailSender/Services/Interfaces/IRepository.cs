using EmailSender.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Services.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        void Add(T item);
        bool Remove(T iteam);
        void Update(int id, T item);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
