using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI GeracoesText;
    public List<Sack> Sacks;
    public Sack Descedent;

    public void CloseAllExceptFor(int id)
    {
        foreach (var sack in Sacks)
        {
            if (sack.ID == id) continue;
            sack.gameObject.SetActive(false);
        }
    }

    public void OpenAll()
    {
        foreach (var sack in Sacks)
        {
            sack.gameObject.SetActive(true);
        }
    }

    public void SetDescendent(Chromosome chromosome)
    {
        Descedent.gameObject.SetActive(true);
        Descedent.SetChromossome(chromosome);
    }

    public void RemoveDescedent()
    {
        Descedent.gameObject.SetActive(false);
    }
}
