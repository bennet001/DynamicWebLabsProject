using DigiDepot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiDepot.Interfaces
{
    interface IDataHandler<T>
    {
        List<T> GetAllItems();

        void Update(T item);

        void Update(T cart, int item, int quantity);

        void Delete(T item);

        void Remove(T cart, int item);

        T Get(int id);

        T Get(T item);

        void Create(T item);

        void Save(IEnumerable<T> item);
    }
}
