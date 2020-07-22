using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstruturarRepository.Repository
{
    public interface IRepository<T> where T: class
    {
        void Add(T obj);
        void Update(T obj);
        List<T> GetAll();
        void Deletar(object id);
        void Save();

    }
}
