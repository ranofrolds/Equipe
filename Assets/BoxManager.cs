using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{

    public Sprite loading, trash;
    public List<Sprite> sprites = new List<Sprite>();
    public List<BoxScript> boxes = new List<BoxScript>();

    public float multiplier;

    void Start(){
        foreach(BoxScript box in boxes){
            if(box.status!="Trash" && box.status!="Energia"){
                box.changeStatus("Unavailable");
            }
        }

        
        
        
    }

    void orderBoxes()
    {
        boxes.Clear();

        for(int i=0; i<28;i++){
            boxes.Add(GameObject.Find("box"+i.ToString()).GetComponent<BoxScript>());

            //colocar seus respectivos BOX TYPES

            if(i>=0 && i<17){
                boxes[i].boxType="Papelao";
                //papelao
            }
            else if(i>=17 && i<24){
                boxes[i].boxType="Metal";
                //metal
            }
            else if(i>=24 && i<27){
                boxes[i].boxType="Lixeira";
                //lixeira
            }
            else{
                boxes[i].boxType="Energia";
                boxes[i].status="Energia";
                //energia
            }
        }
    }

}
