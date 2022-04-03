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
	public readonly static float MaxWeight = 15f; 
    public const int Population = 10;
	public List<Allele> ObjectList;

    public Darwin(List<Allele> myObjects)
    {
		ObjectList = myObjects;
        Chromosomes = new Chromosome[Population];
    }

	public Chromosome Crossing(Chromosome father, Chromosome mother)
	{
		var son = new Chromosome();
		var offSet = Population / 2;
		//TODO colocar em um for só, comuna seu patriota burro
		for(int index = 0; index <= offSet - 1; index++)
        {
			son.Alleles[index] = father.Alleles[index];
        }
		for (int index = offSet; index <= Population - 1; index++)
		{
			son.Alleles[index] = mother.Alleles[index];
		}

		return son;
	}

	private (Chromosome father, Chromosome mother) Roulette()
	{
		throw new NotImplementedException();
		//return (null, null);
	}

	private (Chromosome father, Chromosome mother) Tournament()
	{
		foreach(var chrome in Chromosomes)
        {
			chrome.IsChoosen = false;
        }
		return (GetOneBetter(), GetOneBetter());
	}
	private Chromosome GetOneBetter()
	{
		var rnd = new System.Random();
		var index1 = rnd.Next(0, Population);
		var index2 = rnd.Next(0, Population);

		while(!Chromosomes[index1].IsChoosen)
        {
			index1 = rnd.Next(0, Population);
		}

		while (index1 == index2 && !Chromosomes[index2].IsChoosen)
		{
			index2 = rnd.Next(0, Population);
		}

		var betterSurvives = Chromosomes[index1].CompareTo(Chromosomes[index2]);

		if (betterSurvives > 1)
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
