using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEditor;

public class PackageSystem : MonoBehaviour
{
    public PackageItemList packageitemdata;
    public GameObject itemGUI;
    public Transform item;
    public SpriteRenderer icon;
    public TextMeshProUGUI quantitytext;
    public Transform outline;

    void Update()
    {
        //更新背包物品图标和持有数量
        for(int index=1;index<10;index++)
        {

            if (packageitemdata.packageList[index-1].id!=0)
            {
                //更新图标
                item = itemGUI.transform.Find("Item"+index+"/Icon");
                icon = item.GetComponent<SpriteRenderer>();
                string path = "Assets/Resources/Image/Item/"+packageitemdata.packageList[index-1].id+".png";
                if(AssetDatabase.LoadAssetAtPath<Sprite>(path))
                {
                    icon.sprite = Resources.Load<Sprite>("Image/Item/"+packageitemdata.packageList[index-1].id);
                }
                else
                {
                    icon.sprite =Resources.Load<Sprite>("Image/Item/null");
                }
                //更新持有数量
                item = itemGUI.transform.Find("Item"+index+"/QuantityText");
                quantitytext = item.GetComponent<TextMeshProUGUI>();

                quantitytext.text = packageitemdata.packageList[index-1].quantity.ToString();     
            }
            else
            {
                item = itemGUI.transform.Find("Item"+index+"/Icon");
                icon = item.GetComponent<SpriteRenderer>(); 
                icon.sprite = null; 

                item = itemGUI.transform.Find("Item"+index+"/QuantityText");
                quantitytext = item.GetComponent<TextMeshProUGUI>();
                quantitytext.text="";
            }
            //更新选中的物品栏和缩放
            item = itemGUI.transform.Find("Item"+index);
            if(packageitemdata.packageList[index-1].enable==true)
            {

                item.transform.localScale = new Vector3(115,115,115);
                outline = itemGUI.transform.Find("Outline");
                outline.transform.position = item.transform.position;
            }
            else
            {
              item.transform.localScale = new Vector3(100,100,100);
            }
        }     
    }



}
