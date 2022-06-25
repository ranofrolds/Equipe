using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER : MonoBehaviour
{
 
    Rigidbody2D PEDRAO;

    // Start is called before the first frame update
    void Start()
    {
        
        PEDRAO = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        
        

    }

    void FixedUpdate()
    {

        //setar o x
        float X;
        X = 15 * Input.GetAxis("Horizontal");


        //aplicar o x
        PEDRAO.velocity = new Vector2(X, PEDRAO.velocity.y);


        


    }

}
