using UnityEngine;

public class WorldDrop : MonoBehaviour
{
    [Header("Runtime data")]
    public Collectable item;
    public int count = 1;

    [Header("Optional visuals")]
    public SpriteRenderer spriteRenderer;

    public void Init(Collectable newItem, int newCount)
    {
        item = newItem;
        count = newCount;

        if (spriteRenderer != null && item != null)
            spriteRenderer.sprite = item.icon; // uses your Collectable.icon
    }
}
