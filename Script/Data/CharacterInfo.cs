using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterInfo", menuName = "ScriptableObject/CharacterInfo", order = 0)]
public class CharacterInfoList : ScriptableObject 
{
    [SerializeField] public int power;
    [SerializeField] public int maxpower;
}


