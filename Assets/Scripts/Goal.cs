using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviourPun
{
    public GameObject targert;
    public List<BoxCollider2D> boxCollider2Ds;
    private Bounds bounds;
    private int index;
    public PhotonView view;
    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log(bounds.extents.x);
        //Debug.Log(bounds.extents.y);
        //Debug.Log(bounds.extents.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Ball.isGoal)
        {
            Ball.isGoal = false;
            if (!GameManager.instance.isSolo)
            {
                view.RPC("RandomPosition", RpcTarget.AllBuffered);
            }
            else
            {
                RandomPosition();

            }
        }

    }
    [PunRPC]
    void RandomPosition()
    {
        targert.transform.rotation = Quaternion.identity;

        var index = Random.Range(0, boxCollider2Ds.Count);
        bounds = boxCollider2Ds[index].bounds;
       // Debug.Log(index);
        // Bounds bounds = GetComponent<Collider2D>().bounds;
        float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
        float offsetY = Random.Range(-bounds.extents.y, bounds.extents.y);
        // float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);

        if (index == 2 || index == 3) 
        {
            targert.transform.Rotate(0, 0, 90f);
        }
        else
        {
            targert.transform.rotation = Quaternion.identity;
        }

        // Vector2 newPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        targert.transform.position = bounds.center + new Vector3(offsetX, offsetY, 0);
    }

}
