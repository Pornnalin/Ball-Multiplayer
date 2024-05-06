using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class GameManagerPun : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerPrefab;
    public static bool isRedy = true;
    public TextMeshProUGUI code;
    public TextMeshProUGUI status;
  //  public PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {

        if (PhotonNetwork.InRoom)
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate(this.playerPrefab.name, Vector3.zero, Quaternion.identity, 0);
        }
        else
        {

            Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //isRedy = PhotonNetwork.CurrentRoom.PlayerCount > 1;
        code.text = PhotonNetwork.CurrentRoom.Name;
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            status.gameObject.SetActive(true);
            status.text = "WaitPlayer.....";
        }
        else
        {
            status.gameObject.SetActive(false);
        }
        
    }
    public override void OnJoinedRoom()
    {
        // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
        PhotonNetwork.Instantiate(this.playerPrefab.name, Vector3.zero, Quaternion.identity, 0);

    }
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.Log("OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

            //LoadArena();
        }
    }
    //void LoadArena()
    //{
    //    if (!PhotonNetwork.IsMasterClient)
    //    {
    //        Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
    //        return;
    //    }

    //    Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);

    //    PhotonNetwork.LoadLevel("PunBasics-Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
    //}
}
