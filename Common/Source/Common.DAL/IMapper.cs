using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Common.DAL
{
    public interface IMapper<T1, T2>
    {
        T1 Map(T2 entity);
        T2 Map(T1 entity);
    }
}
