using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    [SerializeField] private GameObject inventoryUIPanel;
    
    private List<ItemData> items = new List<ItemData>();
    private bool isInventoryOpen = false;
    public UnityEvent OnInventoryUpdated;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            if (inventoryUIPanel != null)
            {
                isInventoryOpen = inventoryUIPanel.activeInHierarchy;
                Debug.Log($"Initial inventory state: {(isInventoryOpen ? "OPEN" : "CLOSED")}");
            }
            else
            {
                Debug.LogError("Inventory UI Panel is not assigned in the Inspector on InventoryManager!");
            }
        }
    }

    
    public void ToggleInventory()
    {
        if (inventoryUIPanel == null) return;
        
        isInventoryOpen = !isInventoryOpen;
        inventoryUIPanel.SetActive(isInventoryOpen);

        Time.timeScale = isInventoryOpen ? 0f : 1f;

        Debug.Log("Inventory Toggled: " + (isInventoryOpen ? "OPEN" : "CLOSED"));
    }

    public void AddItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogError("Attempted to add a null item to inventory.");
            return;
        }

        items.Add(item);
        Debug.Log($"[Inventory] Added {item.itemName}. Total items: {items.Count}");

        OnInventoryUpdated.Invoke(); 
    }

    public List<ItemData> GetItems()
    {
        return items;
    }
}