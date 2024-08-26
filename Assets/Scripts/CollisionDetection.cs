using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;
    public GameObject HitParticle;
    public AudioSource enemyDeathSound;

    int count = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && wc.isAttacking)
        {
            count++;
            //enemyDeathSound.Play();
            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");
            print("count"+ count);

            if (count >= 2)
            {
                count = 0;
                Destroy(other.gameObject);
            }
           
        }
    }
}
