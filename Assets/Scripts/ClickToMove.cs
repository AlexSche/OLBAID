using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.InputSystem.InputAction;

public class ClickToMove : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Tilemap map;
    private MouseInput mouseInput;
    private Vector3 targetPosition;

    private void Awake() => mouseInput = new MouseInput();
    private void OnEnable() => mouseInput.Enable();
    private void OnDisable() => mouseInput.Disable();
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        mouseInput.Mouse.MouseClick.performed += context => mouseClick(context);
    }

    // Update is called once per frame
    void Update()
    {
       move();
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
            targetPosition = worldPosition;
            //remove depth movement
            targetPosition.z = 0;
        }
    }

    private void move() {
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.smoothDeltaTime);
        }
    }
}
