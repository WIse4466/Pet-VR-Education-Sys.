using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMoving : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    public float speed = 1.0f;

    private bool isCollisionOnTriggered = false;
    private bool lookAt = false;
    // Start is called before the first frame update
    void Start()
    {
        lastWaypointSwitchTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(lookAt == true)
        {
            Debug.Log("不要動");
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1.0f;
            MovingOnRoad();
        }
    }

    void MovingOnRoad()
    {
        //設定移動路徑的起終點
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;
        Debug.Log("currentWaypoint起點" + currentWaypoint);
        //計算兩點間的距離，以此算出需花費時間
        //currentTimeOnPath可以算出現在離上一個點過多久了，用Lerp可以模擬出他的路徑
        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
        //
        Debug.Log("預計總花費時間" + totalTimeForPath);
        Debug.Log("在這條路上跑多久了" + currentTimeOnPath);
        Debug.Log("狗的位子" + gameObject.transform.position);
        Debug.Log("終點的位子" + endPosition);
        if (totalTimeForPath - currentTimeOnPath <= 0.02)
        {
            Debug.Log("抵達終點了");
            if (currentWaypoint < waypoints.Length - 2)
            {
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
                Debug.Log("成功++");
                RotateIntoMoveDirection();
                //換成走向下一個點
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                //AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                audioSource.Play();
            }
            else
            {
                Debug.Log("結束");
                //Destroy(gameObject);
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
        float rotationAngle = Mathf.Atan2(z, x) * 180 / Mathf.PI;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, newDirection, 0.1f, 0.0f);
        //轉動dog的子物件
        GameObject default1 = gameObject.transform.Find("default").gameObject;
        //default1.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        //default1.transform.RotateAround(default1.transform.position, Vector3.up, rotationAngle);
        //default1.transform.rotation = Quaternion.LookRotation(newDir);
        default1.transform.rotation = Quaternion.identity; // 重置旋转
        default1.transform.Rotate(Vector3.up, 360 - rotationAngle); // 应用新的旋转

    }

    public void test()
    {
        Debug.Log("成功");
        lookAt = true;
    }

    public void exit()
    {
        Debug.Log("離開");
        lookAt = false;
    }
}
