using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
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
    public PlayerInput[] playerInput;
    public PhotonView pv;

    public string currentControlScheme;

    private Dictionary<string, int> controlSchemeToInt = new Dictionary<string, int>
    {
        { "KeyboardMouse", 0 },
        { "Gamepad", 1 },
        { "Touch", 2 },
        { "Joystick", 3 },
        { "XR", 4 }
        // Add more control schemes and their corresponding integers as needed
    };

    [SerializeField] int controlSchemeInt;

    public void Start()
    {
        playerInput = GameObject.FindObjectsOfType<PlayerInput>();
        currentSpeed = speed;
        currentPos = transform.position;
        //if (SceneManager.GetActiveScene().name != "Solo")
        //{
        //    pv = GetComponent<PhotonView>();
        //}
    }

    void Update()
    {
        playerInput = GameObject.FindObjectsOfType<PlayerInput>();

        //if (SceneManager.GetActiveScene().name != "Solo")
        //{
            if (pv.IsMine && GameManger.isRedy)
            {

                for (int i = 0; i < playerInput.Length; ++i)
                {
                    // อ่านค่า Input จาก Joystick
                    if (canMove)
                    {
                        moveInput = playerInput[i].actions["Move"].ReadValue<Vector2>();
                        // Debug.Log(moveInput);
                    }
                    if (playerInput[i].actions["Dash"].IsPressed() && !isDash)
                    {
                        StartCoroutine(waitChangeSpeed());
                        Debug.Log("Dash!!");
                    }
                }
            }
     
    }

    void FixedUpdate()
    {
        //if (SceneManager.GetActiveScene().name != "Solo")
        //{
            if (pv.IsMine && GameManger.isRedy)
            {
                for (int i = 0; i < playerInput.Length; ++i)
                {
                    currentControlScheme = playerInput[i].currentControlScheme;

                    if (controlSchemeToInt.TryGetValue(currentControlScheme, out controlSchemeInt))
                    {
                        Debug.Log(controlSchemeInt);
                    }
                }

                //if(playerInput.currentControlScheme)

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
        //}
        //if (SceneManager.GetActiveScene().name == "Solo")
        //{
        //    Vector2 newPos = moveInput * currentSpeed;
        //    rigi.velocity = newPos;
        //    currentPos = newPos;

        //    if (moveInput.magnitude < moveThreshold)
        //    {
        //        // No movement input detected
        //        // You can add your code here to handle this case
        //        rigi.velocity = Vector2.zero;
        //        Debug.Log("No movement input detected.");
        //    }

        //}
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

