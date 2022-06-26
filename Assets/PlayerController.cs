using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int heldItemId = -1;

    public BoxManager boxManager;


    Transform teleport1, teleport2;

    SpriteRenderer heldSprite;

    bool teleported=false;
    Rigidbody2D r;
    
    float h, v;

    LayerMask boxLayer;


    void Start()
    {
        heldSprite = transform.Find("HeldSprite").GetComponent<SpriteRenderer>();
        boxLayer = LayerMask.GetMask("Box");
        r = GetComponent<Rigidbody2D>();
        teleport1 = GameObject.Find("teleport1").transform;
        teleport2 = GameObject.Find("teleport2").transform;
    }


    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");        

        doTeleport();

        boxDetect();
       
        if(heldItemId == -1) heldSprite.sprite = null;
        else heldSprite.sprite = boxManager.sprites[heldItemId];


    }

    void FixedUpdate()
    {

        Move();



    }


    void boxDetect()
    {
        //Detectar se está próximo de uma caixa
        Collider2D boxCol = null;
        Collider2D[] nearBoxes;
        nearBoxes = Physics2D.OverlapCircleAll(transform.position, .5f, boxLayer);
        Debug.DrawLine(transform.position, transform.position + (Vector3.right * 0.5f));


        if(nearBoxes.Length >= 1)
        {
            boxCol = nearBoxes[0];
            for(int i = 1; i < nearBoxes.Length; i++)
            {
                float currentDistance = Vector2.Distance(transform.position, boxCol.bounds.center);
                float myDistance = Vector2.Distance(transform.position, nearBoxes[i].bounds.center);

                if (myDistance < currentDistance) boxCol = nearBoxes[i];

            }
        }

        BoxScript boxScript = boxCol?.gameObject.GetComponent<BoxScript>();

        for(int i = 0; i < boxManager.boxes.Count; i++)
        {
                
                boxManager.boxes[i].selected = false;
              
        }

        if(boxScript != null) 
        {
            

            boxScript.selected = true;

            if (Input.GetButtonDown("Pegar") && heldItemId == -1)
            {
                int picked = boxScript.pickItem();
                if(picked != -2)
                {
                    heldItemId = picked;
                }       
            }
            else if (Input.GetButtonDown("Pegar") && boxScript.status == "Trash")
            {


                heldItemId = boxScript.pickItem();


            }


        }

    }
    void Move()
    {
        //Movimentação
        Vector2 newSpeed = new Vector2();

        
        newSpeed.x = speed * h;
        newSpeed.y = speed * v;

        r.velocity = newSpeed;

    }

    void doTeleport(){
        if(Vector2.Distance(transform.position,teleport1.position)>=0.6f &&Vector2.Distance(transform.position,teleport2.position)>=0.6f){
            teleported=false;
        }

        if(Vector2.Distance(transform.position,teleport1.position)<0.5f&&teleported==false){
            teleported=true;
            Vector2 novo= new Vector2(teleport2.position.x+.6f,teleport2.position.y);
            transform.position=novo;
        }

        if(Vector2.Distance(transform.position,teleport2.position)<0.5f&&teleported==false){
            teleported=true;
            Vector2 novo= new Vector2(teleport1.position.x-.6f,teleport1.position.y+.2f);
            transform.position=novo;
        }
    }

}
