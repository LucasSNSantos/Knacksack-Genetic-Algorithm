using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI WeightText;
    public TextMeshProUGUI ValueText;

    public void Set(GameObjectAllele allele)
    {
        NameText.text = allele.Name;
        WeightText.text = allele.Weight + "kg";
        ValueText.text = allele.Value + "g";
    }
}
