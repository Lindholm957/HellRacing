using Photon.Pun;
using UnityEngine;

namespace Server
{
    public class ServerLauncher : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            Debug.Log("Connecting to master server");
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master server");
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Joined server!");
        }
    }
}
