using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BulletCollisionBehaviour : MonoBehaviour
{
    public Image healthBar;
    float hitDamage = 0.1f;
    
    public TMP_Text message;

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
        //if the princess collides with a GameObject with the name 'gnomeWeapon', she takes health damage 
        if (other.CompareTag("gnomeWeapon"))
        {
            healthBar.fillAmount -= hitDamage;

            Destroy(other.gameObject);

            if (healthBar.fillAmount <= 0.01f)
            {
                message.SetText("Game over. Try again.");

                StartCoroutine(clearText());

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator clearText()
    {
        yield return new WaitForSeconds(4);

        message.SetText("");
    }
}
