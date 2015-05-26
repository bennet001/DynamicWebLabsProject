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
        IEnumerable<T> GetAllItems();

        void Update(T pro);

        void Delete(T pro);

        T Get(int id);

        T Get(T pro);

        void Create(T pro);

        void Save(IEnumerable<T> pro);
    }
}
