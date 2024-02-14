using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Player player;
    private BoxCollider2D boxCollider;
    private SpriteRenderer sprite;

    public Sprite openDoor;

    public GameObject messageText; // Assign text in the Unity Editor
    private bool showMessage = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        messageText.SetActive(false);
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

        if (showMessage)
        {
            messageText.SetActive(true); // Show the UI text when player enters the trigger zone
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            showMessage = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            showMessage = false;
            messageText.SetActive(false); // Hide the UI text when player exits the trigger zone
        }
    }
}
