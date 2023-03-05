using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{
    private Character2DController Player;
    
    LevelManager levelManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

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
                StartCoroutine(DelayCoroutine(1.50F));                
            }
        }
    }

    IEnumerator DelayCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        levelManager.NextScene();
    }
}
