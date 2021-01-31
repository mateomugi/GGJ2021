using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuLobby : MonoBehaviourPunCallbacks
{
    [SerializeField]private TMP_Text _listaDeJogadores;
    [SerializeField] private Button _comecaJogo;
    [SerializeField] private Button _kickPlayer;
    [SerializeField] private Menu _menu;
    
    [PunRPC]
    public void AtualizaLista()
    {
        _listaDeJogadores.text = NetworkManager.Instance.ObterListaDeJogadores();
        if (NetworkManager.Instance.DonoDaSala())
        {
            if (NetworkManager.Instance.maxPlayers())
            {
                _comecaJogo.interactable = true;
                _kickPlayer.gameObject.SetActive(true);
                _kickPlayer.onClick.AddListener(() => { _menu.KickPlayer(NetworkManager.Instance.LastPlayer()); }); 
                return;
            }
        }
        _comecaJogo.interactable = false;
        _kickPlayer.onClick.RemoveAllListeners();
        _kickPlayer.gameObject.SetActive(false);

    }
}
