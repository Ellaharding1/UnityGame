using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    public TMP_Text title;
    public string level;

    public TMP_Text displayMessage;
    public string message;

    // Start is called before the first frame update
    void Start()
    {
        title.SetText(level);

        StartCoroutine(clearTitle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator clearTitle()
    {
        yield return new WaitForSeconds(3);

        //display the second message 
        title.SetText("");

        yield return new WaitForSeconds(1);

        displayMessage.SetText(message);

        yield return new WaitForSeconds(5);

        displayMessage.SetText("");
    }
}
