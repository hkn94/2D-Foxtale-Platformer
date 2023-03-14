using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invicibleCounter;

    private SpriteRenderer sR;

    private Animator animator;

    private bool playerDeath;

    public GameObject deathEffect;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerDeath = false;
        currentHealth = maxHealth;
        sR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (invicibleCounter > 0)
        {
            invicibleCounter -= Time.deltaTime;
            if (invicibleCounter <= 0)
            {
                sR.color = new Color(sR.color.r, sR.color.g, sR.color.b, 1f);
            }
        }
    }

    public void DealDamage(int damage)
    {
        if (invicibleCounter <= 0)
        {
            currentHealth -= 1 * damage;
            AudioManager.instance.PlaySFX(9);

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                Instantiate(deathEffect, transform.position, transform.rotation);

                LevelManager.instance.RespawnPlayer();
                // gameObject.SetActive(false);
            }
            else
            {
                invicibleCounter = invincibleLength;
                sR.color = new Color(sR.color.r, sR.color.g, sR.color.b, 0.75f);
                PlayerController.instance.KnockBack();
            }

            UIController.instance.UpdateHealthDisplay();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHealthDisplay();
    }
}
