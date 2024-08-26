using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class HealthBarBehaviour : MonoBehaviour
{
    //use a controller to determine when the main character collides with weapons or health items 
    CharacterController controller;

    public Image healthBar;
    public float healthAmount = 100f;

    //test to see if collisions are being recognized 
    public TMP_Text test;

    //values defined by the user 
    int damage;
    int heal;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the user has health less than 0, they restart the level and the scene reloads 
        if (healthAmount <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            DecreaseHealth(damage);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseHealth(heal);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "potion")
        {
            test.SetText("potion collected");
            Destroy(gameObject); //This Destroys
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //a collision with a gnome causes the player's health bar to decrement by 2 units 
        //with a elf causes the health bar to decrement by 5 
        //with the dragon causes the health bar to decrement by 10 

        //picking up a health potion increments the player's health bar by 3 units 

        if(collision.gameObject.tag == "potion")
        {

            test.SetText("potion picked up");
            if (healthAmount == 100f)
            {
                //nothing happens 
            }
            else
            {
                healthAmount += 30f;
            }

            //Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "gnome")
        {
            //decrement the health bar, then check the main player's health
            //if healthAmount is < or == 0, restart the level 

            test.SetText("collision between main charater and gnome");
            //healthAmount -= 30f;
            Destroy(collision.gameObject);
        }
    }

    public void DecreaseHealth(int damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void IncreaseHealth(int heal)
    {
        healthAmount += heal;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }
}