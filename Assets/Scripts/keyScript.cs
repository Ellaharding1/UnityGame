using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyScript : MonoBehaviour
{
    public GameObject doorCollider;
    public AudioSource keySound;
    // Start is called before the first frame update
    void Start()
    {
        doorCollider.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "princess")
        {
            keySound.Play();
            doorCollider.SetActive(true);
            Destroy(gameObject);
        }
    }
}
