using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class greenPotionScript : MonoBehaviour
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
        if (other.name == "PrincessLevel3")
        {
            potionSound.Play();
            if (healthBar.fillAmount == 1f)
            {
                Destroy(gameObject);
            }
            else
            {
                healthBar.fillAmount += 0.5f;
                Destroy(gameObject);

            }
        }
    }
}
