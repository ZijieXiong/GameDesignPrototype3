using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Player player;
    private BoxCollider2D boxCollider;
    private SpriteRenderer sprite;

    public Sprite openDoor;

    public AudioClip doorSound;
    private AudioSource audioSource;
    private GameObject messageText; // Assign text in the Unity Editor

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        messageText = GameObject.Find("Canvas").transform.Find("DoorMessage").gameObject;
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(doorSound);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            messageText.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            messageText.SetActive(false); // Hide the UI text when player exits the trigger zone
        }
    }
}
