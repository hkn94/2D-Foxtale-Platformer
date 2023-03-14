using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHit : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Bounce");
        }
    }
}
