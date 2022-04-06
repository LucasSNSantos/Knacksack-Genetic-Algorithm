using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public enum ESelection
{
    Torneio,
    Roleta
}

public class Darwin
{
    public Chromosome[] Chromosomes;
    public static ESelection Selection;
	public readonly static float MaxWeight = 15f;
    public const int Population = 10;
	public float MutationTax = 0.02f;
    public int CurrentGeneration;
    public int MaxGenerations;
	public List<Allele> ObjectList { get; private set; }
	private static Random random;
    public Chromosome HasToMutate;

    public Darwin(List<Allele> myObjects, int maxGenerations = 30)
    {
		random = new Random();
        MaxGenerations = maxGenerations;

		ObjectList = myObjects;
        Chromosomes = new Chromosome[Population];

		FillPopulation();
    }

	public IEnumerable<Chromosome> Genetic(int generations)
    {
        for (int i = 0; i < generations; i++)
        {
            // selecao
            // elimina o cromossomo mais fraco
            KillWeak();

            // torneio
            var (father, mother) = Tournament();

            // crossing
            var descendent = Crossing(father, mother);

            SwapDescendent(descendent);

            // mutacao
            ChooseWhoMutates();
            // todo revisar issae
            FullMutation(i);

            yield return descendent;
        }
    }

    public void ChooseWhoMutates()
    {
        HasToMutate = Chromosomes.OrderBy(x => random.Next()).First(); 
    }

    public void FullMutation(int i)
    {
        if (i % 7 == 0)
        {
            Mutate(HasToMutate);
        }
    }

    public void SwapDescendent(Chromosome descendent)
    {
        for (int j = 0; j < Chromosomes.Length; j++)
        {
            if (Chromosomes[j].Executed)
            {
                Chromosomes[j] = descendent;
            }
        }
    }

    public void FillPopulation()
    {
        for (int index = 0; index < Chromosomes.Length; index++)
		{
			Chromosomes[index] = new Chromosome(this).Randomize();
		}
    }

	private void Mutate(Chromosome chromosome)
    {
		var first = chromosome.Alleles.OrderBy(x => random.Next()).First();
		first.OnBag = !first.OnBag;
    }

	public Chromosome Crossing(Chromosome father, Chromosome mother)
	{
		var son = new Chromosome(this);
		var offSet = random.Next(2, son.Size - 3);
		
		for(int index = 0; index < son.Size; index++)
        {
			if (index < offSet)
            {
				son.Alleles[index].OnBag = father.Alleles[son.Size - index - 1].OnBag;
            } else
            {
				son.Alleles[index].OnBag = mother.Alleles[index - offSet].OnBag;
            }
		}

		return son;
	}

	private (Chromosome father, Chromosome mother) Roulette()
	{
		throw new NotImplementedException();
		//return (null, null);
	}

	public (Chromosome father, Chromosome mother) Tournament()
    {
        ResetChoosen();
        return (GetOneBetter(), GetOneBetter());
    }

    public void ResetChoosen()
    {
        foreach (var chrome in Chromosomes)
        {
            chrome.IsChoosen = false;
        }
    }

    public void KillWeak()
    {
		Chromosome weaker = Chromosomes.First();

		foreach(var chromo in Chromosomes)
        {
			if (chromo.Fitness() < weaker.Fitness())
            {
				weaker = chromo;
            }
        }

		weaker.Executed = true;
    }

	private Chromosome GetOneBetter()
	{
		var index1 = random.Next(0, Population);
		var index2 = random.Next(0, Population);

		while(Chromosomes[index1].IsChoosen || Chromosomes[index1].Executed)
        {
			index1 = random.Next(0, Population);
		}

		while (index1 == index2 || Chromosomes[index2].IsChoosen || Chromosomes[index2].Executed)
		{
			index2 = random.Next(0, Population);
		}

		var betterSurvives = Chromosomes[index1].CompareTo(Chromosomes[index2]);

		if (betterSurvives > 0)
		{
			Chromosomes[index1].IsChoosen = true;
			return Chromosomes[index1];
		}
		else
		{
			Chromosomes[index2].IsChoosen = true;
			return Chromosomes[index2];
		}
	}
}
