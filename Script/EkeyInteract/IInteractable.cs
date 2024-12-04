using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable  
{
    public float time {get;set;}
    public bool keepactions{get;set;}

    void interact(GameObject obj,GameObject input);
    string getInteractText(); 
}

