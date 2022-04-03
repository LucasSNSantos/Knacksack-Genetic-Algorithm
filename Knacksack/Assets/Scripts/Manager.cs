using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Flags]
public enum AGState
{
    KillWeak = 0,
    Tournament,
    Crossing ,
    Swap,
    ChooseMutation,
    Mutation,
    FimGeneration
}

public class Manager : MonoBehaviour
{
    private Darwin MyD;
    public List<GameObjectAllele> Items;
    public List<Sack> Sacks;

    public UIManager UI;

    public int Generations = 20;

    private bool IsAutoOn = false;

    public float StepTime = 1f;

    private float CurrentTimer = 0f;

    private Coroutine CurrentCoroutine;
    private AGState state;

    private (Chromosome father, Chromosome mother) Participants;
    private Chromosome Descendent;

    private void Awake()
    {
        MyD = new Darwin(Items.Select(x => x.GetVanillaAllele()).ToList(), Generations);

        print("POPULACAO INICIAL");
        foreach(var chromossome in MyD.Chromosomes)
        {
            print(chromossome.ToString());
        }
        print("FIM POP INICIAL");

        int count = 0;
        foreach(var chromossome in MyD.Chromosomes)
        {
            var sack = Sacks.ElementAt(count);
            sack.SetChromossome(chromossome);
            count++;
        }

        UI.GeracoesText.text = "GERACAO 1" + "/" + MyD.MaxGenerations + " ESTADO INCIAL";
    }

    void Start()
    {
        //MyD.OnCrossing = (c) =>
        //{
        //    print("Resultado do cruzamento: " + c.ToString());
        //};

        //MyD.OnKilled = (c) =>
        //{
        //    print("Individuo mandado ao gulag: " + c.ToString());
        //};

        //MyD.OnMutated = (c) =>
        //{
        //    print("Individuo mutado: " + c.ToString());
        //};

        //MyD.OnTournament = (f, m) =>
        //{
        //    print("Torneio!");
        //    print(f.ToString());
        //    print(m.ToString());
        //    print("Fim torneio");
        //};

        //StartCoroutine(GeneticCoroutine());
    }

    public void EvolveOneGeneration()
    {
        if (MyD.CurrentGeneration + 1 == MyD.MaxGenerations)
        {
            UI.GeracoesText.text = "O MAXIMO DE GERACOES FOI ALCANÇADO (" + MyD.MaxGenerations + ")";
            IsAutoOn = false;
            return;
        }

        string prefix = "GERACAO " + (MyD.CurrentGeneration+1) + "/" + MyD.MaxGenerations;

        switch (state)
        {
            case AGState.KillWeak:
                MyD.KillWeak();
                HandleKillWeak();
                break;
            case AGState.Tournament:
                Participants = MyD.Tournament();
                HandleTournament();
                break;
            case AGState.Crossing:
                Descendent = MyD.Crossing(Participants.father, Participants.mother);
                UI.SetDescendent(Descendent);
                break;
            case AGState.Swap:
                HandleSwap();
                MyD.SwapDescendent(Descendent);
                break;
            case AGState.ChooseMutation:
                MyD.ChooseWhoMutates();
                HandleWhoMutates();
                break;
            case AGState.Mutation:
                MyD.FullMutation(MyD.CurrentGeneration);
                HandleMutation();
                break;
            case AGState.FimGeneration:
                MyD.CurrentGeneration++;
                foreach (var sack in Sacks)
                {
                    sack.ResetStatus();
                }

                break;
        }

        int count = 0;
        foreach (var chromossome in MyD.Chromosomes)
        {
            var sack = Sacks.ElementAt(count);
            sack.SetChromossome(chromossome);
            count++;
        }

        UI.GeracoesText.text = prefix + " ESTADO " + Enum.GetName(typeof(AGState), state);
    }

    private void HandleKillWeak()
    {
        var weak = MyD.Chromosomes.FirstOrDefault(x => x.Executed).ID;

        Sacks.FirstOrDefault(x => x.ID == weak).SetExecuted();
    }

    private void HandleTournament()
    {
        var par1 = Participants.father.ID;
        var par2 = Participants.mother.ID;

        Sacks.FirstOrDefault(x => x.ID == par1).SetParticipant(true, 0);
        Sacks.FirstOrDefault(x => x.ID == par2).SetParticipant(true, 1);
    }

    private void HandleSwap()
    {
        var weak = MyD.Chromosomes.FirstOrDefault(x => x.Executed);

        var swapper = Sacks.FirstOrDefault(x => x.ID == weak.ID);

        swapper.ResetStatus();
        //swapper.SetChromossome(Descendent);

        UI.RemoveDescedent();
    }

    private void HandleWhoMutates()
    {
        var mutate = MyD.HasToMutate;

        var sack = Sacks.FirstOrDefault(x => x.ID == mutate.ID);

        sack.SetMutation(true);
    }

    private void HandleMutation()
    {
        var mutate = MyD.HasToMutate;

        var sack = Sacks.FirstOrDefault(x => x.ID == mutate.ID);

        sack.SetMutation(false);

        //sack.SetChromossome(mutate);
        sack.ResetStatus();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !IsAutoOn)
        {
            Step();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            IsAutoOn = !IsAutoOn;

            //if (IsAutoOn)
            //{
            //    CurrentCoroutine = StartCoroutine(StepCoroutine());
            //} else
            //{
            //    if (CurrentCoroutine != null)
            //    {
            //        StopCoroutine(CurrentCoroutine);
            //    }
            //}
        }

        if (IsAutoOn)
        {
            CurrentTimer += Time.deltaTime;

            if (CurrentTimer >= StepTime)
            {
                Step();
                CurrentTimer = 0;
            }
        }
    }

    private IEnumerator StepCoroutine()
    {
        Step();

        yield return new WaitForSecondsRealtime(StepTime);
    
        if (IsAutoOn)
        {
            StartCoroutine(StepCoroutine());
        }
    }

    private void Step()
    {
        EvolveOneGeneration();

        print((int)state);

        if (state == AGState.FimGeneration)
        {
            state = AGState.KillWeak;
        }
        else
        {
            state = (AGState)((int)state + 1);
        }
    }

    public IEnumerator GeneticCoroutine()
    {
        var counter = 0;
        foreach(var chromosome in MyD.Genetic(Generations))
        {
            print("GERACAO: " + counter++);
            print("Gerado chromossome: " + chromosome.ToString());
            yield return new WaitForSeconds(0.5f);
        }
    }
}
