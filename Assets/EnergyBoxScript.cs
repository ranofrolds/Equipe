using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int energia=100;
    public string status="Full";
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void carregarEnergia(int waveDifficulty){
        if(energia<=20){
            StartCoroutine(delayCharge(1/(waveDifficulty*6)));
        }
    }

    IEnumerator delayCharge(float delay)
    {
        while(energia<100){
            yield return new WaitForSeconds(delay);
            energia+=1;
        }
        status="Full";
        
    }

    public IEnumerator delayDischarge(float delay, int waveDifficulty)
    {
        while(energia>0){
            yield return new WaitForSeconds(delay);
            energia-=waveDifficulty;
            if(energia<=0){
                energia=0;
            }
            else if(energia<=20){
                status="Critical";
            }
            
        }
        status="NoBattery";
    }
}
