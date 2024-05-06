using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePlayer : MonoBehaviour
{
    public int scoreP1;
    public int scoreP2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Player1Add()
    {
        scoreP1 += 1;
    }
    public void Player2Add()
    {
        scoreP2 += 1;
    }
}
