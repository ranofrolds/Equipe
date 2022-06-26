using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    int idRobot;

    public List<ItemSlot> robotParts = new List<ItemSlot>();

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

    



    // Start is called before the first frame update
    void Start()
    {

        speed= GameObject.Find("Wave").GetComponent<WaveScript>().speed;





        //pegar lista de pontos
        points = GameObject.Find("Checkpoints").GetComponent<CheckpointList>().points;

        //pegar robotManager
        robotManager = GameObject.Find("RobotManager").GetComponent<RobotManager>();

        //pegar wave script
        waveScript = GameObject.Find("Wave").GetComponent<WaveScript>();

        //posiçao do robo = ponto inicial
        transform.position = points[0].position;

    }

    // Update is called once per frame
    void Update()
    {

        Queue<GameObject> robots = GameObject.Find("Wave").GetComponent<WaveScript>().robots;
        bool reached = Vector2.Distance(transform.position, points[currentPoint].position) <= .1f;
        if (reached){
            currentPoint++;
        } 


        Vector3 goal = points[currentPoint].position - transform.position;

        
        transform.Translate(goal.normalized * speed * Time.deltaTime);

        if(currentPoint==12){
            if(robots!=null){

                if(robots.Count == 1)
                {
                    GameObject.Find("Wave").GetComponent<WaveScript>().restarted = false;
                }
                robots.Dequeue();

            }
            

         


        //para cada slot de item
        for(int i = 0; i < robotParts.Count; i++)
        {
            if(robotParts[i].currentItem == null) robotParts[i].currentItem = nothing;
            bool rightItem = robotParts[i].idRequestedItemType == robotParts[i].currentItem.idItemType;

            //se estiver preenchido e certo, adicionar ponto
            if(robotParts[i].filled && rightItem) score += 10;
        }

            waveScript.maxScore += maxScore;
            waveScript.score += score;
            
            if(score==maxScore){
                GameObject.Find("Wave").GetComponent<WaveScript>().done[GameObject.Find("Wave").GetComponent<WaveScript>().idWave]+=1;
            }

            Destroy(gameObject);
           
        }

        

    }

    public bool insertPart(Item insertedPart){

        bool anySlotFilled = false;
        //para cada slot de item
        for(int i = 0; i < robotParts.Count; i++)
        {
            
            //colocar no primeiro disponivel
            if(robotParts[i].filled == false && anySlotFilled == false)
            {



                //registrar que preencheu ja algum
                anySlotFilled = true;

                //registrar no slot que ele foi preenchido
                robotParts[i].filled = true;

                //colocar o item atual no slot
                robotParts[i].currentItem = insertedPart;

                holes[i].sprite = insertedPart.sprite;
                holes[i].color = Color.white;

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
