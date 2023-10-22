using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManagerSkin : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject[] dogs;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject dog in dogs)
        {
            GameObject cloneDog = Instantiate(dog);
            cloneDog.GetComponent<DogWandering>().waypoints = waypoints;
            dog.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
