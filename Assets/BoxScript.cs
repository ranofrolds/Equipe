using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Item item;
    public string status;

    public string boxType;
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
            if(status == "Unavailable") itemSprite.sprite = null;
            else if (status == "Trash") itemSprite.sprite = manager.trash;
            else if(status != "Loading") itemSprite.sprite = item.sprite;
            else itemSprite.sprite = manager.loading;
            itemSprite.transform.localScale = selected ? (Vector3.one * 2) : Vector3.one;
        }

        

    }

    public void changeStatus(string newStatus){
        if(newStatus=="Loading"){
            this.status = newStatus;
            StartCoroutine(delay(2));
        }
        else if(newStatus=="Ready"){
            this.status=newStatus;
        }
        else if(newStatus=="Unavailable"){
            this.status=newStatus;
        }
    }

    IEnumerator delay(int delay)
    {
        yield return new WaitForSeconds(delay);
        changeStatus("Ready");
    }


    public Item pickItem(){
        if(this.status=="Ready"){
            //pegar o item


            //mudar o status para loading
            changeStatus("Loading");

            print("pegou o item");
            
            return item;

        }
        
        else if(this.status == "Trash")
        {
            print ("jogou o item fora");
            return null;
        }

        else return null;

    }

    
}

