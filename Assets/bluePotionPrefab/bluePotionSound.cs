using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bluePotionSound : MonoBehaviour
{
    public AudioSource potionSound2;
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
        if (other.tag == "bluePotion")
        {
            potionSound2.Play();
        }
    }
}
