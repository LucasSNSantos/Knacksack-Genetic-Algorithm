using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public List<GameObjectAllele> Objects;

    public Manager manager;

    private void Awake()
    {
        Objects = manager.Items;
    }

    public void Spawn(Allele[] alleles)
    {
        foreach(var obje in Objects)
        {
            if (alleles.FirstOrDefault(x => x.ID == obje.ID).OnBag)
            {
                var rigidbody = obje.GetComponent<Rigidbody2D>();
                rigidbody.gravityScale = 1;
            } else
            {
                obje.gameObject.SetActive(false);
            }
        }
    }

    public void RemoveFromScene()
    {
        foreach(var obje in Objects)
        {
            obje.GetComponent<Rigidbody2D>().gravityScale = 0;
            obje.transform.position = obje.InitialPosition.position;
            obje.transform.rotation = obje.InitialPosition.rotation;
        }
    }
}
