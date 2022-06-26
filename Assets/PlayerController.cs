using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Item heldItem;
    public Item nothing;


    public BoxManager boxManager;
    public RobotManager robotManager;

    Transform teleport1, teleport2;

    SpriteRenderer heldSprite;

    bool teleported=false;
    Rigidbody2D r;
    
    float h, v;

    LayerMask boxLayer, robotLayer;


    void Start()
    {
        heldSprite = transform.Find("HeldSprite").GetComponent<SpriteRenderer>();
        boxLayer = LayerMask.GetMask("Box");
        robotLayer = LayerMask.GetMask("Robot");
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

        robotDetect();
       
        heldSprite.sprite = heldItem.sprite;

    }

    void FixedUpdate()
    {

        Move();



    }

    public float radius;
    void robotDetect()
    {

        //Detectar se está próximo de um robô
        Collider2D robotCol = null;
        Collider2D[] nearRobots;
        //float radius = 1f;
        nearRobots = Physics2D.OverlapCircleAll(transform.position, radius, robotLayer);


        if(nearRobots.Length >= 1)
        {
            robotCol = nearRobots[0];
            for(int i = 1; i < nearRobots.Length; i++)
            {
                float currentDistance = Vector2.Distance(transform.position, robotCol.bounds.center);
                float myDistance = Vector2.Distance(transform.position, nearRobots[i].bounds.center);

                if (myDistance < currentDistance) robotCol = nearRobots[i];

            }
        }

        RobotScript robotScript = robotCol?.gameObject.GetComponent<RobotScript>();

        for(int i = 0; i < robotManager.robots.Count; i++)
        {
                
                robotManager.robots[i].selected = false;
              
        }

        if(robotScript != null) 
        {
            
            

            robotScript.selected = true;

            //se clicou pra inserir & está segurando alguma coisa
            if (Input.GetButtonDown("Pegar") && heldItem.idItemType != -1)
            {

                //se a parte foi inserida com sucesso
                if (robotScript.insertPart(heldItem))
                {
                    //tirar item da mão
                    heldItem = nothing;
                }
                
            }


        }

    }
    void boxDetect()
    {
        //Detectar se está próximo de uma caixa
        Collider2D boxCol = null;
        Collider2D[] nearBoxes;
        nearBoxes = Physics2D.OverlapCircleAll(transform.position, .5f, boxLayer);


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

            if (Input.GetButtonDown("Pegar") && heldItem == nothing && boxScript.status != "Energia")
            {
                Item picked = boxScript.pickItem();
                if(picked != null)
                {
                    heldItem = picked;
                }
            }
            else if (Input.GetButtonDown("Pegar") && boxScript.status == "Trash")
            {


                heldItem = nothing;


            }
            else if (Input.GetButtonDown("Pegar") && boxScript.status == "Energia")
            {
                print("ENTROU");
                if(GameObject.Find("box27").GetComponent<EnergyBoxScript>().status!="OK" && GameObject.Find("box27").GetComponent<EnergyBoxScript>().status!="Charging"&&
                GameObject.Find("box27").GetComponent<EnergyBoxScript>().status!="Full"){
                    GameObject.Find("box27").GetComponent<EnergyBoxScript>().status="Charging";
                    GameObject.Find("box27").GetComponent<EnergyBoxScript>().carregarEnergia(GameObject.Find("Wave").GetComponent<WaveScript>().waveDifficulty);
                }
                

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

        //teleport 1 = direita
        //se mais pra direita do que o teleport 1, teleportar
        bool toTheRight = transform.position.x > teleport1.position.x;
        if(toTheRight&&teleported==false){
            teleported=true;
            Vector2 novo= new Vector2(teleport2.position.x+.6f,teleport2.position.y);
            transform.position=novo;
        }

        bool toTheLeft = transform.position.x < teleport2.position.x;
        if(toTheLeft&&teleported==false){
            teleported=true;
            Vector2 novo= new Vector2(teleport1.position.x-.6f,teleport1.position.y+.2f);
            transform.position=novo;
        }
    }

}
