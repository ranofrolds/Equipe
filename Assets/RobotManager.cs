using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{

    public Queue<Item> allItems = new Queue<Item>();

    public List<Item> itemList = new List<Item>();
    public List<Item> currentItems = new List<Item>();


    public List<Item> currentBodys = new List<Item>();
    public List<Item> currentHeads = new List<Item>();
    public List<Item> currentArms = new List<Item>();
    public List<Item> currentLegs = new List<Item>();


    public int maxItemTypeId;
    public List<RobotScript> robots = new List<RobotScript>();



    public Item Arm, Leg, Body, Head;




    void Start()
    {

        int rnd=Random.Range(0, 12);

        while(itemList[rnd].type != "Arm")
        {
                rnd=Random.Range(0, 12);
        }
        Arm = itemList[rnd];
        currentItems.Add(Arm);
        currentArms.Add(Arm);
        itemList.RemoveAt(rnd);

        

        while(itemList[rnd].type != "Leg")
        {
                rnd=Random.Range(0, 11);
        }
        Leg = itemList[rnd];
        currentItems.Add(Leg);
        currentLegs.Add(Leg);
        itemList.RemoveAt(rnd);
        
        GerarQuatro();
        
       


        

        GerarFila();



    }

    void GerarQuatro()
    {

        
         int rnd=Random.Range(0, 10);

        while(itemList[rnd].type != "Body")
        {
                rnd=Random.Range(0, 10);
        }
        Body = itemList[rnd];
        currentItems.Add(Body);
        currentBodys.Add(Body);
        itemList.RemoveAt(rnd);
        
        while(itemList[rnd].type != "Head")
        {
                rnd=Random.Range(0, 9);
        }
        Head = itemList[rnd];
        currentItems.Add(Head);
        currentHeads.Add(Head);
        itemList.RemoveAt(rnd);

        
    }
    void GerarFila()
    {

        while (itemList.Count > 0){
            int rnd=Random.Range(0, itemList.Count);
            allItems.Enqueue(itemList[rnd]);
            itemList.RemoveAt(rnd);
        }

    }

}
