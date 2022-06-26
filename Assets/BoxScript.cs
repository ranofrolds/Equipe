using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int itemTypeId;
    public int idSprite;
    public string status;

    public BoxManager manager;

    public bool selected;
    SpriteRenderer itemSprite;

    void Start()
    {   

        if(transform.childCount > 0) itemSprite = transform.Find("ItemSprite").GetComponent<SpriteRenderer>();
        

    }

    void Update()
    {
        //Se tem filho
        if(transform.childCount > 0) 
        {
            if (status == "Trash") itemSprite.sprite = manager.trash;
            else if(status != "Loading") itemSprite.sprite = manager.sprites[idSprite];
            else itemSprite.sprite = manager.loading;
            itemSprite.transform.localScale = selected ? (Vector3.one * 2) : Vector3.one;
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
            
            return idSprite;

        }
        
        else if(this.status == "Trash")
        {
            print ("jogou o item fora");
            return -1;
        }

        else return -2;

    }

    
}

