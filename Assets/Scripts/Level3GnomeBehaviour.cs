using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3GnomeBehaviour : MonoBehaviour
{
    public Image healthBar;
    public TMP_Text restartMessage;

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
        //gnomes decrease princess health by 2 points 

        if (other.CompareTag("princess"))
        {
            if (healthBar.fillAmount <= 0.2f)
            {
                //player dies and level restarts 
                healthBar.fillAmount = 0f;
                Destroy(gameObject);

                restartMessage.SetText("Game over. Try again.");

                StartCoroutine(clearText());

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                healthBar.fillAmount -= 0.2f;
                healthBar.fillAmount = Mathf.Clamp01(healthBar.fillAmount);

                Destroy(gameObject);

                if (healthBar.fillAmount <= 0.01f)
                {
                    restartMessage.SetText("Game over. Try again.");

                    StartCoroutine(clearText());

                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }

    IEnumerator clearText()
    {
        yield return new WaitForSeconds(3);

        restartMessage.SetText("");

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
