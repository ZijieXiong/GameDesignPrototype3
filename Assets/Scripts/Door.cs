using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Player player;
    private BoxCollider2D boxCollider;
    private SpriteRenderer sprite;

    public Sprite openDoor;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int numOfKey = player.GetNumOfKeys();
        if(numOfKey >= 4)
        {
            boxCollider.isTrigger = true;
            sprite.sprite = openDoor;
        }
        else{
            boxCollider.isTrigger = false;
        }
    }
}
