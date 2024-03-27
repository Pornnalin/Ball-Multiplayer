using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSoloController : MonoBehaviour
{
    public float speed;
    public float currentSpeed;
    public float dash;
    private Vector2 moveInput;
    public Rigidbody2D rigi;
    public Vector2 currentPos;
    public bool isDash = false;
    private float moveThreshold = 0.1f;
    public bool canMove = true;
    public PlayerInput playerInput;



    public void Start()
    {
        // playerInput = FindObjectOfType<PlayerInput>();
        currentSpeed = speed;
        currentPos = transform.position;


        playerInput = GetComponent<PlayerInput>();
        //playerInput.SwitchCurrentControlScheme(ControllerManager.scheme);
        //Debug.Log(ControllerManager.scheme);

    }

    void Update()
    {

        if (GameManger.isRedy)
        {
            // อ่านค่า Input จาก Joystick

            moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
            Debug.Log(moveInput);

            if (playerInput.actions["Dash"].IsPressed() && !isDash)
            {
                StartCoroutine(waitChangeSpeed());
                Debug.Log("Dash!!");
            }

            Debug.Log(playerInput.currentControlScheme);

        }


    }

    void FixedUpdate()
    {
        //if (SceneManager.GetActiveScene().name != "Solo")
        //{
        if (GameManger.isRedy)
        {
            // ขยับตำแหน่งของ GameObject ตาม Input ที่รับเข้ามา
            Vector2 newPos = moveInput * currentSpeed;
            rigi.velocity = newPos;
            currentPos = newPos;

            if (moveInput.magnitude < moveThreshold)
            {
                // No movement input detected
                // You can add your code here to handle this case
                rigi.velocity = Vector2.zero;
                Debug.Log("No movement input detected.");
            }
            //   Debug.Log(newPos);
        }

    }
    IEnumerator waitChangeSpeed()
    {
        isDash = true;
        currentSpeed = dash;
        yield return new WaitForSeconds(1f);
        currentSpeed = speed;
        isDash = false;

    }
}
