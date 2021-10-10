using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetics
{
    interface IChromosome<T>
    {
        IChromosome<T> Create(IChromosome<T> parner, float mutationChance);
        T GetValue();
    }
}
