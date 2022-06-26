using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
   
    public GameObject robot;
    public int idWave;
    public Queue<GameObject> robots=new Queue<GameObject>();

    public List<int>done=new List<int>();
    public float speed;
    
    public bool restarted=false;


    public float delayBetweenRobots = 3;
    public float delayBetweenWaves = 5;


    public RobotManager robotManager;


    void Start()
    {
        restarted=false;
        idWave=1;
        done.Add(0);

        GameObject currentRobot = Instantiate(robot);
        robots.Enqueue(currentRobot);
        robotManager.robots.Add(currentRobot.GetComponent<RobotScript>());
        StartCoroutine(delay(delayBetweenRobots));
        
    }

    void Restart(){
        idWave++;
        done.Add(0);        
        GameObject currentRobot = Instantiate(robot);
        robots.Enqueue(currentRobot);
        robotManager.robots.Add(currentRobot.GetComponent<RobotScript>());
        StartCoroutine(delay(delayBetweenRobots));
    }


    void Update()
    {

        if(robots.Count==0 && restarted==false){
            //BOTAR UM DELAY PARA COMEÇAR PROXIMA WAVE VVV
            restarted=true;
            StartCoroutine(delayNextWave(delayBetweenWaves));
        }
    }

    IEnumerator delay(float delay)
    {
        for(int i=1; i<(idWave*2)+1; i++){
            yield return new WaitForSeconds(delay);
            
            GameObject currentRobot = Instantiate(robot);
            robots.Enqueue(currentRobot);
            robotManager.robots.Add(currentRobot.GetComponent<RobotScript>());
        }
    }

    IEnumerator delayNextWave(float delay)
    {
        yield return new WaitForSeconds(delay);
        Restart();
    }
}
