using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class ExitToScene
{
    public string exitTag;
    public string sceneName;
}

[System.Serializable]
public class SpawnPoint
{
    public string lastScene;
    public Transform spawnPoint;
}


public class Player : MonoBehaviour
{
    public float speed = 3f;
    public ExitToScene[] exitsToScenes;
    public SpawnPoint[] spawnPoints;
    private Rigidbody2D rb;
    private int key;
    private string lastScene;
    private string currentScene;
    public float keysNeeded;
    public AudioClip keyPickupSound; // Assign your key pickup sound in the Unity Editor
    private AudioSource audioSource;
    public static Player instance;

    private UIController uiControl;

    // Start is called before the first frame update
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        key = PlayerPrefs.GetInt("NumOfKey", 0);
        lastScene = PlayerPrefs.GetString("LastScene", "");
        currentScene = SceneManager.GetActiveScene().name;
        audioSource = GetComponent<AudioSource>();

        if(lastScene != "" )
        {
            if(currentScene[0] != lastScene[0])
            {   
                Debug.Log("Entering new level");
                key = 0;
                PlayerPrefs.SetInt("NumOfKey", key);
            }
        }
        //adjust player spawning position based on the last scene
        if(spawnPoints != null)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                if(lastScene == spawnPoint.lastScene)
                {
                    transform.position = spawnPoint.spawnPoint.position;
                }
            }
        }

        uiControl = GameObject.Find("UI Controller").GetComponent<UIController>();
        uiControl.UpdateLockUI(key);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 newPosition = rb.position + new Vector2(moveX, moveY) * speed * Time.fixedDeltaTime;
        
        rb.MovePosition(newPosition);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key")) {
            key++;
            PlayerPrefs.SetInt(currentScene+"KeyFound", 1);
            Destroy(other.gameObject);
            audioSource.PlayOneShot(keyPickupSound);
            uiControl.UpdateLockUI(key);
            return;
        }
        if(other.CompareTag("Treasure"))
        {
            clearPlayerPrefs();
            SceneManager.LoadScene("Winning");
            return;
        }
        foreach (var exitToScene in exitsToScenes)
        {   
            if (other.CompareTag(exitToScene.exitTag))
            {
                PlayerPrefs.SetInt("NumOfKey", key);
                PlayerPrefs.SetString("LastScene", currentScene);
                SceneManager.LoadScene(exitToScene.sceneName);
                return;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has collided with an enemy!");
            clearPlayerPrefs();
            SceneManager.LoadScene("Losing");
        }
    }

    private void clearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        clearPlayerPrefs();
    }

    public int GetNumOfKeys()
    {
        return key;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
