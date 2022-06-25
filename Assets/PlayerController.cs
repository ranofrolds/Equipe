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


    void Start()
    {
        r = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");        

        //DETECTAR INPUT
        doTeleport();
    }



    void FixedUpdate()
    {

       Vector2 novaVelocidade = new Vector2();
        
        novaVelocidade.x = Velocidade * h;
        novaVelocidade.y = Velocidade * v;

        r.velocity = novaVelocidade;

    }

    void doTeleport(){
        if(Vector2.Distance(transform.position,teleport1.position)>0.6f &&Vector2.Distance(transform.position,teleport2.position)>0.5f){
            teleported=false;
        }

        if(Mathf.Abs(transform.position.x-teleport1.position.x)<0.5f&&teleported==false ){
            teleported=true;
            Vector2 novo= new Vector2(teleport2.position.x+.7f,teleport2.position.y);
            transform.position=novo;
        }

        if(Mathf.Abs(transform.position.x-teleport2.position.x)<.5f&&teleported==false){
            teleported=true;
            Vector2 novo= new Vector2(teleport1.position.x-.7f,teleport1.position.y);
            transform.position=novo;
        }
    }
}
