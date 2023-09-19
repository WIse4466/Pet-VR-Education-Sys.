using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManagement : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject dog;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate是實例化，把dog這個prefab實例化
        //取得實例化的dog中名為MoveDog的組件
        //把這個物件裡的waypoints assign給MoveDog的waypoints
        Instantiate(dog).GetComponent<MoveDog>().waypoints = waypoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
