using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Health")]
    public float health = 10;
    private bool canTakeDamage = true;
    [HideInInspector]public int powerUps = 0;

    [Header("Blink")]
    public float blinkSpeed = 0.15f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = transform.Find("PlayerSprite").GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Manage Player Damage 
    /// </summary>
    public void TakeDamage()
    {
        if (canTakeDamage) { 
            //reduce health
            health--;

            StartCoroutine(BlinkSprite(4));

            if (health <= 0)
            {
                GameManager.gameManager.LoseGame();
                
            }
        }

    }

    private IEnumerator BlinkSprite(int blinkTimes)
    {
        canTakeDamage = false;
        //Repat blinkTimes times
        do
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(blinkSpeed);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(blinkSpeed);
            blinkTimes--;
        }while (blinkTimes > 0);

        canTakeDamage = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            //Delete the GameObject Power Up from the Hierarchy Game
            Destroy(collision.gameObject);

            //Increment number of powerups collected
            powerUps++;
            
            //If all the powerUps collected call WinGame
            if (powerUps >= GameManager.gameManager.powerUpsCount)
            {
                GameManager.gameManager.WinGame();
            }

        }
    }
}
