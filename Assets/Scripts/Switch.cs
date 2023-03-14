using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject objectToSwitch;
    private SpriteRenderer spriteRenderer;
    public Sprite downSprite;
    private bool hasSwitched;
    public bool deactiveOnSwitch;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && !hasSwitched)
        {
            if(!deactiveOnSwitch)
            {
                objectToSwitch.SetActive(false);
            }
            else
            {
                objectToSwitch.SetActive(true);
            }

            spriteRenderer.sprite = downSprite;
            hasSwitched = true;
        }
    }
}
