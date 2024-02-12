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
    // Start is called before the first frame update
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
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
         foreach (var exitToScene in exitsToScenes)
    {
        if (other.CompareTag(exitToScene.exitTag))
        {
            SceneManager.LoadScene(exitToScene.sceneName);
            return;
        }
    }
    }
}
