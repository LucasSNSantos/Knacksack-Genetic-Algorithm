using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESelection
{
    Torneio,
    Roleta
}

public class Darwin
{
    public Chromosome[] Chromosomes;
    public static ESelection Selection;
    public const int Population = 10;

    public Darwin()
    {
        Chromosomes = new Chromosome[Population];
    }

	public Chromosome Crossing(Chromosome father, Chromosome mother)
	{
		var son = new Chromosome();
		var offSet = Population / 2;
		for(int index = 0; index <= offSet - 1; index++)
        {
			son.Alleles[index] = father.Alleles[index];
        }
		for (int index = offSet; index <= Population - 1; index++)
		{
			son.Alleles[index] = father.Alleles[index];
		}
		return son;
	}

	private (Chromosome, Chromosome) Roulette()
	{
		throw new NotImplementedException();
		//return (null, null);
	}

	private (Chromosome, Chromosome) Tournament()
	{
		// checar se os dois sao iguais?
		return (GetOneBetter(), GetOneBetter());
	}
	private Chromosome GetOneBetter()
	{
		var rnd = new System.Random();
		var index1 = rnd.Next(0, Population);
		var index2 = rnd.Next(0, Population);

		while (index1 == index2)
		{
			index2 = rnd.Next(0, Population);
		}

		var betterSurvives = Chromosomes[index1].CompareTo(Chromosomes[index2]);

		if (betterSurvives > 1)
		{
			return Chromosomes[index1];
		}
		else
		{
			return Chromosomes[index2];
		}
	}
}
