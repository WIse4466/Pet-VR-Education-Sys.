using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DogMoving : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    public float speed = 1.0f;

    private bool lookAt = false;

    public Animator animator;
    float control = 0.0f;
    public float acceleration = 0.5f;
    int VelocityHash;

    public AudioSource audioSource;
    public TextMeshProUGUI Information;
    // Start is called before the first frame update
    void Start()
    {
        lastWaypointSwitchTime = Time.time;
        gameObject.transform.position = waypoints[0].transform.position;
        animator = GetComponent<Animator>();
        //VelocityHash = Animator.StringToHash("control");
        audioSource = gameObject.GetComponent<AudioSource>();
        //AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
        Information.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(lookAt == true)
        {
            Debug.Log("不要動");
            Time.timeScale = 0;
            //audioSource = GetComponent<AudioSource>();
            audioSource.Pause();
            Information.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            animator.SetBool("turn", false);
            MovingOnRoad();
            audioSource.UnPause();
            //audioSource.loop = true;
            //audioSource.Play();
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            Information.gameObject.SetActive(false);

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
        //調整blend的數值
        if(control<=1)
        {
            control = (currentTimeOnPath / totalTimeForPath)*acceleration;
        }
        animator.SetFloat("control", control);
        Debug.Log("blend值" + control);

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
                //做動畫
                animator.SetBool("turn", true);
                animator.SetFloat("control", 1);

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
        Debug.Log("新方向" + newDirection);
        //利用Mathf.Atan2來決定要轉多少度
        float x = newDirection.x;
        float y = newDirection.y;
        float z = newDirection.z;
        float rotationAngle = Mathf.Atan2(x, z) * 180 / Mathf.PI;
        Debug.Log("轉幾度" + rotationAngle);
        //轉動dog
        //gameObject.transform.localEulerAngles = new Vector3(0.0f, rotationAngle, 0.0f);
        gameObject.transform.rotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
        Debug.Log("轉了"+gameObject.transform.rotation);
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
