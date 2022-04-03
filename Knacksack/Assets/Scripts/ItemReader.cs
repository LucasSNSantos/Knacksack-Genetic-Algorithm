using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReader : MonoBehaviour
{
    private Camera mainCam;
    public ItemInfo itemInfo;

    void Start()
    {
        mainCam = Camera.main;

        itemInfo.gameObject.SetActive(false);
    }

    void Update()
    {
        var mouse2d = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var mousePosition = mainCam.ScreenToWorldPoint(mouse2d);

        var hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            var gameObjectAllele = hit.collider.GetComponent<GameObjectAllele>();

            if (gameObjectAllele != null)
            {
                itemInfo.gameObject.SetActive(true);
                itemInfo.Set(gameObjectAllele);
            } else
            {
                itemInfo.gameObject.SetActive(false);
            }
        } else
        {
            itemInfo.gameObject.SetActive(false);
        }
    }
}
