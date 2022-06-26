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

    int waveDifficulty;
    


    void Start()
    {
        restarted=false;
        idWave=1;
        done.Add(0);
        generateWaveBox();
        GameObject currentRobot = Instantiate(robot);
        generateRobot(currentRobot.GetComponent<RobotScript>());
        robots.Enqueue(currentRobot);
        robotManager.robots.Add(currentRobot.GetComponent<RobotScript>());
        
        StartCoroutine(delay(delayBetweenRobots));
        
    }

    void Restart(){
        idWave++;
        done.Add(0);        
        generateWaveBox();
        GameObject currentRobot = Instantiate(robot);
        generateRobot(currentRobot.GetComponent<RobotScript>());
        robots.Enqueue(currentRobot);
        robotManager.robots.Add(currentRobot.GetComponent<RobotScript>());
        StartCoroutine(delay(delayBetweenRobots));
    }


    void Update()
    {
        print(GameObject.Find("box27").GetComponent<EnergyBoxScript>().energia);
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
        for(int i=1; i<(idWave*2)+1; i++){
            yield return new WaitForSeconds(delay);
            
            GameObject currentRobot = Instantiate(robot);
            generateRobot(currentRobot.GetComponent<RobotScript>());
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

        if(idWave>0)
        {
            if(idWave == 1){
                waveDifficulty=1;
                papelao=6;
                //ativar só algumas de papelao
            }
            else if(idWave == 3){
                waveDifficulty=2;
                papelao=3;
               //ativar mais algumas de papelao
            }
            else if(idWave == 5){
                waveDifficulty=3;
                papelao=4;
               //ativar mais algumas de papelao
            }
            else if(idWave ==7){
                 waveDifficulty=4;
                papelao=4;
                //ativar todas de papelao
            }
            else if(idWave ==10){
                 waveDifficulty=5;
                metal=1;
                //ativar algumas de metal
            }
            else if(idWave ==13){
                 waveDifficulty=6;
                metal=2;
                //ativar algumas de metal
            }
            else if(idWave == 18)
            {
                waveDifficulty=7;
                metal=4;
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
            int rnd=Random.Range(0, 7);
            while(boxesMetal[rnd].status=="Ready" || boxesMetal[rnd].status=="Loading"){
                rnd=Random.Range(0, 7);
            }
            boxesMetal[rnd].status="Ready";
        }

    }

    void generateRobot(RobotScript robotScript)
    {
        //pegar lista de buracos
        for(int i = 0; i < robotScript.transform.childCount; i++)
        {
            robotScript.holes.Add(robotScript.transform.GetChild(i).GetComponent<SpriteRenderer>());
            robotScript.holes[i].enabled = false;
        }


        int wave= idWave;
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

        
        if(min !=0 && max!=0 )
        {
            int quantidadeItens = Random.Range(min, max+1);

            for(int i = 0; i < quantidadeItens; i++)
            {

                //Criar novo slot de item
                ItemSlot newSlot = (ItemSlot)ScriptableObject.CreateInstance("ItemSlot");

                //gerar random o tipo de item que vai ser
                newSlot.idRequestedItemType = Random.Range(0, robotManager.maxItemTypeId + 1);
                //definir que o slot não foi preenchido
                newSlot.filled = false;

                //btaço
                if(newSlot.idRequestedItemType == 0) newSlot.icon = armIcon;
                //bola
                else if(newSlot.idRequestedItemType == 1) newSlot.icon = ballIcon;

                //adicionar slot a lista de slots
                robotScript.robotParts.Add(newSlot);

                //Aumentar o score máximo
                robotScript.maxScore += 10;
            }
            
        }
        
        //habilitar numero certo de buracos
        for(int i = 0; i < robotScript.robotParts.Count; i++)
        {
            robotScript.holes[i].enabled = true;
            robotScript.holes[i].sprite = robotScript.robotParts[i].icon;
        }

    }
    
}
