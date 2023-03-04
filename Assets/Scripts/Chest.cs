using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Character2DController Player;

    void Start()
    {
        Player = FindObjectOfType<Character2DController>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Player.followingKey != null) 
            {
                Player.followingKey.followTarget = transform;
            }
        }
    }
}
