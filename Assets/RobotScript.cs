using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    int idRobot;
    bool[] robotParts;
    bool done;


    List<Transform> points = new List<Transform>();
    public float speed;

    int currentPoint = 0;





    // Start is called before the first frame update
    void Start()
    {


        //pegar lista de pontos
        points = GameObject.Find("Checkpoints").GetComponent<CheckpointList>().points;

        //posiçao do robo = ponto inicial
        transform.position = points[0].position;

        done=false;
        
        int wave=1;
        int min=0, max=0;
        /*
        wave 1 - 2 -> 2 peças
        wave 3 - 5 ->2 - 3 peças
        wave  6- 8 -> 2- 4 
        wave 8 - 12 -> 3 - 5
        wave 12+ -> 5
        */

        if(wave>0){
            if(wave <= 2){
                min=1;
                max=2;
            }
            else if(wave <=5){
                min=2;
                max=3;
            }
            else if(wave <=8){
                min=2;
                max=4;
            }
            else if(wave <=12){
                min=3;
                max=5;
            }
            else{
                min=5;
                max=5;
            }
        }

        if(min !=0 && max!=0 ){
            int n= Random.Range(min-1, max);
            robotParts = new bool[n];
        }
        


    }

    // Update is called once per frame
    void Update()
    {

        
        bool reached = Vector2.Distance(transform.position, points[currentPoint].position) <= .1f;
        if (reached) currentPoint++;


        Vector3 goal = points[currentPoint].position - transform.position;
        
        transform.Translate(goal.normalized * speed * Time.deltaTime);
    }

    void insertPart(int idPart){
        if(robotParts[idPart]==false){
            robotParts[idPart]=true;
            //insere
        }
        else{
            print("Ja foi inserido");
            //nao insere
        }

    }
}
