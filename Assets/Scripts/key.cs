using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    private bool isFollowing;
    public float followSpeed;
    public Transform followTarget;



    void Start()
    {
        
    }

    void Update()
    {
        if(isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            if (!isFollowing) 
            {
                Character2DController Player = FindObjectOfType<Character2DController>();

                followTarget = Player.KeyFollowPoint;

                isFollowing = true;

                Player.followingKey = this;

            }
        }
    }
}
