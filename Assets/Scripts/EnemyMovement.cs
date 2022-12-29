using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed;
    public Transform playerTransform;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        rotateEnemy();
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
        Vector3 offset = playerTransform.position - transform.position;
        if (offset.magnitude > .1f) {
        //If we're further away than .1 unit, move towards the target.
        //The minimum allowable tolerance varies with the speed of the object and the framerate. 
        // 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
        offset = offset.normalized * movementSpeed;
        //normalize it and account for movement speed.
        characterController.Move(offset * Time.deltaTime);
        //actually move the character.
        }
    }
}
