using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome
{
    public static int Size = 10;
    public Allele[] Alleles;

    public Chromosome()
    {
        Alleles = new Allele[Size];
    }
    
    public float Fitness()
    {
        return 0;
    }

    public int CompareTo(Chromosome chromossome)
    {
        var myFitness = this.Fitness();
        var otherFitness = chromossome.Fitness();
        return myFitness.CompareTo(otherFitness);
    }

}
