using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Player.followingKey != null) 
            {
                Player.followingKey.followTarget = transform;
                SceneManager.LoadScene("Level 3");
            }
        }
    }
}
