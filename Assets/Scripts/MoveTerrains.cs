using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTerrains : MonoBehaviour
{
    public float moveDistance = 10.0f;
    public Terrain[] terrains;
    // Start is called before the first frame update
    void Start()
    {
        //Terrain[] terrains = FindObjectsOfType<Terrain>();

        foreach(Terrain terrain in terrains)
        {
            Vector3 terrainPosition = terrain.transform.position;
            terrainPosition.z += moveDistance;
            terrain.transform.position = terrainPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
