using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Player player;

    [Header("Drag the parent object that contains all slot UI objects")]
    public Transform slotsParent;

    private List<Slots_UI> slots = new List<Slots_UI>();

    private void Awake()
    {
        // Auto-load all Slots_UI components under slotsParent
        if (slotsParent != null)
            slots = new List<Slots_UI>(slotsParent.GetComponentsInChildren<Slots_UI>(true));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            ToggleInventory();
    }

    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            Refresh();
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }

    void Refresh()
    {
        if (player == null || player.inventory == null)
        {
            Debug.LogError("Inventory_UI.Refresh: Player or Player.inventory is NULL. Assign Player in Inspector.");
            return;
        }

        if (slots == null || slots.Count == 0)
        {
            Debug.LogError("Inventory_UI.Refresh: slots list is empty. Assign slotsParent or populate slots.");
            return;
        }

        int count = Mathf.Min(slots.Count, player.inventory.slots.Count);

        for (int i = 0; i < count; i++)
        {
            if (slots[i] == null)
            {
                Debug.LogError($"Inventory_UI.Refresh: slots[{i}] is NULL (missing Slots_UI component or broken reference).");
                continue;
            }

            var invSlot = player.inventory.slots[i];

            if (invSlot.type != CollectableType.NONE && invSlot.count > 0)
                slots[i].SetItem(invSlot);
            else
                slots[i].SetEmpty();
        }
    }

    public void Remove(int slotID)
    {
        Collectable itemToDrop = GameManager.instance.itemManager.GetItemByType(
            player.inventory.slots[slotID].type);
        if (itemToDrop != null)
        {
            player.DropItem(itemToDrop);
            player.inventory.Remove(slotID);
            Refresh();
        }
    }
}

