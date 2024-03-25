using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] SpriteRenderer ballSprite;
    [SerializeField] CircleCollider2D circleCollider2D;
    [SerializeField] Rigidbody2D rigi;
    public static bool isGoal = false;
    // Start is called before the first frame update
    void Start()
    {

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
        }
    }
    IEnumerator waitUpdate()
    {
        isGoal = true;
        SetEnable(false, RigidbodyType2D.Static);
        yield return new WaitForSeconds(0.5f);
        this.transform.position = Vector3.zero;
        SetEnable(true, RigidbodyType2D.Dynamic);

    }
    private void SetEnable(bool set,RigidbodyType2D type2D)
    {
        ballSprite.enabled = set;
        circleCollider2D.enabled = set;
        rigi.bodyType = type2D;
    }

}
