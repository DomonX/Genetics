using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Genetics
{
    class FloatChromosome : IChromosome<float>
    {
        private readonly float value;

        FloatChromosome(float value)
        {
            this.value = value;
        }

        public float GetValue()
        {
            return value;
        }

        public IChromosome<float> Create(IChromosome<float> partner, float mutationChance)
        {
            float value = partner.GetValue() + GetValue() * 0.5f;
            if (Random.Range(0.0f, 1.0f) < mutationChance)
            {
                value += Random.Range(-0.1f, 0.1f);
                value = Mathf.Clamp01(value);
            }
            return new FloatChromosome(value);
        }

    }
}
