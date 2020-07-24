using EmailSender.Models.Interfaces;
using EmailSender.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Services.Base
{
    abstract class RepositoryInMemory<T> : IRepository<T> where T: IEntity
    {
        private List<T> Items = new List<T>();
        private int LastId = 1;
        protected RepositoryInMemory() { }
        protected RepositoryInMemory(IEnumerable<T> items) 
        {
            foreach (var item in Items)
                Add(item);
            
        } 
        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (Items.Contains(item))
                return;
            item.Id = ++LastId;
            Items.Add(item);
        }

        public IEnumerable<T> GetAll() => Items;

        public T Get(int id) => GetAll().FirstOrDefault(item => item.Id == id);

        public bool Remove(T item) => Items.Remove(item);

        public void Update(int id, T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(item), item, "Индекс не может быть меньше 1");
            if (Items.Contains(item))
                return;
            var dbitem = Get(id);
            if (dbitem == null)
                throw new InvalidOperationException("Рудактируемый элемент не найден");
            Update(item, dbitem);
        }

        protected abstract void Update(T Source, T Destination);
    }
}
