using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Velocidade;
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
    }


    void FixedUpdate()
    {

       Vector2 novaVelocidade = new Vector2();
        
        novaVelocidade.x = Velocidade * h;
        novaVelocidade.y = Velocidade * v;

        r.velocity = novaVelocidade;

    }
}
