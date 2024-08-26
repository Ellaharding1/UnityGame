using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class pinkPotionScript : MonoBehaviour
{
    public GameObject Princess;
    public Image healthBar;

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
        if (other.tag == "princess")
        {
            if (healthBar.fillAmount == 1f)
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
