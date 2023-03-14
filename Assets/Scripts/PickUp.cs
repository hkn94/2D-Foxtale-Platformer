using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool isGem, isCherry;

    private bool isCollected;

    public GameObject pickEffect;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            if (isGem)
            {
                AudioManager.instance.PlaySFX(6);
                LevelManager.instance.gemsCollected++;
                isCollected = true;
                Destroy(gameObject);
                Instantiate(pickEffect, transform.position, transform.rotation);
                UIController.instance.UpdateGemCount();
            }

            if (isCherry && PlayerHealthController.instance.currentHealth < PlayerHealthController.instance.maxHealth)
            {
                AudioManager.instance.PlaySFX(7);
                isCollected = true;
                if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.HealPlayer();
                }
                Destroy(gameObject);
                Instantiate(pickEffect, transform.position, transform.rotation);
            }
        }
    }
}
