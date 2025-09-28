using UnityEngine;

public class ItemEquip : MonoBehaviour
{
    private GameObject currentEquippedItem;
    public RectTransform itemHolder;

    public void EquipItem(Item item)
    {
        if (currentEquippedItem != null)
            DestroyImmediate(currentEquippedItem);

        if (item != null && item.equippedPrefab != null)
        {
            currentEquippedItem = Instantiate(item.equippedPrefab, itemHolder);
            RectTransform equippedItemTransform = currentEquippedItem.GetComponent<RectTransform>();
            equippedItemTransform.localPosition = new Vector3(0, 0, 0);            
        }
    }

    public GameObject GetEquippedItem()
    {
        return currentEquippedItem;
    } 
}