using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectAllele : MonoBehaviour
{
    public float Weight;
    public int ID;
    public decimal Value;
    public Sprite mySprite;
    public string Name;

    public Allele GetVanillaAllele()
    {
        return new Allele
        {
            Weight = this.Weight,
            ID = this.ID,
            Value = this.Value
        };
    }
}
