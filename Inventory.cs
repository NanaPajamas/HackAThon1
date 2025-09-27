using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    public Item cane;
    private Item selectedItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cane != null)
            items.Add(cane);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
