using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Velocidade;
    public Transform teleport1, teleport2;

    bool teleported=false;
    Rigidbody2D r;
    
    float h, v;

    LayerMask boxLayer;


    void Start()
    {
        boxLayer = LayerMask.GetMask("Box");
        r = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");        

        doTeleport();
    }


    public float boxDetectionRadius;
    void FixedUpdate()
    {

        //Movimentação
        Vector2 newSpeed = new Vector2();

        
        newSpeed.x = Velocidade * h;
        newSpeed.y = Velocidade * v;

        r.velocity = newSpeed;


        //Detectar se está próximo de uma caixa
        GameObject box;
        Collider2D[] nearBoxes = Physics2D.OverlapCircleAll(transform.position, boxDetectionRadius, boxLayer);


        if(nearBoxes.Length > 1)
        {
            for(int i = 0; i < nearBoxes.Length; i++)
            {

  

            }
        }
        else box = nearBoxes[0].gameObject;

        

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

    void pickItem(){
        
    }
}
