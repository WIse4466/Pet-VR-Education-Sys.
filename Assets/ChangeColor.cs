using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeColor : MonoBehaviour
{
    public TextMeshPro dogSaying;
    public GvrReticlePointer GvrReticlePointer;
    public void Start()
    {
        //dog1 = GetComponent<MeshRenderer>();    
    }

    public void Red()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        //GetComponent<MeshRenderer>().transform.position = new Vector3(0f, 0.183f, -0.622f);
        dogSaying.text = "Bark";
        dogSaying.color = Color.cyan;
    }

    public void Blue()
    {
        Texture dogSkin = Resources.Load("dog1_tex.png", typeof(Texture)) as Texture;
        GetComponent<Renderer>().material.SetTexture("_MainTex", dogSkin);
        dogSaying.text = "";
    }

    public void Black()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }
}
