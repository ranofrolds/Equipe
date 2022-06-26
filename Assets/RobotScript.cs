using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    int idRobot;

    public bool armsSeparated;
    public bool legsSeparated;

    public List<ItemSlotMonobehaviour> slots = new List<ItemSlotMonobehaviour>();

    public int score;
    public int maxScore;

    List<Transform> points = new List<Transform>();

    public RobotManager robotManager;
    WaveScript waveScript;

    public List<SpriteRenderer> holes = new List<SpriteRenderer>();
    public Item nothing;

    
    public float speed;
    public bool selected;
    int currentPoint = 0;

    
    public List<Item> build = new List<Item>();

    public SidebarMANAGER managerSidebar;



    // Start is called before the first frame update
    void Start()
    {

        speed= GameObject.Find("Wave").GetComponent<WaveScript>().speed;





        //pegar lista de pontos
        points = GameObject.Find("Checkpoints").GetComponent<CheckpointList>().points;

        //pegar robotManager
        robotManager = GameObject.Find("RobotManager").GetComponent<RobotManager>();

        managerSidebar = GameObject.Find("SidebarMANAGER").GetComponent<SidebarMANAGER>();

        //pegar wave script
        waveScript = GameObject.Find("Wave").GetComponent<WaveScript>();

        //posiçao do robo = ponto inicial
        transform.position = points[0].position;

    }

    // Update is called once per frame
    void Update()
    {


        


        Queue<GameObject> robots = GameObject.Find("Wave").GetComponent<WaveScript>().robots;

        if(currentPoint==12)
        {



            

            if(robots!=null){

                if(robots.Count == 1)
                {
                    GameObject.Find("Wave").GetComponent<WaveScript>().restarted = false;
                }
                robots.Dequeue();

            }     
    
            for(int i = 0; i < managerSidebar.requests.Count; i++)
            {
                if(build == managerSidebar.requests[i].GetComponent<RobotRequest>().Build)
                {
                    
                    Destroy(managerSidebar.requests[i].gameObject);
                    managerSidebar.requests.RemoveAt(i);
                }
            }
            
            
            Destroy(gameObject);
               
        }

        bool reached = Vector2.Distance(transform.position, points[currentPoint].position) <= .1f;
        if (reached){
            currentPoint++;
        } 


        Vector3 goal = points[currentPoint].position - transform.position;

        
        transform.Translate(goal.normalized * speed * Time.deltaTime);

        

        

    }

    public bool insertPart(Item insertedPart){

        bool anySlotFilled = false;
        
        
        //para cada slot de item
        for(int i = 0; i < slots.Count; i++)
        {
            if(anySlotFilled) break;
            //está prenchido?

            //se não...
            if(!slots[i].filled)
            {
               
                //...é a parte certa q o player está tentando colocar?

                //se sim, colocar parte
                if(insertedPart.type == slots[i].type)
                {
                    anySlotFilled = true;
                    slots[i].currentItem = insertedPart;
                    slots[i].filled = true;


                    slots[i].transform.GetComponent<SpriteRenderer>().sprite = 
                        slots[i].isLeft ? insertedPart.spriteLeft : insertedPart.spriteRight;

                    
                    build.Add(insertedPart);
                }

            }

        }
           
            
            
        

        if(anySlotFilled == true)
        {
            print("slot preenchido");
            return true;
        }
        else 
        {
            print("nenhum slot preenchido");
            return false;
        }
    }
}
