using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBallCollision : MonoBehaviour
{
    //code so that everytime a ball collides with the wall, the ball is destroyed
    //code is linked with the wall prefab 

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
        if (other.CompareTag("gnomeWeapon"))
        {
            Destroy(other.gameObject);
        }
    }
}
