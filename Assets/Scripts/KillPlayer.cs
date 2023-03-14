using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            LevelManager.instance.RespawnPlayer();
            // PlayerHealthController.instance.DealFallDamage(1);
            // UIController.instance.UpdateHealthDisplay();
        }
    }
}
