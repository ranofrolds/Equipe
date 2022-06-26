using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
   
    public Sprite armIcon, ballIcon;

    [Space(15)]

    public GameObject robot;
    public int idWave;
    public Queue<GameObject> robots=new Queue<GameObject>();

    public List<int>done=new List<int>();
    public float speed;
    
    public int score;
    public int maxScore;
    public bool restarted=false;


    public float delayBetweenRobots = 3;
    public float delayBetweenWaves = 5;


    public RobotManager robotManager;

    public int waveDifficulty;

    public SidebarMANAGER sidebarMANAGER;

    public GameObject request;
    


    void Start()
    {
        restarted=false;
        idWave=1;
        done.Add(0);
        generateWaveBox();
        GameObject currentRobot = Instantiate(robot);
        //generateRobot(currentRobot.GetComponent<RobotScript>());
            sidebarMANAGER.requests.Add(Instantiate(request).transform);
        robots.Enqueue(currentRobot);
        robotManager.robots.Add(currentRobot.GetComponent<RobotScript>());
        
        StartCoroutine(delay(delayBetweenRobots));
        
    }

    void Restart(){
        idWave++;
        done.Add(0);        
        generateWaveBox();
        GameObject currentRobot = Instantiate(robot);
        //generateRobot(currentRobot.GetComponent<RobotScript>());
            sidebarMANAGER.requests.Add(Instantiate(request).transform);
        robots.Enqueue(currentRobot);
        robotManager.robots.Add(currentRobot.GetComponent<RobotScript>());
        StartCoroutine(delay(delayBetweenRobots));
    }


    void Update()
    {

        //TEMPORARIO - TIRAR ISSO ANTES DE BUILDAR O JOGO
        if(Input.GetKeyDown(KeyCode.R)) UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        //print(GameObject.Find("box27").GetComponent<EnergyBoxScript>().energia);
        if(robots.Count==0 && restarted==false){
            //BOTAR UM DELAY PARA COMEÇAR PROXIMA WAVE VVV
            restarted=true;
            StartCoroutine(delayNextWave(delayBetweenWaves));
        }

        if(GameObject.Find("box27").GetComponent<EnergyBoxScript>().status=="Full"){
            GameObject.Find("box27").GetComponent<EnergyBoxScript>().status="OK";
            StartCoroutine(GameObject.Find("box27").GetComponent<EnergyBoxScript>().delayDischarge(1,waveDifficulty));
        }
    }

    IEnumerator delay(float delay)
    {

        int maxRobots = idWave*2;
        maxRobots = Mathf.Clamp(maxRobots, 0, 6);

        for(int i=1; i<(maxRobots)+1; i++){
            yield return new WaitForSeconds(delay);
            
            GameObject currentRobot = Instantiate(robot);
            //generateRobot(currentRobot.GetComponent<RobotScript>());
            sidebarMANAGER.requests.Add(Instantiate(request).transform);
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
        //metal 7 max
        int papelao=0, metal=0;
        Item newItem=null;
        if(idWave>0)
        {
            if(idWave == 1){
                

                waveDifficulty=1;
                papelao=6;
                //ativar só algumas de papelao
            }
            else if(idWave == 3){
                newItem=robotManager.allItems.Dequeue();
                waveDifficulty=2;
                papelao=3;
               //ativar mais algumas de papelao
            }
            else if(idWave == 5){
                newItem=robotManager.allItems.Dequeue();
                waveDifficulty=3;
                papelao=4;
               //ativar mais algumas de papelao
            }
            else if(idWave ==7){
                newItem=robotManager.allItems.Dequeue();
                 waveDifficulty=4;
                papelao=4;
                //ativar todas de papelao
            }
            else if(idWave ==10){
                newItem=robotManager.allItems.Dequeue();
                 waveDifficulty=5;
                metal=1;
                //ativar algumas de metal
            }
            else if(idWave ==13){
                newItem=robotManager.allItems.Dequeue();
                 waveDifficulty=6;
                metal=1;
                //ativar algumas de metal
            }
            else if(idWave ==15){
                newItem=robotManager.allItems.Dequeue();
                 waveDifficulty=7;
                metal=1;
                //ativar algumas de metal
            }
            else if(idWave == 18)
            {
                newItem=robotManager.allItems.Dequeue();
                waveDifficulty=8;
                metal=2;
                //ativar todas de metal
            }
            else if(idWave == 20)
            {
                newItem=robotManager.allItems.Dequeue();
                waveDifficulty=9;
                metal=3;
                //ativar todas de metal
            }
        }

        //Na wave 1, pegar 1 de cada
        if(newItem!=null){
            robotManager.currentItems.Add(newItem);
            switch(newItem.type){
                    case "Arm":
                    robotManager.currentArms.Add(newItem);
                    break;
                    case "Body":
                    robotManager.currentBodys.Add(newItem);
                    break;
                    case "Leg":
                    robotManager.currentLegs.Add(newItem);
                    break;
                    case "Head":
                    robotManager.currentHeads.Add(newItem);
                    break;


            }
        }
        


        int randomItem = Random.Range(0, robotManager.currentItems.Count);

        for(int i=0; i<papelao;i++){

            randomItem++;
            if(randomItem == robotManager.currentItems.Count) randomItem = 0;


            //ativa as de papelao
            int rnd=Random.Range(0, 17);
            

            while(boxesPapelao[rnd].status=="Ready" || boxesPapelao[rnd].status=="Loading"){
                rnd=Random.Range(0, 17);
            }
            boxesPapelao[rnd].status="Ready";
            boxesPapelao[rnd].item = robotManager.currentItems[randomItem];
        }

        for(int i=0; i<metal;i++){

            randomItem++;
            if(randomItem == robotManager.currentItems.Count) randomItem = 0;


            //ativa as de metal
            int rnd=Random.Range(0, 7);
            
            while(boxesMetal[rnd].status=="Ready" || boxesMetal[rnd].status=="Loading"){
                rnd=Random.Range(0, 7);
            }
            boxesMetal[rnd].status="Ready";
            boxesMetal[rnd].item = robotManager.currentItems[randomItem];
        }

    }

    
}
