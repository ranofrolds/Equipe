using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int idBox;
    public int idItem;
    public string status;



    public void changeStatus(string newStatus){
        if(newStatus=="Loading"){
            this.status = newStatus;
            StartCoroutine(delay(2));
        }
        else if(newStatus=="Ready"){
            this.status=newStatus;
        }
    }

    IEnumerator delay(int delay)
    {
        yield return new WaitForSeconds(delay);
        changeStatus("Ready");
    }


    public void pickItem(){
        if(this.status=="Ready"){
            //pegar o item
        }
        
        changeStatus("Loading");
    }

    
}

