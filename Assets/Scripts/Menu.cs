using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Menu : MonoBehaviourPunCallbacks
{
   [SerializeField] private MenuEntrada _menuEntrada;
   [SerializeField] private MenuLobby _menuLobby;

   private void Start()
   {
      _menuEntrada.gameObject.SetActive(false);
      _menuLobby.gameObject.SetActive(false);
   }

   public override void OnConnectedToMaster()
   {
      MenuEntrada();
   }

   public override void OnJoinedRoom()
   {
      MudaMenu(_menuLobby.gameObject);
      _menuLobby.photonView.RPC("AtualizaLista", RpcTarget.All);
   }

   public void MudaMenu(GameObject menu)
   {
      _menuEntrada.gameObject.SetActive(false);
      _menuLobby.gameObject.SetActive(false);

      menu.SetActive(true);
   }

   public override void OnPlayerLeftRoom(Player otherPlayer)
   {
      _menuLobby.AtualizaLista();
   }
   
   public void SairLobby()
   {
      NetworkManager.Instance.SairDoLobby();
      MudaMenu(_menuEntrada.gameObject);
   }
   
   
   public void ComeçaJogo(string sceneName)
   {
      NetworkManager.Instance.photonView.RPC("ComecaJogo",RpcTarget.All,sceneName);
   }

   public void KickPlayer(Player player)
   {
      NetworkManager.Instance.photonView.RPC("KickPlayer",player);     
   }

   public void MenuEntrada()
   {
      MudaMenu(_menuEntrada.gameObject);
   }
}
