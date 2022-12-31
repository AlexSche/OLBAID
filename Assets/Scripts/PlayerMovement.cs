using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public CharacterController characterController;
    private Vector3 targetPosition;
    private Animator animator;
    private PlayerAttack playerAttack;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            locateMousePositionOnTerrain();
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            playerAttack.useBasicAttack();
        }
        else if (Input.GetKey(KeyCode.Alpha1)) {
            playerAttack.useWhipWhirl();
        }
        moveToPosition();
    }

    void locateMousePositionOnTerrain()
    {
        //shoot ray from camera to mouse click on terrain
        //on terrain hit get that positon
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000)) {
            Debug.Log(hit.collider.tag);
            if (hit.collider.tag == "Floor") {
            //fix y to the players position so it can't walk "up"
            targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
        }
    }

    void moveToPosition() 
    {
        rotatePlayer();
        movePlayerTowardsPosition();
    }

    void rotatePlayer() {
        //rotate player into the target location
        Quaternion playerRotation = Quaternion.LookRotation(targetPosition-transform.position);
        playerRotation.x = 0f;
        playerRotation.z = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, Time.deltaTime * 10);
    }

    void movePlayerTowardsPosition() {
        Vector3 offset = targetPosition - transform.position;
        if (offset.magnitude > .1f) {
        //If we're further away than .1 unit, move towards the target.
        //The minimum allowable tolerance varies with the speed of the object and the framerate. 
        // 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
        offset = offset.normalized * movementSpeed * Time.smoothDeltaTime;
        //normalize it and account for movement speed.
        //actually move the character.
        characterController.Move(offset);
        //play walking animation
        animator.SetBool("isWalking", true);
        } else {
            animator.SetBool("isWalking", false);
        }
    }
}
