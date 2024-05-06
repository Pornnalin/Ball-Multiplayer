using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviourPun
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
    public PhotonView pv;
    public Color[] colors;
    public SpriteRenderer playerSprite;
    public TextMeshProUGUI nameText;
    public void Start()
    {
        // playerInput = FindObjectOfType<PlayerInput>();
        currentSpeed = speed;
        currentPos = transform.position;
        playerInput = GetComponent<PlayerInput>();
        playerSprite = GetComponent<SpriteRenderer>();
        //  photonView.RPC("ChangeColorRPC", RpcTarget.OthersBuffered, color);
        photonView.RPC("SendPlayerNameRPC", RpcTarget.AllBuffered);
    }

    void Update()
    {

        if (pv.IsMine && GameManagerPun.isRedy)
        {
            // อ่านค่า Input จาก Joystick
            if (canMove)
            {
                moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
                // Debug.Log(moveInput);
            }
            if (playerInput.actions["Dash"].IsPressed() && !isDash)
            {
                StartCoroutine(waitChangeSpeed());
                Debug.Log("Dash!!");
            }

            //Debug.Log(playerInput.currentControlScheme);

        }
        else
        {
            playerInput.enabled = false;
        }


    }

    void FixedUpdate()
    {
        //if (SceneManager.GetActiveScene().name != "Solo")
        //{
        if (pv.IsMine && GameManagerPun.isRedy)
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
                // Debug.Log("No movement input detected.");
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

    [PunRPC]
    void SendPlayerNameRPC()
    {
        nameText.text = pv.Owner.NickName; ;
        // this.target.photonView.Owner.NickName;
        Debug.Log("Player name: " + nameText.text);
    }

}

