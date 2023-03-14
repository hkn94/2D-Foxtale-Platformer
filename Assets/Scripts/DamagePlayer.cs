using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    PlayerHealthController playerHealthController;

    public int damageDealt;

    void Start()
    {
        playerHealthController = FindObjectOfType<PlayerHealthController>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            PlayerHealthController.instance.DealDamage(damageDealt);
        }
    }
}
