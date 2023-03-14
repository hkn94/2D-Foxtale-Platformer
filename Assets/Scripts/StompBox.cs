using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    public GameObject deathEffect;

    public GameObject collectible;
    [Range(0, 100)] public float changeToDrop;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy"))
        {
            other.transform.parent.gameObject.SetActive(false);

            Instantiate(deathEffect, other.transform.position, other.transform.rotation);

            PlayerController.instance.Bounce();

            float dropSelect = Random.Range(0, 100f);

            Vector3 cherryPosition = new Vector3(other.transform.position.x, other.transform.position.y - 0.25f, other.transform.position.z);

            if(dropSelect <= changeToDrop)
            {
                Instantiate(collectible, cherryPosition, other.transform.rotation);
            }

            AudioManager.instance.PlaySFX(3);
        }
    }
}
