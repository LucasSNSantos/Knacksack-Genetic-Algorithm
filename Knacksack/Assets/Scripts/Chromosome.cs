using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chromosome
{
    public static int Size = 10;
    public Allele[] Alleles;
    public bool IsChoosen;
    public Chromosome()
    {
        Alleles = new Allele[Size];
    }

    // f(n) = 0.01 - 0.99
    public float Fitness()
    {
        float TotalWeight = Alleles.Sum(x => x.Weight);
        decimal TotalValue = Alleles.Sum(x => x.Value);

        var stepWeight = Mathf.Lerp(0, Darwin.MaxWeight, TotalWeight);
        if(stepWeight > 1 )
        {
            return 0;
        }
        else
        {
            return (float) TotalValue / stepWeight;
        }
    }

    public int CompareTo(Chromosome chromossome)
    {
        var myFitness = Fitness();
        var otherFitness = chromossome.Fitness();
        return myFitness.CompareTo(otherFitness);
    }

}
