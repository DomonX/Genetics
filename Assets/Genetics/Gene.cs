using System;
using System.Collections.Generic;
using UnityEngine;

namespace Genetics
{
    /*
    public abstract class Gene<T>
    {
        public T Value { get; protected set; }
       
        public Gene(T value) {
            Value = value;
        }

        public Gene()
        {
            Value = InitializeValue();
        }

       public abstract T Cross(T secondValue);
       public abstract T Mutate(T value);
       public abstract Gene<T> Initialize();
       protected abstract T InitializeValue();
    }

    public class GenotypesNotMatchingException<T> : Exception
    {
        public GenotypesNotMatchingException(Genotype<T> first, Genotype<T> second) : base("Unable to cross two different Chromosomes keys are: " + first.Key + " and " + second.Key) { }
    }

    public abstract class Chromosome<T> : ICloneable
    {
        public List<Gene<T>> Genes { get; protected set; } = new List<Gene<T>>();
       
        public Chromosome(List<Gene<T>> genes)
        {
            Genes = genes;
        }

        public Chromosome<T> Cross(Chromosome<T> second)
        {

        }

        public abstract Chromosome<T> Initialize();
    }

    public abstract class Genotype<T>
    {
        public List<Chromosome<T>> Chromosomes { get; protected set; } = new List<Chromosome<T>>();
        public string Key { get; protected set; }

        public Genotype(string key, List<Chromosome<T>> chromosomes)
        {
            Key = key;
            Chromosomes = chromosomes;
        }
        public abstract Genotype<T> Instantiniate(List<Chromosome<T>> chromosomes);

        public bool Match(Genotype<T> second)
        {
            return Key == second.Key;
        }

    }

    public class FloatGene : Gene<float>
    {
        public FloatGene(float value) : base(value)
        {
        }

        public override float Cross(float secondValue)
        {
            return (Value + secondValue) / 2;
        }

        public override float Mutate(float value)
        {
            System.Random random = new System.Random();
            return random.Next(0, 2) == 1 ? value + 0.1f : value - 0.1f;
        }

        public override Gene<float> Initialize()
        {
           return new FloatGene(UnityEngine.Random.Range(0.0f, 1.0f));
        }

        protected override float InitializeValue()
        {
            return UnityEngine.Random.Range(0.0f, 1.0f);
        }
    }

    public class FloatChromosome
    public class GenotypeFactory<T>
    {
        // Creates instance of genotype by crossing two entities
        public static Genotype<T> Cross(Genotype<T> mother, Genotype<T> father)
        {
            if(!mother.Match(father) {
                throw new GenotypesNotMatchingException<T>(mother, father);
            }

            List<Chromosome<T>> chromosomes = new List<Chromosome<T>>();
            for(int i = 0; i < mother.Chromosomes.Count; i++)
            {
                chromosomes.Add(mother.Chromosomes[i].Cross(father.Chromosomes[i]));
            }

            return mother.Instantiniate(chromosomes);
        }

        // Creates instance of genotype by copying itself
        public static Genotype<T> Replicate(Genotype<T> mother) {
            return mother.Instantiniate(mother.Chromosomes);
        }

        // Creates instance of genotype by randomizing new instance
        public static Genotype<T> Instantiniate(Genotype<T> mother) {
            int size = mother.Chromosomes.Count;
            List<Chromosome<T>> chromosomes = new List<Chromosome<T>>();
            for(int i = 0; i < size; i++)
            {
                chromosomes.Add(mother.Chromosomes[i].Initialize());
            }
            return mother.Instantiniate(chromosomes);
        }

    }
    */
}
