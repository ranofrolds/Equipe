using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{

    public Sprite loading, trash;
    public List<Sprite> sprites = new List<Sprite>();
    public List<BoxScript> boxes = new List<BoxScript>();

    void Start(){
        foreach(BoxScript box in boxes){
            if(box.status!="Trash"){
                box.changeStatus("Unavailable");
            }
        }

        for(int i=0; i<28;i++){
            //colocar seus respectivos BOX TYPES

            if(i>=0 && i<17){
                //papelao
            }
            else if(i>=17 && i<24){
                //metal
            }
            else if(i>=24 && i<27){
                //lixeira
            }
            else{
                //energia
            }
        }
        
    }

}
