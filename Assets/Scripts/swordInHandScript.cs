using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordInHandScript : MonoBehaviour
{
    public GameObject pickUpSword;
    public GameObject inHandSword;
    public GameObject Princess;
    public Transform holdPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "pickUpSword")
        {
            //Add 1 to points.
            Destroy(gameObject); //This Destroys
        }
    }
}
