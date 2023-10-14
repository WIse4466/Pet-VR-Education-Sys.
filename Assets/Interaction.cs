using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    int watchTimes = 0;
    public float speed = 1.0f;
    public Vector3 target;
    float elaspedTime = 0.0f;
    float duration = 2.0f;
    float t;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //animator.SetBool("watching", false);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        elaspedTime += Time.deltaTime;
        t = Mathf.Clamp01(elaspedTime / duration);
    }

    public void watch()
    {
        animator.SetBool("watching", true);
        audioSource.Play();
        animator.SetInteger("watchTimes", watchTimes);
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
        //transform.Translate(Vector3.back * speed, Space.World);
        float moveDistance = 0.1f;
        target = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance);
        //transform.position = Vector3.MoveTowards(transform.position, target, speed);
        transform.position = Vector3.Lerp(transform.position, target, t);
        
    }
}
