using UnityEngine;

public class Doberman_Controller : MonoBehaviour
{
    public GameObject[] waypoints;
    public int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    public float speed = 1.0f;

    private bool lookAt = false;
    public bool feedingMode = false;

    int answer = 1;//你再改成你的答案

    Animator animator;
    AudioSource audioSource;
    public AudioClip[] audios;

    // Start is called before the first frame update
    void Start()
    {
        lastWaypointSwitchTime = Time.time;
        animator = GetComponent<Animator>();
        if (waypoints.Length > 0)
        {
            gameObject.transform.position = waypoints[0].transform.position;
            RotateIntoMoveDirection();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAt == true && feedingMode == false)
        {
            animator.SetBool("watch", true);
            Time.timeScale = 0;
        }
        else if(feedingMode == false)
        {
            animator.SetBool("watch", false);
            Time.timeScale = 1.0f;
            MovingOnRoad();
        }

        if (Input.GetButtonDown("Fire3"))//按下餵食鍵後的反應
        {
            Time.timeScale = 1.0f;
            feedingMode = true;
            animator.SetBool("feeding mode", true);
            animator.SetInteger("correct", answer);//傳入的答案正確與否(0錯誤或1正確)
        }
    }

    void MovingOnRoad()
    {
        if(feedingMode == true)
        {
            return;
        }
        //設定移動路徑的起終點
        Vector3 startPosition = Vector3.zero;
        Vector3 endPosition = Vector3.zero;
        if (currentWaypoint < 2)
        {
            startPosition = waypoints[currentWaypoint].transform.position;
            endPosition = waypoints[currentWaypoint + 1].transform.position;
            RotateIntoMoveDirection();
        }
        else if(currentWaypoint == 2)
        {
            startPosition = waypoints[currentWaypoint].transform.position;
            endPosition = waypoints[0].transform.position;
            RotateIntoMoveDirection();
        }

        //計算兩點間的距離，以此算出需花費時間
        //currentTimeOnPath可以算出現在離上一個點過多久了，用Lerp可以模擬出他的路徑
        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

        if (totalTimeForPath - currentTimeOnPath <= 0.02)
        {
            if (currentWaypoint < 3)
            {
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
                RotateIntoMoveDirection();
            }
            else
            {
                currentWaypoint = 0;
                RotateIntoMoveDirection();
            }
        }
    }

    private void RotateIntoMoveDirection()
    {
        //利用目前的起終點計算移動方向
        Vector3 newDirection = Vector3.zero;
        if (currentWaypoint < 2)
        {
            Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
            Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
            newDirection = (newEndPosition - newStartPosition);
        }else if(currentWaypoint == 2)
        {
            Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
            Vector3 newEndPosition = waypoints[0].transform.position;
            newDirection = (newEndPosition - newStartPosition);
        }

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
