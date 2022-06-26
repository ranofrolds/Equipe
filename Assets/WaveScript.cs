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

     void generateWaveBox(){
        List<BoxScript> boxes = GameObject.Find("BoxManager").GetComponent<BoxManager>().boxes;
        List<BoxScript> boxesPapelao=new List<BoxScript>();
        List<BoxScript> boxesMetal=new List<BoxScript>();

        foreach(BoxScript box in boxes){
            //se for de papelao, adiciona no boxes papelao
            if(box.boxType=="Papelao"){
                boxesPapelao.Add(box);
            }
            else if(box.boxType=="Metal"){
                boxesMetal.Add(box);
            }
            //se for de metal adiciona no boxes metal
        }

        //papelao 17 max
        //metal 8 max
        int papelao=0, metal=0;

        if(idWave>0){
            if(idWave <= 2){
                papelao=6;
                //ativar só algumas de papelao
            }
            else if(idWave <=5){
                papelao=12;
               //ativar mais algumas de papelao
            }
            else if(idWave <=8){
                papelao=17;
                //ativar todas de papelao
            }
            else if(idWave <=12){
                metal=4;
                //ativar algumas de metal
            }
            else{
                metal=8;
                //ativar todas de metal
            }
        }

        for(int i=0; i<papelao;i++){
            //ativa as de papelao
            int rnd=Random.Range(0, 17);
            while(boxesPapelao[rnd].status=="Ready" || boxesPapelao[rnd].status=="Loading"){
                rnd=Random.Range(0, 17);
            }
            boxesPapelao[rnd].status="Ready";
        }

        for(int i=0; i<metal;i++){
            //ativa as de metal
            int rnd=Random.Range(0, 17);
            while(boxesMetal[rnd].status=="Ready" || boxesMetal[rnd].status=="Loading"){
                rnd=Random.Range(0, 17);
            }
            boxesMetal[rnd].status="Ready";
        }

    }
    
}
