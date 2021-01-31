using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuEntrada : MonoBehaviour
{
    [SerializeField]private TMP_Text _userName;
    [SerializeField]private TMP_Text _roomName;

    public void CriaSala()
    {
        NetworkManager.Instance.CriaSala(_roomName.text);
        NetworkManager.Instance.MudaNick(_userName.text);
    }
    public void EntraSala()
    {
        NetworkManager.Instance.EntraSala(_roomName.text);
        NetworkManager.Instance.MudaNick(_userName.text);
    }
}
