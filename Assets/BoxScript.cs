using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int idBox;
    public int idItem;
    public string status;

    public BoxManager manager;

    
    void Update()
    {
        
        //Se tem filho
        if(transform.childCount > 0)
        {
            SpriteRenderer itemSprite = transform.Find("ItemSprite").GetComponent<SpriteRenderer>();
            itemSprite.sprite = manager.sprites[idItem];
        }

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


    public int pickItem(){
        if(this.status=="Ready"){
            //pegar o item


            //mudar o status para loading
            changeStatus("Loading");

            print("pegou o item");
            
            return idItem;

        }
        
        else return -1;

    }

    
}

