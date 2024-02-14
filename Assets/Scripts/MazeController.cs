using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

public class MazeController : MonoBehaviour
{
    public ExitToScene[] exitsToScenes;
    public SpawnPoint[] spawnPoints;

    private string lastScene;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetPlayerSpawnPosition(Vector3 playerPosition)
    {
        Vector3 res = playerPosition;
        lastScene = PlayerPrefs.GetString("LastScene", "");
        if(spawnPoints != null)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                if(lastScene == spawnPoint.lastScene)
                {
                    res = spawnPoint.spawnPoint.position;
                }
            }
        }
        return res;
    }
    public string GetNextScene(Collider2D other)
    {
        foreach (var exitToScene in exitsToScenes)
        {   
            if (other.CompareTag(exitToScene.exitTag))
            {
                return exitToScene.sceneName;
            }
        }
        return "";
    }
}
