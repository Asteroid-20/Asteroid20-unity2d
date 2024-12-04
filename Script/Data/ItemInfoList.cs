using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemInfo", menuName = "ScriptableObject/StaticItemData", order=0)]
public class ItemInfoList : ScriptableObject
{
    [SerializeField] TextAsset dataFile;
    [SerializeField] public List<ItemInfo> itemlist;
    void OnValidate()
    {
        if(!dataFile) return;
        itemlist.Clear();
        string[] textInLines =  dataFile.text.Split('\n');
        for(int index = 1 ; index< textInLines.Length-1; index++)
        {
            string[] Values = textInLines[index].Split(',');
            
            ItemInfo iteminfo = new ItemInfo();

            iteminfo.id = int.Parse(Values[0]);
            iteminfo.name = Values[1];
            iteminfo.text = Values[2];
            iteminfo.appraisal = int.Parse(Values[3]);
            iteminfo.price = int.Parse(Values[4]);
            iteminfo.usespace = int.Parse(Values[5]);
            iteminfo.levelrequirement = int.Parse(Values[6]);
            iteminfo.maxlevel = int.Parse(Values[7]);
            iteminfo.efficiency = float.Parse(Values[8]);
            iteminfo.expend =int.Parse(Values[9]);
            
            itemlist.Add(iteminfo);

        }

    }
}

[System.Serializable]
public class ItemInfo
{
    public int id;
    public string name;
    public string text;
    public int appraisal;
    public int price;
    public int usespace;

    public int levelrequirement;
    public int maxlevel;
    public float efficiency;
    public int expend;
}