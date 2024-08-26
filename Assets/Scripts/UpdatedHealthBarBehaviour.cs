using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdatedHealthBarBehaviour : MonoBehaviour
{
    public Image healthBar;

    //specify health damage or boost based on enemy or pickup  
    float gnomeDamage = 0.2f;
    float elfDamage = 0.3f;
    float dragonDamage = 0.4f;
    float potionBoost = 0.3f;

    public TMP_Text display; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //code for collision with potion 
        if(collision.gameObject.tag == "potion")
        {
            if(healthBar.fillAmount >= 1f)
            {
                //nothing happens, health bar cannot increase anymore 
            }
        }
        else
        {
            healthBar.fillAmount += potionBoost;


            healthBar.fillAmount = Mathf.Clamp01(healthBar.fillAmount);

            Destroy(collision.gameObject);
        }

        //code for collision with gnome (first level)
        if (collision.gameObject.tag == "Enemy")
        {
            //if health bar is less than or equal to 10 percent, the game is over 

            if (healthBar.fillAmount <= 0.2f)
            {
                healthBar.fillAmount = 0f;
                Destroy(collision.gameObject);

                //the game is over, and the scene is reloaded 
                display.SetText("Game over. Try again.");

                StartCoroutine(clearText());


            }
            else
            {
                healthBar.fillAmount -= gnomeDamage;

                healthBar.fillAmount = Mathf.Clamp01(healthBar.fillAmount);

                Destroy(collision.gameObject);
            }
        }

        //code for collision with elf (second level)
        if (collision.gameObject.tag == "ElfTag")
        {
            //if health bar is less than or equal to 10 percent, the game is over 

            if (healthBar.fillAmount <= 0.3f)
            {
                healthBar.fillAmount = 0f;
                Destroy(collision.gameObject);

                //the game is over, and the scene is reloaded 
                display.SetText("Game over. Try again.");

                StartCoroutine(clearText());


            }
            else
            {
                healthBar.fillAmount -= elfDamage;

                healthBar.fillAmount = Mathf.Clamp01(healthBar.fillAmount);

                Destroy(collision.gameObject);
            }
        }

        //code for collision with dragon 
        if (collision.gameObject.tag == "DragonTag")
        {
            if (healthBar.fillAmount <= 0.4f)
            {
                healthBar.fillAmount = 0f;
                Destroy(collision.gameObject);

                //the game is over, and the scene is reloaded 
                display.SetText("Game over. Try again.");

                StartCoroutine(clearText());


            }
            else
            {
                healthBar.fillAmount -= dragonDamage;

                healthBar.fillAmount = Mathf.Clamp01(healthBar.fillAmount);

                Destroy(collision.gameObject);
            }

        }
    }

    IEnumerator clearText()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
