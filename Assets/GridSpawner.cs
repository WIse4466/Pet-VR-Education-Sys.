using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public ObjectPool cellPool;
    public int rows = 5;
    public int columns = 5;
    public float spacing = 2f;
    public GameObject terrain;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < rows * columns; i++)
        {
            Vector3 spawnPosition = new Vector3(i * spacing, 0, i * spacing);
            Instantiate(terrain, spawnPosition, Quaternion.identity);
        }
        //SpawnGrid();
    }

    void SpawnGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                Vector3 spawnPosition = new Vector3(j * spacing, 0, i * spacing);
                Quaternion spawnRotation = Quaternion.identity;

                GameObject cell = cellPool.GetObjectFromPool(spawnPosition, spawnRotation);
            }
        }
    }
}
