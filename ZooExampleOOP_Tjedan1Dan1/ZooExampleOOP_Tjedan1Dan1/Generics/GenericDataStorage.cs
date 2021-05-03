using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsExampleOOP_Tjedan1Dan1.Generics
{
    class GenericDataStorage<T>
    {
        public T StoredData { get; set; }

        public void StoreData(T data)
        {
            this.StoredData = data;
        }
    }
}
