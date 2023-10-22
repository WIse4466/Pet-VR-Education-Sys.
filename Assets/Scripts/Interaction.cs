using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    int watchTimes = 0;
    public float speed = 0.0001f;
    public Vector3 target;
    int answer = 0;
    bool move = false;
    float startMovingTime;
    Vector3 endPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //animator.SetBool("watching", false);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //elaspedTime += Time.deltaTime;
        //t = Mathf.Clamp01(elaspedTime / duration);
        if(Input.GetButtonDown("Fire3"))
        {
            animator.SetBool("feeding mode", true);
            animator.SetInteger("correct", answer);
            StartCoroutine(AfterFeeding(4.0f));
        }

        if(move == true)
        {
            moving();
        }
    }

    public void watch()
    {
        animator.SetBool("watching", true);
        audioSource.Play();
        animator.SetInteger("watchTimes", watchTimes % 2);
        if(watchTimes >0)
        {
            //moving();
        }
        watchTimes++;
        Debug.Log("watching"+watchTimes);
    }

    public void lookAround()
    {
        animator.SetBool("watching", false);
        audioSource.Pause();
        Debug.Log("看周圍");
    }

    public void moving()
    {
        float moveDistance = 1f;
    
        //設定移動路徑的起終點
        Vector3 startPosition = gameObject.transform.position;
        //Vector3 endPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance);
        Debug.Log("currentWaypoint起點" + startPosition);
        //計算兩點間的距離，以此算出需花費時間
        //currentTimeOnPath可以算出現在離上一個點過多久了，用Lerp可以模擬出他的路徑
        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - startMovingTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
        Debug.Log("預計總花費時間" + totalTimeForPath);
        Debug.Log("在這條路上跑多久了" + currentTimeOnPath);
        Debug.Log("狗的位子" + gameObject.transform.position);
        Debug.Log("終點的位子" + endPosition);
    }

    IEnumerator AfterFeeding(float delay)
    {
        yield return new WaitForSeconds(delay);
        move = true;
        startMovingTime = Time.time;
        endPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
    }
}
