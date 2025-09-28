using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<Item> items;
    public Item[] startItems;
    private Item selectedItem;
    public ItemEquip itemEquip;

    public RectTransform[] itemSlots;

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
        foreach (Item startItem in startItems)
        {
            if (startItem != null)
                AddItem(startItem);
        }

        SetSelectedItem(items[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(nextItemKey))
            SwitchItem(true);
        else if (Input.GetKeyDown(prevItemKey))
            SwitchItem(false);
    }

    private void AddItem(Item item)
    {
        items.Add(item);

        int index = items.IndexOf(item);
        RectTransform itemSlot = itemSlots[index];

        Image itemImage = itemSlot.GetComponentInChildren<Image>();
        itemImage.gameObject.SetActive(true);
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
                SetSelectedItem(nextItem);
        }
        else
        {
            int prevIndex = currentItemIndex - 1;
            if (prevIndex < 0)
                prevIndex += items.Count;

            Item prevItem = items[prevIndex];
            if (prevItem != null)
                SetSelectedItem(prevItem);
        }
    }

    public Item GetSelectedItem()
    {
        return selectedItem;
    }

    private void SetSelectedItem(Item item)
    {
        selectedItem = item;
        int index = items.IndexOf(selectedItem);

        RectTransform slotTransform = itemSlots[index];
        SelectableImage selectableImage = slotTransform.GetComponentInChildren<SelectableImage>();
        selectableImage.Select();

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i != index)
            {
                RectTransform otherTransform = itemSlots[i];
                SelectableImage otherSelectableImage = otherTransform.GetComponentInChildren<SelectableImage>();
                otherSelectableImage.Deselect();
            }
        }

        if (itemEquip != null)
            itemEquip.EquipItem(selectedItem);
    }
}
