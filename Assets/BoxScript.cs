using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    int idBox;
    int idItem;
    string status;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeStatus(string newStatus){
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


    void pickItem(){
        if(this.status=="Ready"){
            //pegar o item
        }
        
        //mudar o status para loading
        changeStatus("Loading");
    }

    
}

