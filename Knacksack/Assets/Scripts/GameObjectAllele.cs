using UnityEngine;

public class GameObjectAllele : MonoBehaviour
{
    public int ID;
    public float Weight;
    public float Value;
    public Sprite mySprite;
    public string Name;

    public Transform InitialPosition;

    private void Awake()
    {
        InitialPosition = transform;
    }

    public Allele GetVanillaAllele()
    {
        return new Allele
        {
            Weight = Weight,
            ID = ID,
            Value = (decimal)Value
        };
    }
}
