using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    int idRobot;

    public List<ItemSlot> robotParts = new List<ItemSlot>();
    bool done;



    List<Transform> points = new List<Transform>();

    public RobotManager robotManager;

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

        //pegar robotManager
        robotManager = GameObject.Find("RobotManager").GetComponent<RobotManager>();

        //posiçao do robo = ponto inicial
        transform.position = points[0].position;

        done=false;
        
        int wave= GameObject.Find("Wave").GetComponent<WaveScript>().idWave;
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
            int quantidadeItens = Random.Range(min, max+1);

            for(int i = 0; i < quantidadeItens; i++)
            {
                int newItem = Random.Range(0, robotManager.currentItems.Count + 1);

                ItemSlot newSlot = (ItemSlot)ScriptableObject.CreateInstance("ItemSlot");

                newSlot.idItemType = Random.Range(0, robotManager.maxItemTypeId + 1);
                newSlot.filled = false;


                robotParts.Add(newSlot);
            }
            
        }
        
        //habilitar numero certo de buracos
        for(int i = 0; i < robotParts.Count; i++)
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

                if(robots.Count == 1)
                {
                    GameObject.Find("Wave").GetComponent<WaveScript>().restarted = false;
                }
                robots.Dequeue();

            }
            Destroy(gameObject);
        }

        if(done==true){
            GameObject.Find("Wave").GetComponent<WaveScript>().done[GameObject.Find("Wave").GetComponent<WaveScript>().idWave]+=1;
        }

    }

    public bool insertPart(Item insertedPart){

        bool anySlotFilled = false;
        //para cada slot de item
        for(int i = 0; i < robotParts.Count; i++)
        {
            print(i);
            //se o id do item for diferente do pedido, passar pro próximo slot
            

            //se o id do item colocado for igual o id pedido pelo slot, e nenhum item ja foi colocado
            if(!(insertedPart.idItemType != robotParts[i].idItemType))
            {
            if (anySlotFilled == false && robotParts[i].filled == false)
            {

                //marcar que ja preencheu um slot
                anySlotFilled = true;

                //trocar o sprite
                holes[i].color = Color.white;
                holes[i].sprite = insertedPart.sprite;

                //marcar como colocado
                robotParts[i].filled = true;

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
