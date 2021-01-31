using System;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance!=this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        
        print("Conectado");

    }

    public void CriaSala(string roomNameText)
    {
        var rooomOptions = new RoomOptions();
        rooomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(roomNameText,rooomOptions);
    }
    public void EntraSala(string roomNameText)
    {
        PhotonNetwork.JoinRoom(roomNameText);
    }

    public void MudaNick(string nickName)
    {
        PhotonNetwork.NickName = nickName;
    }

    public string ObterListaDeJogadores()
    {
        string lista = "";
        foreach (var player in PhotonNetwork.PlayerList)
        {
            lista += player.NickName + "\n";
        }
        
        return lista;
    }

    public bool DonoDaSala()
    {
        return PhotonNetwork.IsMasterClient;
    }

    public void SairDoLobby()
    {
        PhotonNetwork.LeaveRoom();
    }
    
    [PunRPC]
    public void ComecaJogo(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
    [PunRPC]
    public void KickPlayer()
    {
        PhotonNetwork.LeaveRoom();
    }

    public bool maxPlayers()
    {
        return PhotonNetwork.PlayerList.Length == PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public Player LastPlayer()
    {
        return PhotonNetwork.PlayerList.Last();
    }
}
