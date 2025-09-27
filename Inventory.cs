using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<Item> items;
    public Item startItem;
    private Item selectedItem;

    [Header("Inventory Controls")]
    public KeyCode nextItemKey;
    public KeyCode prevItemKey;

    void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (startItem != null)
        {
            items.Add(startItem);
            selectedItem = startItem;
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

    public Item GetSelectedItem()
    {
        return selectedItem;
    }
}
