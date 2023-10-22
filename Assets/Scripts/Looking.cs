using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looking : MonoBehaviour
{
    private bool lookAt = false;
    bool feedingMode = false;
    Animator animator;
    AudioSource audioSource;
    public AudioClip[] audios;

    int answer = 1;//你再改成你的答案

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lookAt == true && feedingMode == false)
        {
            animator.SetBool("watch", true);
            audioSource.clip = audios[1];
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if(feedingMode == false)
        {
            animator.SetBool("watch", false);
            audioSource.clip = audios[0];
            audioSource.Play();
        }

        if (Input.GetButtonDown("Fire3"))//按下餵食鍵後的反應
        {
            feedingMode = true;
            animator.SetBool("feeding mode", true);
            animator.SetInteger("correct", answer);//傳入的答案正確與否(0錯誤或1正確)
            if(answer == 1)
            {
                audioSource.clip = audios[2];
                audioSource.Play();
            }
        }
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
