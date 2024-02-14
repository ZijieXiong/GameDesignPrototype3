using System.Collections;
using System.Collections.Generic;
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


public class UIController : MonoBehaviour
{
    private GameObject canvas;
    private Player player;
    public GameObject lockPrefab;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLockUI(int numOfKey)
    {
        int numOfLock = 4 - numOfKey;
        foreach (Transform child in canvas.transform)
        {
            if (child.gameObject.CompareTag("LockUI"))
            {
                Destroy(child.gameObject);
            }
        }
        for (int i = 0; i < numOfLock; i++)
        {
            GameObject heart = Instantiate(lockPrefab, canvas.transform);
            heart.tag = "LockUI";

            RectTransform rect = heart.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector3(-400 + (rect.sizeDelta.x * i * 1.2f), 200, -3);
        }
    }
}
