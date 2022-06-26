using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    int idRobot;
    public bool[] robotParts;
    bool done;



    List<Transform> points = new List<Transform>();

    public List<SpriteRenderer> holes = new List<SpriteRenderer>();

    
    public float speed;
    public bool selected;
    int currentPoint = 0;

    



    // Start is called before the first frame update
    void Start()
    {

        speed= GameObject.Find("Wave").GetComponent<WaveScript>().speed;


        //pegar lista de buracos
        for(int i = 0; i < transform.childCount; i++)
        {
            holes.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
            holes[i].enabled = false;
        }


        //pegar lista de pontos
        points = GameObject.Find("Checkpoints").GetComponent<CheckpointList>().points;

        //posiçao do robo = ponto inicial
        transform.position = points[0].position;

        done=false;
        
        int wave= GameObject.Find("Wave").GetComponent<WaveScript>().idWave;
        
        
        //habilitar numero certo de buracos
        for(int i = 0; i < robotParts.Length; i++)
        {
            holes[i].enabled = true;
        }


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

        if(currentPoint==11){
            if(robots!=null){

                robots.Dequeue();
            }
            Destroy(gameObject);
        }

        if(done==true){
            GameObject.Find("Wave").GetComponent<WaveScript>().done[GameObject.Find("Wave").GetComponent<WaveScript>().idWave]+=1;
        }

    }

    public bool insertPart(int idPart){
        if(robotParts[idPart]==false){

            BoxManager boxManager = GameObject.Find("BoxManager").GetComponent<BoxManager>();

            robotParts[idPart]=true;
            holes[idPart].color = Color.white;
            holes[idPart].sprite = boxManager.sprites[idPart];
            
            return true;



            //insere
        }
        else{
            print("Ja foi inserido");
            return false;
            //nao insere
        }

    }
}
