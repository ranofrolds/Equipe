using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject robot;
    public int idWave;
    public Queue<GameObject> robots=new Queue<GameObject>();

    public List<int>done=new List<int>();
    public float speed;
    
    void Start()
    {
        idWave=1;
        done.Add(0);
        robots.Enqueue(Instantiate(robot));
        StartCoroutine(delay(1));
        
    }

    void Restart(){
        done.Add(0);
        robots.Enqueue(Instantiate(robot));
        StartCoroutine(delay(1));
    }

    // Update is called once per frame
    void Update()
    {
        if(robots.Count==0){
            idWave++;
            //BOTAR UM DELAY PARA COMEÇAR PROXIMA WAVE VVVVVVVVVVVVV
            Restart();
        }
    }

    IEnumerator delay(int delay)
    {
        for(int i=1; i<(idWave*2)+1; i++){
            yield return new WaitForSeconds(delay);
            
            robots.Enqueue(Instantiate(robot));
        }
    }
}
