using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    public Item cane;
    private Item selectedItem;

    [Header("Inventory Controls")]
    public KeyCode nextItemKey;
    public KeyCode prevItemKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cane != null)
        {
            items.Add(cane);
            selectedItem = cane;            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(nextItemKey))
            SwitchItem(true);
        else if (Input.GetKeyDown(prevItemKey))
            SwitchItem(false);
    }

    private void SwitchItem(bool next)
    {
        int currentItemIndex = items.IndexOf(selectedItem);

        if (next)
        {
            int nextIndex = currentItemIndex + 1;
            if (nextIndex >= items.Count)
                nextIndex -= items.Count;

            Item nextItem = items[nextIndex];
            if (nextItem != null)
                selectedItem = nextItem;
        }
        else
        {
            int prevIndex = currentItemIndex - 1;
            if (prevIndex < 0)
                prevIndex += items.Count;

            Item prevItem = items[prevIndex];
            if (prevItem != null)
                selectedItem = prevItem;
        }
    }
}
