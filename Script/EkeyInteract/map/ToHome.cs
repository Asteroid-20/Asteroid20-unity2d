using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToHome : MonoBehaviour,IInteractable
{
    public float time {get;set;}
    public bool keepactions{get;set;}
    public void interact(GameObject obj,GameObject input)
    {
        SceneManager.LoadScene("Home");
    }

    public string getInteractText()
    {
        return "进入房子" ;
    } 
}
