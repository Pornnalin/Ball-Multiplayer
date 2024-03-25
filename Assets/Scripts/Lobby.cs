using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject createJoin;
    public GameObject namePanel;
    [Header("UI")]
    public TextMeshProUGUI playerName;
    public TMP_InputField playerNameInput;
    public TextMeshProUGUI codeRoom;
    public TMP_InputField joinNameInput;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void OnConnectedToMaster()
    {
        OpenUi();
        Debug.Log("OnConnectedToMaster() was called by PUN");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat(" OnDisconnected() was called by PUN with reason {0}", cause);
    }
    public void OnLoginButtonClicked()
    {
        string _playerName = playerNameInput.text;

        if (!_playerName.Equals(""))
        {
            PhotonNetwork.LocalPlayer.NickName = _playerName;
            playerName.text = _playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }
    private void OpenUi()
    {
        createJoin.SetActive(true);
        namePanel.SetActive(false);

    }
    public void OnCreateRoomButtonClicked()
    {
        string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        // string roomName = createNameInput.text;
        int randA = Random.Range(0, Alphabet.Length);
        int randZ = Random.Range(0, Alphabet.Length);

        string roomName = Alphabet[randA] + Random.Range(0, 100).ToString() + Alphabet[randZ];
        codeRoom.text = roomName;

        //byte maxPlayers;
        //byte.TryParse(MaxPlayersInputField.text, out maxPlayers);
        //maxPlayers = /*(byte)Mathf.Clamp(maxPlayers, 2, 8)*/2;

        RoomOptions options = new RoomOptions { MaxPlayers = 2, PlayerTtl = 10000 };

        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    //for use code
    public void OnJoinRoomButtonClicked()
    {
        // SetActivePanel(JoinRandomRoomPanel.name);

        PhotonNetwork.JoinRoom(joinNameInput.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LoadSceen()
    {
        SceneManager.LoadScene(2);
    }
}
