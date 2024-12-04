using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStart : MonoBehaviour
{
    public Vector3 beforechange;
    public GameObject button;
    public Vector3 position;
    public Vector3 beforeposition = new Vector3(0, 0, 0);


    // Start is called before the first frame update
    void Start()
    {
        beforechange = transform.localScale;
        beforeposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseEnter()
    {
        transform.localScale += new Vector3(0.2f, 0.2f, 0);
    
    }
    void OnMouseExit()
    {
        transform.localScale = beforechange;
    }
    void OnMouseOver() 
    {
        beforeposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    //Êó±ê°´ÏÂ
    void OnMouseDown()
    {
        SceneManager.LoadScene("MainPlay");
    }


    void OnMouseDrag()
    {
        
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        beforeposition.z = 0;
        transform.position += (position - beforeposition);
        beforeposition = position;
    }
}
