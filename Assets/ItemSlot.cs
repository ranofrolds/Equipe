using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Slot", menuName = "ScriptableObjects/Item Slot", order = 1)]
public class ItemSlot : ScriptableObject
{
    

    public Sprite icon;

    public int idRequestedItemType;
    public Item currentItem;

    public bool filled;



}
