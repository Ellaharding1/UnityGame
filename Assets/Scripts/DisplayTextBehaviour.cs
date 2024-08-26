using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DisplayTextBehaviour : MonoBehaviour
{
    //text that will be assigned on screen 
    // public string text;

    //link to a UI text element in Unity
    public TMP_Text display;

    //time text is displayed for 
    public float time;

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
        if (other.CompareTag("googles"))
        {
            display.SetText("Press 'n' to access night vision");

            Destroy(other.gameObject);
            StartCoroutine(clearText());
        }
    }

    IEnumerator clearText()
    {
        yield return new WaitForSeconds(3);

        display.SetText("");
    }
}
