using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIController : MonoBehaviour
{
    private GameObject canvas;
    public GameObject lockPrefab;
    // Start is called before the first frame update

    void Awake()
    {
        canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            Debug.LogError("Failed to find Canvas object.");
        }
        else
        {
            Debug.Log(canvas.transform);
        }
        if (lockPrefab == null)
        {
            Debug.LogError("Failed to find lockPrefab object.");
        }
    }

    void Start()
    {

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
