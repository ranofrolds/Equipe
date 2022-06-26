using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    
    public Sprite icon;
    public Sprite spriteLeft, spriteRight;

    public int variant;
    public string type;

}
