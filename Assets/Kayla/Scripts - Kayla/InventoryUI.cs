using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{

    public GameObject slotPrefab; 
    
    public Transform slotParent; 

    private List<InventorySlotUI> currentSlots = new List<InventorySlotUI>();

    private void Start()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnInventoryUpdated.AddListener(RefreshInventorySlots);
        }
    }

    public void RefreshInventorySlots()
    {

        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager instance is null!");
            return;
        }

        List<ItemData> currentItems = InventoryManager.Instance.GetItems();
        Debug.Log($"Current items count: {currentItems.Count}");

        foreach (InventorySlotUI slot in currentSlots)
        {
            if (slot != null && slot.gameObject != null)
                Destroy(slot.gameObject);
        }
        currentSlots.Clear();

        for (int i = 0; i < currentItems.Count; i++)
        {
            Debug.Log($"Creating slot for item: {currentItems[i].itemName}");

            GameObject newSlotObject = Instantiate(slotPrefab, slotParent);
            newSlotObject.SetActive(true);
            newSlotObject.name = $"Slot_{i}_{currentItems[i].itemName}";

            InventorySlotUI newSlotUI = newSlotObject.GetComponent<InventorySlotUI>();

            if (newSlotUI != null)
            {
                newSlotUI.SetItem(currentItems[i]);
                currentSlots.Add(newSlotUI);
                Debug.Log($"Created slot for {currentItems[i].itemName}");
            }
        }
    }

}