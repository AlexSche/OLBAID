using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 2f;
    public Tilemap map;
    private MouseInput mouseInput;
    private Vector3 targetPosition;
    private Animator animator;
    private PlayerAttack playerAttack;

    private void Awake() => mouseInput = new MouseInput();
    private void OnEnable() => mouseInput.Enable();
    private void OnDisable() => mouseInput.Disable();
    void Start()
    {        
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        targetPosition = transform.position;
        mouseInput.Mouse.MouseClick.performed += context => mouseClick(context);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            playerAttack.useBasicAttack();
        }
        else if (Input.GetKey(KeyCode.Alpha1)) {
            playerAttack.useWhipWhirl();
        }
        movePlayerTowardsPosition();
    }

    private void mouseClick(CallbackContext context) {
        //find position clicked on screen
        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        //add z to mousePosition (need to add camera distance);
        Vector3 zPosition = new Vector3(mousePosition.x,mousePosition.y,Mathf.Abs(Camera.main.transform.position.z));
        //translate position on screen to gameworld position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(zPosition);
        //translate worldPosition to gridPosition
        Vector3Int gridPosition = map.WorldToCell(worldPosition);
        //check if clicked position is on a tile
        if (map.HasTile(gridPosition)) {
            //remove depth movement
            worldPosition.z = transform.position.z;
            targetPosition = worldPosition;
        }
        rotatePlayer();
    }

    void rotatePlayer() {
        Vector2 direction = targetPosition - transform.position;
        Vector3 topFromPlayer = new Vector3(transform.position.x, transform.position.y + 1,transform.position.z);
        Vector2 rightDirection = topFromPlayer - transform.position;
        float angle = Vector2.Angle(rightDirection, direction);
        if (targetPosition.x < transform.position.x) {
            angle *= -1;
        }
        transform.rotation = Quaternion.Euler(0,angle,0);
    }

    void movePlayerTowardsPosition() {
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.smoothDeltaTime);
        //play walking animation
        animator.SetBool("isWalking", true);
        } else {
            animator.SetBool("isWalking", false);
        }
    }
}
