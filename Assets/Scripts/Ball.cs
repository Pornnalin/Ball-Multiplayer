using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviourPunCallbacks
{
    [SerializeField] SpriteRenderer ballSprite;
    [SerializeField] CircleCollider2D circleCollider2D;
    [SerializeField] Rigidbody2D rigi;
    public static bool isGoal = false;
    [SerializeField] private CameraShake cameraShake;
    public ScorePlayer scorePlayer;
    private bool isPlayerOneGetBall;

    // Start is called before the first frame update
    void Start()
    {
        cameraShake = FindAnyObjectByType<CameraShake>();
        scorePlayer = FindAnyObjectByType<ScorePlayer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Goal") && !isGoal)
        {
            StartCoroutine(waitUpdate());

            if (isPlayerOneGetBall && GameManager.instance.isSolo)
            {
                scorePlayer.Player1Add();
            }
            else if (isPlayerOneGetBall)
            {
                scorePlayer.Player1Add();
            }
            else
            {
                scorePlayer.Player2Add();

            }

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //scorePlayer.SoloAdd();

        if (collision.collider.CompareTag("P1"))
        {
            isPlayerOneGetBall = true;
            Debug.Log("Player1GetBall");
        }
        else if (collision.collider.CompareTag("P2"))
        {
            //
            isPlayerOneGetBall = false;
        }

    }
    IEnumerator waitUpdate()
    {
        isGoal = true;
        SetEnable(false, RigidbodyType2D.Static);
        StartCoroutine(cameraShake.Shake());
        yield return new WaitForSeconds(0.5f);
        this.transform.position = Vector3.zero;
        SetEnable(true, RigidbodyType2D.Dynamic);

    }
    private void SetEnable(bool set, RigidbodyType2D type2D)
    {
        ballSprite.enabled = set;
        circleCollider2D.enabled = set;
        rigi.bodyType = type2D;
    }

}
