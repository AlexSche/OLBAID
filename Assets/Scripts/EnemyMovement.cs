using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed;
    public Transform playerTransform;
    private CharacterController characterController;
    private Rigidbody2D rb2D;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rotateEnemy();
        moveEnemyToPlayer();
    }
    void rotateEnemy() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Vector2 direction = playerTransform.position - transform.position;
        Vector3 topFromPlayer = new Vector3(transform.position.x, transform.position.y + 1,transform.position.z);
        Vector2 rightDirection = topFromPlayer - transform.position;
        float angle = Vector2.Angle(rightDirection, direction);
        if (playerTransform.position.x < transform.position.x) {
            angle *= -1;
        }
        if (angle < 0) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    void moveEnemyToPlayer() {
        if (Vector3.Distance(transform.position, playerTransform.position) > 0.5f) 
        {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction = Vector2.ClampMagnitude(direction, 1);
        Vector2 newPos = transform.position + direction * movementSpeed * Time.fixedDeltaTime;
        rb2D.MovePosition(newPos);
        //play walking animation
        animator.SetBool("isWalking", true);
        } else {
            Debug.DrawLine(transform.position, playerTransform.position, Color.green, 4);
            animator.SetBool("isWalking", false);
        }
    }
}
