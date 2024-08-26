using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    //make it so that this file can be reused for keys at each level 

    public Image key;
    // Start is called before the first frame update
    void Start()
    {
        key.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("princess"))
        {
            key.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
