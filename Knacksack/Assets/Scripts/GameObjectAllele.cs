using UnityEngine;

public class GameObjectAllele : MonoBehaviour
{
    public int ID;
    public float Weight;
    public float Value;
    public Sprite mySprite;
    public string Name;

    public Vector3 InitialPosition;
    public Quaternion InitialRotation;

    private void Awake()
    {
        InitialPosition = transform.position;
        InitialRotation = transform.rotation;
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
