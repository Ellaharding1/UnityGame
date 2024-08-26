using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollitionDetection2 : MonoBehaviour
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
            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");
            print("count" + count);
            Destroy(other.gameObject);
            Destroy(gameObject);

            if (count >= 2)
            {
                count = 0;
                print("TESTAGAIN");
                Destroy(other.gameObject);
            }

        }
    }

}
