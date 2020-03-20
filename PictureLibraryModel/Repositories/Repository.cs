using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly List<T> listOfItems;

        public Repository(List<T> list)
        {
            this.listOfItems = list;
        }

        public void Add(T entity)
        {
            listOfItems.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            listOfItems.AddRange(entities);
        }

        public T Get(int id)
        {
            if (listOfItems.Count > id && id >= 0)
            {
                return listOfItems[id];
            }
            else 
            {
                return null;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return listOfItems;
        }

        public void Remove(T entity)
        {
            listOfItems.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            foreach(T t in entities)
            {
                listOfItems.Remove(t);
            }
        }
    }
}
