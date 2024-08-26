using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKeyOpenDoor : MonoBehaviour
{
    
    public GameObject AnimeObject;
    public bool Action = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Action = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Action = true;
        if (other.tag == "princess")
        {
            AnimeObject.GetComponent<Animator>().Play("DoorOpen");
            Action = false;

      
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        Action = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

}
