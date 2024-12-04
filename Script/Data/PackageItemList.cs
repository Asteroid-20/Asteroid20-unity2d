using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Package", menuName = "ScriptableObject/PackageItemData", order=0)]
public class PackageItemList : ScriptableObject
{
    [SerializeField] public List<PackageItem> packageList = new List<PackageItem>();

}
[System.Serializable]
public class PackageItem
{
    public int id=0;
    public int quantity=0;
    public int level=0;
    public int exp=0;
    public bool enable = false;

}

