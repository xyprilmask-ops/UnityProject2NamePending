using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;


public class Collectable : MonoBehaviour
{
    //player walks into collectable
    //add collectable to player inventory
    //delete collectable from scene

    public CollectableType type;
    public Sprite icon;

    public Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            player.inventory.Add(this);
            Destroy(this.gameObject);
        }
    }
}

public enum CollectableType
{
    NONE, CANNABIS_SEEDBAG, KHAT_SEEDBAG, PLANTER_POTBAG
}