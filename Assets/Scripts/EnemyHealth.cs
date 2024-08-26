using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public string enemyTag; 
    public float enemyHealth;

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
        if(other.CompareTag(enemyTag))
        {
            //each time the enemy is hit by a bullet, their health decrements by 1
            enemyHealth--;

            //if the enemy reaches no health, the enemy dies 
            if (enemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
