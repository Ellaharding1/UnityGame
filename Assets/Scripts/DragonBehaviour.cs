using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    //dragons cause 0.3f damage 

    public Image healthBar;
    public TMP_Text message;

    float damage = 0.3f;

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
        if (other.CompareTag("fireball"))
        {
            healthBar.fillAmount -= damage;
            Destroy(other.gameObject);

            if (healthBar.fillAmount <= 0.01f)
            {
                healthBar.fillAmount = 0f;

                //display game over message and reload scene 

                message.SetText("Game over. Try again.");

                StartCoroutine(clearText());

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator clearText()
    {
        yield return new WaitForSeconds(3);

        message.SetText("");
    }
}
