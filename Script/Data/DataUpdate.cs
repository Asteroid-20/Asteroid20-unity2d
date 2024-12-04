using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataUpdate : MonoBehaviour
{
    //引入ScriptableObject
    public CharacterInfoList cil;
    public PackageItemList pil;
    //引入GameObject
    public GameObject power;
    //声明组件
    private TextMeshProUGUI powertext;
    void Awake()
    {
        //映射组件 
        powertext = power.GetComponent<TextMeshProUGUI>(); 
    }
    void Update()
    {
        powertext.text = cil.power.ToString() + "/" + cil.maxpower.ToString();
    }
}
