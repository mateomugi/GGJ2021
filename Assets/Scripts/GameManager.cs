using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance { get; private set; }

    private List<PlayerController> _jogadores;

    public List<PlayerController> Jogadores
    {
        get => _jogadores;
        private set => _jogadores = value;
    }

    [SerializeField] private string _prefabLocation;

    [SerializeField] private Transform[] _spawn;
    private int _jogadoresEmJogo = 0;
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
        photonView.RPC("AdicionaJogador",RpcTarget.AllBuffered);
        Jogadores = new List<PlayerController>();
    }
    
    [PunRPC]
    private void AdicionaJogador()
    {
        _jogadoresEmJogo++;
        if (_jogadoresEmJogo == PhotonNetwork.PlayerList.Length)
        {
            CriaJogador();
        }
    }

    private void CriaJogador()
    {
        var jogadorObj = PhotonNetwork.Instantiate(_prefabLocation, _spawn[
            NetworkManager.Instance.DonoDaSala()?0:1].position,
            Quaternion.identity);
        var jogador = jogadorObj.GetComponent<PlayerController>();
        
        jogador.photonView.RPC("Inicializa",RpcTarget.All,PhotonNetwork.LocalPlayer);
    }
}
