using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.SceneManagement;

public class FireBehaviour : MonoBehaviour
{
    public Image healthBar;
    public Image shield;
    public float damage;
    public TMP_Text display;

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
            if (!shield.gameObject.activeSelf)
            {
                healthBar.fillAmount -= damage;
            }

            if(healthBar.fillAmount <= 0)
            {
                display.SetText("Game over. Try again.");

                StartCoroutine(clearText());

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator clearText()
    {
        yield return new WaitForSeconds(3);

        display.SetText("");
    }
}
