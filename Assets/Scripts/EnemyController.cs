using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyType
{
    Goblin,
    Eye
}


public class EnemyController : MonoBehaviour
{
    //Enemy Type
    public EnemyType enemyType = EnemyType.Goblin;

    //Components
    private Transform groundDetection;
    private SpriteRenderer spriteRenderer;
    private Animator playerAnimator;
    [Header("Enemy Health")]
    public int enemyHealth = 3;
    private bool canTakeDamage = true;
    //Sprite blinking speed for damage
    public float blinkSpeed = 0.15f;
    //Timer for the death animation
    [SerializeField] private float maxTime;
    private float actualTime;
    private bool isTimeActive;

    //Ground check
    private LayerMask layer;

    //Movement
    public Vector2 direction = Vector2.right;
    public float moveSpeed = 1;

    private void Awake()
    {
        groundDetection = transform.GetChild(1);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        layer = LayerMask.GetMask("Ground");
        playerAnimator = GetComponentInChildren<Animator>();
        DeactivateCounter();


    }
    /// <summary>
    /// Detect if the enemy touch the floor 
    /// </summary>
    /// <returns></returns>
    private bool GroundDetection()
    {
        return Physics2D.Raycast(groundDetection.position, Vector2.down, 1f, layer);
    }

    /// <summary>
    /// Detect if the enemy touch the ceiling
    /// </summary>
    /// <returns></returns>
    private bool CeilingDetection()
    {
        return Physics2D.Raycast(groundDetection.position, Vector2.up, 1f, layer);
    }

    private bool WallDetection()
    { 
        Vector2 direction = Vector2.zero;

        if (spriteRenderer.flipX)
        {
            direction = Vector2.right;
        }
        else if (!spriteRenderer.flipX)
        {
            direction = Vector2.left;
        }

        //direction = (spriteRenderer.flipX) ? Vector2.right : Vector2.left;

        return Physics2D.Raycast(groundDetection.position, direction, 0.1f, layer);
    }

    private void Update()
    {
        EnemyMove();
        if (isTimeActive)
        {
            ChangeCounter();
        }

    }

    private void EnemyMove()
    {
        //Goblin Detection and change direction
        if (enemyType.Equals(EnemyType.Goblin))
        {

            //If not detect ground or detect wall change direction
            if (!GroundDetection() || WallDetection())
            {
                direction = -direction;
                spriteRenderer.flipX = !spriteRenderer.flipX;
                //Invert the groundDetection X point
                groundDetection.transform.localPosition = new Vector3(-groundDetection.transform.localPosition.x, groundDetection.transform.localPosition.y, groundDetection.localPosition.z);
            }
        }
        //EYE Detection and change direction
        else if (enemyType.Equals(EnemyType.Eye))
        {
            //Going UP
            if (direction.y > 0)
            {
                if (CeilingDetection())
                {
                    direction = -direction;
                    //Invert the groundDetection Y point
                    groundDetection.transform.localPosition = new Vector3(groundDetection.transform.localPosition.x, -groundDetection.transform.localPosition.y, groundDetection.localPosition.z);
                }
            }
            //Going Down
            else if (direction.y < 0)
            {
                if (GroundDetection())
                {
                    direction = -direction;
                    //Invert the groundDetection Y point
                    groundDetection.transform.localPosition = new Vector3(groundDetection.transform.localPosition.x, -groundDetection.transform.localPosition.y, groundDetection.localPosition.z);
                }
            }

            
        }//Eye

        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

    }//EnemyMove


    /// <summary>
    /// Plays different animations depending on the enemy
    /// </summary>
    /// 
    public EnemyType EnemySelector()
    {
        if (enemyType.Equals(EnemyType.Goblin))
        {
            playerAnimator.Play("GoblinRun");
            enemyType = EnemyType.Goblin;
        }
        else if(enemyType.Equals(EnemyType.Eye))
        {
            playerAnimator.Play("New Animation");
            enemyType = EnemyType.Eye;
        }
        return enemyType;
    }

    /// <summary>
    /// Controls enemy health
    /// </summary>
    public void EnemyTakesDamage()
    {
       
            //reduce health
            enemyHealth--;
            StartCoroutine(BlinkSprite(4));
        if (enemyHealth <= 0)
            {   
                if (EnemySelector() == EnemyType.Goblin) 
                {
                  playerAnimator.Play("GoblinDies");
                }
                else 
                {
                playerAnimator.Play("EyeDies");

            }
            ActivateCounter();

            }

    }
    /// <summary>
    /// Timer settings for the death animation
    /// </summary>
    private void ChangeCounter()
    {
        actualTime -= Time.deltaTime;
        if (actualTime <= 0)
        {
            Destroy(gameObject);

            isTimeActive = false;
        }
    }
    private void ChangeTimer(bool status)
    {
        isTimeActive = status;
    }
    private void ActivateCounter()
    {
        actualTime = maxTime;
        ChangeTimer(true);
    }
    private void DeactivateCounter()
    {
        ChangeTimer(false);
    }

    private IEnumerator BlinkSprite(int blinkTimes)
    {
        canTakeDamage = false;
        //Repat blinkTimes times
        do
        {
            spriteRenderer.color = Color.grey;
            yield return new WaitForSeconds(blinkSpeed);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(blinkSpeed);
            blinkTimes--;
        } while (blinkTimes > 0);

        canTakeDamage = true;
    }
 
    /// <summary>
    /// Damages the player on collision
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }

    /// <summary>
    /// Change the Animation in order to action
    /// </summary>

}
