using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Character2DController controller = other.GetComponent<Character2DController>();
            if(controller != null)
            {

            }
            Destroy(gameObject);
        }
    }
}
