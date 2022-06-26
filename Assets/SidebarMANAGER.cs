using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidebarMANAGER : MonoBehaviour
{
    
    public List<Transform> transforms = new List<Transform>();
    public List<Transform> requests = new List<Transform>();

    public Animator anim;

    float subir = 1.5f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {

        for(int i = 0; i < requests.Count; i++)
        {
            requests[i].position = transforms[i].position;
        }




    }



}
