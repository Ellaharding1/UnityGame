using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class bluePotionScript : MonoBehaviour
{
    public GameObject PrincessVarient;
    public Image healthBar;
    public AudioSource potionSound;

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
        if (other.CompareTag("princess"))
        {
            potionSound.Play();
            if(healthBar.fillAmount == 1f)
            {
                Destroy(gameObject);
            }
            else
            {
                healthBar.fillAmount += 0.2f;
                Destroy(gameObject);

            }
        }
    }
}
