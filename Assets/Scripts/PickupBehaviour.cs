using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    private InventoryBehaviour inventory;
    public GameObject itemButton;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("princess").GetComponent<InventoryBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("princess"))
        {
            for (int i = 0; i < inventory.entry.Length; i++)
            {
                if (inventory.full[i] == false)
                {
                    //add item to the inventory 
                    inventory.full[i] = true;

                    //instantiate button, and state that it is parented to the inventory spot of index i 
                    Instantiate(itemButton, inventory.entry[i].transform);
                    Destroy(gameObject);
                    break;
                }

            }
        }
    }
}
