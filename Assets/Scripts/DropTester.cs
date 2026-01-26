using UnityEngine;

public class DropTester : MonoBehaviour
{
    public Player player;
    public int testSlotID = 0;
    public float dropOffset = 0.8f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 dropPos = player.transform.position + (Vector3)(Random.insideUnitCircle.normalized * dropOffset);

            if (player.inventory.Drop(testSlotID, dropPos))
            {
                // Refresh inventory UI if you have it
                var ui = FindFirstObjectByType<Inventory_UI>();
                if (ui != null) ui.SendMessage("Refresh", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
