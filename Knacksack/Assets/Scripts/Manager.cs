using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private Darwin DarwinFromBahia;
    public List<GameObjectAllele> Items;

    private void Awake()
    {
        DarwinFromBahia = new Darwin(Items.Select(x => x.GetVanillaAllele()).ToList());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
