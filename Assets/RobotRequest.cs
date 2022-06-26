using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotRequest : MonoBehaviour
{
    
    public List<Item> Build = new List<Item>();

    public List<SpriteRenderer> slots = new List<SpriteRenderer>();

    RobotManager robotManager;

    public Item Body, Head, Arm, Leg;

    void Start()
    {
         //pegar robotManager
        robotManager = GameObject.Find("RobotManager").GetComponent<RobotManager>();

        Arm = robotManager.currentItems[Random.Range(0, robotManager.currentItems.Count)];
        while (Arm.type != "Arm"){
            Arm = robotManager.currentItems[Random.Range(0, robotManager.currentItems.Count)];
        }
        
        Body = robotManager.currentItems[Random.Range(0, robotManager.currentItems.Count)];
        while (Body.type != "Body"){
            Body = robotManager.currentItems[Random.Range(0, robotManager.currentItems.Count)];
        }
        
        Head = robotManager.currentItems[Random.Range(0, robotManager.currentItems.Count)];
        while (Head.type != "Head"){
            Head = robotManager.currentItems[Random.Range(0, robotManager.currentItems.Count)];
        }
        
        Leg = robotManager.currentItems[Random.Range(0, robotManager.currentItems.Count)];
        while (Leg.type != "Leg"){
            Leg = robotManager.currentItems[Random.Range(0, robotManager.currentItems.Count)];
        }


        Build.Add(Body);
        Build.Add(Head);
        Build.Add(Arm);
        Build.Add(Leg);


        slots[0].sprite = Body.icon;
        slots[1].sprite = Head.icon;
        slots[2].sprite = Arm.icon;
        slots[3].sprite = Leg.icon;


    }


}
