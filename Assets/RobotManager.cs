using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{

    public List<Item> allItems = new List<Item>();
    public List<Item> currentItems = new List<Item>();

    public int maxItemTypeId;
    public List<RobotScript> robots = new List<RobotScript>();


}
