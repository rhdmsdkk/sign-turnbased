using UnityEngine;
using UnityEngine.Events;

public class ItemPickUpHandler : MonoBehaviour
{
    public ItemData itemToGive;

    private Interactable linkedInteractable;

    private void Start()
    {
        linkedInteractable = GetComponent<Interactable>();

        if (itemToGive == null)
        {
            Debug.LogError($"ItemPickupHandler on {gameObject.name} is missing ItemData! Cannot pick up.");
            return;
        }

        if (linkedInteractable == null)
        {
            Debug.LogError($"ItemPickupHandler requires an Interactable component on the same GameObject.");
            return;
        }

        linkedInteractable.onDialogueCompleted.AddListener(PerformPickup);

        Debug.Log($"Item Pickup Handler initialized for item: {itemToGive.itemName}");
    }

    private void PerformPickup()
    {
        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager instance is not found. Cannot add item.");
            return;
        }

        InventoryManager.Instance.AddItem(itemToGive);
        Debug.Log($"[Pickup] Successfully added item: {itemToGive.itemName}");
        gameObject.SetActive(false);
    }
}
