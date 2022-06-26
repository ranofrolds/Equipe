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
    }

    void generateWaveBox(){
        int idWave=GameObject.Find("Wave").GetComponent<WaveScript>().idWave;
        if(idWave>0){
            if(idWave <= 2){
                //ativar só algumas de papelao
            }
            else if(idWave <=5){
               //ativar mais algumas de papelao
            }
            else if(idWave <=8){
                //ativar todas de papelao
            }
            else if(idWave <=12){
                //ativar algumas de metal
            }
            else{
                //ativar todas de metal
            }
        }


    }
}
