using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogWandering : MonoBehaviour
{
    
    [HideInInspector]
    public GameObject[] waypoints;
    public int currentWaypoint;
    private float lastWaypointSwitchTime;
    public float speed = 1.0f;
    public int breed;

    private bool lookAt = false;
    private bool move = true;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        lastWaypointSwitchTime = Time.time;
        currentWaypoint = breed;
        animator = GetComponent<Animator>();
        if(waypoints.Length > 0)
        {
            gameObject.transform.position = waypoints[breed].transform.position;
            RotateIntoMoveDirection();
            Debug.Log("初始值" + currentWaypoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(lookAt == true)
        {
            animator.SetBool("run", false);
            Time.timeScale = 0;
            bool run = animator.GetBool("run");
            Debug.Log("品種"+breed+"暫停時表現" + Time.timeScale+run);
        }
        else if(move == true)
        {
            animator.SetBool("run", true);
            Time.timeScale = 1.0f;
            bool run = animator.GetBool("run");
            Debug.Log("品種"+breed+"恢復播放表現" + Time.timeScale+run);
            MovingOnRoad();
            Debug.Log("品種" + breed + "有移動了");
        }
    }

    void MovingOnRoad()
    {
        //設定移動路徑的起終點
        Vector3 startPosition = Vector3.zero;
        Vector3 endPosition = Vector3.zero;
        if (waypoints.Length > 0)
        {
            startPosition = waypoints[currentWaypoint].transform.position;
            endPosition = waypoints[currentWaypoint + 1].transform.position;
        }
        
        //計算兩點間的距離，以此算出需花費時間
        //currentTimeOnPath可以算出現在離上一個點過多久了，用Lerp可以模擬出他的路徑
        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

        if (totalTimeForPath - currentTimeOnPath <= 0.02)
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
                RotateIntoMoveDirection();
            }else
            {
                currentWaypoint = 0;
            }
        }
    }

    private void RotateIntoMoveDirection()
    {
        //利用目前的起終點計算移動方向
        Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
        Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
        Vector3 newDirection = (newEndPosition - newStartPosition);
        
        //利用Mathf.Atan2來決定要轉多少度
        float x = newDirection.x;
        float y = newDirection.y;
        float z = newDirection.z;
        float rotationAngle = Mathf.Atan2(x, z) * 180 / Mathf.PI;
        
        //轉動dog
        //gameObject.transform.localEulerAngles = new Vector3(0.0f, rotationAngle, 0.0f);
        gameObject.transform.rotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
        
    }

    public void Enter()
    {
        lookAt = true;
    }

    public void Exit()
    {
        lookAt = false;
    }
}
