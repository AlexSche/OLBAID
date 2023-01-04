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
        //rotateEnemy();
        moveEnemyToPlayer();
    }
    void rotateEnemy() {
        //rotate player into the target location
        Quaternion enemyRotation = Quaternion.LookRotation(playerTransform.position-transform.position);
        enemyRotation.x = 0f;
        enemyRotation.z = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, enemyRotation, Time.deltaTime * 10);
    }

    void moveEnemyToPlayer() {
        if (Vector3.Distance(transform.position, playerTransform.position) > 0.75f) 
        {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction = Vector2.ClampMagnitude(direction, 1);
        Vector2 newPos = transform.position + direction * movementSpeed * Time.fixedDeltaTime;
        rb2D.MovePosition(newPos);
        //play walking animation
        animator.SetBool("isWalking", true);
        } else {
            animator.SetBool("isWalking", false);
        }
    }
}
