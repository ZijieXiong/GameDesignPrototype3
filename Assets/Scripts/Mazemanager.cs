using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mazemanager : MonoBehaviour
{
    public int rows;
    public int columns;
    public float tileSize;

    private List<Vector2> tilePositions;

    // Start is called before the first frame update
    void Start()
    {
        tilePositions = CalculateTilePositions(10, 18, tileSize);    
    }

    // Update is called once per frame
    void Update()
    {
        
 
    }
    public List<Vector2> CalculateTilePositions(int rows, int columns, float tileSize)
    {
        List<Vector2> tilePositions = new List<Vector2>();
        float startX = -(columns * tileSize) / 2 + tileSize / 2;
        float startY = -(rows * tileSize) / 2 + tileSize / 2;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                float x = startX + j * tileSize;
                float y = startY + i * tileSize;
                tilePositions.Add(new Vector2(x, y));
            }
        }

        return tilePositions;
    }
}
