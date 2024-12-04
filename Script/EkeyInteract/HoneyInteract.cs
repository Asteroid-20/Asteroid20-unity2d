using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyInteract : MonoBehaviour,IInteractable
{
    public float time {get;set;}
    public bool keepactions{get;set;}
    public GameObject rt;
    public AudioClip getting;


    public void interact(GameObject obj,GameObject input)
    {

        // DataManager dm = rt.GetComponent<DataManager>();
        // dm.gs.honeycount+=1;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = getting;
        audioSource.Play();
        print("获得蜂蜜");
    }


    public string getInteractText()
    {
        return "按E拿取蜂蜜";
    } 

}
