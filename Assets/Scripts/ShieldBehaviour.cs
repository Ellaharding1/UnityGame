using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    public Image shield;

    // Start is called before the first frame update
    void Start()
    {
        shield.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shield"))
        {
            Destroy(other.gameObject);

            GetShield();
        }
    }

    private void GetShield()
    {
        shield.gameObject.SetActive(true);

        StartCoroutine(clearScreen());
    }

    IEnumerator clearScreen()
    {
        yield return new WaitForSeconds(10);

        shield.gameObject.SetActive(false);
    }
}
