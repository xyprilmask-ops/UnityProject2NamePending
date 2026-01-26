using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public CollectableType type;
        public int count;

        public int MaxAllowed;

        public Sprite icon; 

        public Slot()
        {
            type = CollectableType.NONE;
            count = 0;
            MaxAllowed = 99;
        }

        public bool CanAddItem()
        {
            if (count < MaxAllowed)
            {
                return true;
            }

            return false;
        }
    

        public void AddItem(Collectable item)
        {
            this.type = item.type;
            this.icon = item.icon;
            count++;
        }

        public void RemoveItem()
        {
            if (count > 0)
            {
                count--;
                if (count == 0)
                {
                    type = CollectableType.NONE;
                    icon = null;
                }
            }
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void Add(Collectable item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.type == item.type && slot.CanAddItem())
            {
                slot.AddItem(item);
                return;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.type == CollectableType.NONE)
            {
                slot.AddItem(item);
                return;
            }
        }
        Debug.LogWarning("Inventory full, cannot add item of type: ");
    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }
}

public partial class Inventory
{
    public GameObject droppedItemPrefab; // assign in Inspector (or via GameManager)

    public bool Drop(int slotID, Vector3 dropPosition)
    {
        if (slotID < 0 || slotID >= slots.Count) return false;

        var slot = slots[slotID];
        if (slot.type == CollectableType.NONE || slot.count <= 0) return false;

        // You need a reference to the Collectable object for this slot.
        // If your InventorySlot only stores "type", you must have a lookup somewhere.
        // Example: Collectable item = GameManager.instance.itemManager.GetItemByType(slot.type);
        Collectable item = GameManager.instance.itemManager.GetItemtype(slot.type); // <-- adapt if needed

        if (item == null)
        {
            Debug.LogError($"Drop failed: no Collectable found for type {slot.type}");
            return false;
        }

        // Spawn world drop
        if (droppedItemPrefab == null)
        {
            Debug.LogError("Drop failed: droppedItemPrefab not assigned on Inventory.");
            return false;
        }

        var go = Object.Instantiate(droppedItemPrefab, dropPosition, Quaternion.identity);
        var worldDrop = go.GetComponent<WorldDrop>();
        if (worldDrop != null)
            worldDrop.Init(item, slot.count);

        // Remove from inventory
        slot.type = CollectableType.NONE;
        slot.count = 0;
        slots[slotID] = slot;

        return true;
    }
}
