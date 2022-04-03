using System;
using System.Linq;
using UnityEngine;

public class Chromosome
{
    private static int _ID = 1;
    public int ID;
    public int Size => myDarwin.ObjectList.Count;
    public Allele[] Alleles;
    public bool IsChoosen;
    public bool Executed;
    public static System.Random random;

    private Darwin myDarwin;

    public Chromosome(Darwin myDarwin)
    {
        ID = _ID;
        
        _ID++;

        if (random == null)
        {
            random = new System.Random();
        }

        this.myDarwin = myDarwin;
        Alleles = new Allele[Size];
    }

    public Chromosome Randomize()
    {
        var objectList = myDarwin.ObjectList;
        var totalWeight = 0.0f;
        
        for (int i = 0; i < Alleles.Length; i++)
        {
            Alleles[i] = objectList.ElementAt(i).Clone();

            var randomNumber = random.Next(0, 10);
            var onBag = randomNumber <= 4;

            if (onBag && Alleles[i].Weight + totalWeight <= Darwin.MaxWeight)
            {
                Alleles[i].OnBag = true;
                totalWeight += Alleles[i].Weight;
            }
        }

        return this;
    }

    public float TotalWeight() => Alleles.Where(x => x.OnBag).Sum(x => x.Weight);

    public decimal TotalValue() => Alleles.Where(x => x.OnBag).Sum(x => x.Value);

    public float Fitness()
    {
        float TotalWeight = Alleles.Where(x => x.OnBag).Sum(x => x.Weight);
        decimal TotalValue = Alleles.Where(x => x.OnBag).Sum(x => x.Value);

        var stepWeight = TotalWeight / Darwin.MaxWeight; // Mathf.Lerp(0, Darwin.MaxWeight, TotalWeight / Darwin.MaxWeight);

        if (stepWeight > 1 || TotalWeight == 0)
        {
            return 0;
        }
        else
        {
            return (float) TotalValue / stepWeight;
        }
    }

    public override string ToString()
    {
        var asString = "Chromossome fitness: " + Fitness() + "\n";
        foreach(var allele in Alleles)
        {
            asString += allele.ToString() + "\n";
        }
        return asString;
    }

    public int CompareTo(Chromosome chromossome)
    {
        return Fitness().CompareTo(chromossome.Fitness());
    }

}
