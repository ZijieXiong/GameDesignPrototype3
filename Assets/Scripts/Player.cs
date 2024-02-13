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


public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public ExitToScene[] exitsToScenes;
    private Rigidbody2D rb;
    private float keys;
    private int key;
    private string lastScene;
    private string currentScene;
    public float keysNeeded;
    public GameObject keyImage;
    // Start is called before the first frame update
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        key = PlayerPrefs.GetInt("NumOfKey", 0);
        Debug.Log(key);
        lastScene = PlayerPrefs.GetString("LastScene", "");
        currentScene = SceneManager.GetActiveScene().name;
        Debug.Log(lastScene);
        Debug.Log(currentScene);
        keyImage.SetActive(false);
        if(lastScene != "" )
        {
            if(currentScene[0] != lastScene[0])
            {   
                Debug.Log("Entering new level");
                key = 0;
                PlayerPrefs.SetInt("NumOfKey", key);
            }
        }
        keys = 0f;
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
            Destroy(other.gameObject);
            keyImage.SetActive(true);
            return;
        }
        foreach (var exitToScene in exitsToScenes)
        {   
            Debug.Log("comparing");
            if (other.CompareTag(exitToScene.exitTag))
            {
                PlayerPrefs.SetInt("NumOfKey", key);
                PlayerPrefs.SetString("LastScene", currentScene);
                SceneManager.LoadScene(exitToScene.sceneName);
                return;
            }
        }
    }

    private void clearPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("NumOfKey");
        PlayerPrefs.DeleteKey("LastScene");
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        clearPlayerPrefs();
    }
}
