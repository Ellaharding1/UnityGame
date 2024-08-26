using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level3DoorOpen_Leave : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "princess")
        {
            SceneManager.LoadScene(3);
        }
    }
   
    // Update is called once per frame
    void Update()
    {

    }
}
