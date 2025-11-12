using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventorySlotUI : MonoBehaviour
{
    public Image itemIconImage;       
    public TMPro.TextMeshProUGUI itemNameText;

    private ItemData currentItemData; 

    public void SetItem(ItemData itemData) 
    {
        currentItemData = itemData;
        itemNameText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        
        if (itemData.icon != null)
        {
            itemIconImage.sprite = itemData.icon;
            itemIconImage.enabled = true;
        }
        else
        {
            itemIconImage.enabled = false;
        }

        itemNameText.text = itemData.itemName;
        
        gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        currentItemData = null;
        itemIconImage.sprite = null;
        itemIconImage.enabled = false;
        itemNameText.text = "";
    }
}