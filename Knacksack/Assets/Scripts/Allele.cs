public class Allele 
{
    public int ID;
    public float Weight;
    public decimal Value;
    public bool OnBag;

    public Allele Clone() => new Allele()
    {
        ID = ID,
        Weight = Weight,
        Value = Value,
        OnBag = OnBag
    };

    public override string ToString()
    {
        return $"| ID: {ID} - W: {Weight} - V: {Value} - OnBag: {OnBag} |";
    }
}
