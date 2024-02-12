using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    // Start is called before the first frame update
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key")) {
            keys++;
            Destroy(other.gameObject);
        }
        foreach (var exitToScene in exitsToScenes)
        {   
            if (other.CompareTag(exitToScene.exitTag) && keys != 0)
            {
                SceneManager.LoadScene(exitToScene.sceneName);
                return;
            }
        }
    }
}