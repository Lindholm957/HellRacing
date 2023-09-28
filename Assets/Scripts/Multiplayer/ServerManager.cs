using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

namespace Multiplayer
{
    public class ServerManager : MonoBehaviourPunCallbacks
    {
        [HideInInspector][SerializeField] private UnityEvent _loadingEvent;
        [HideInInspector][SerializeField] private UnityEvent _createRoomEvent;
        [HideInInspector][SerializeField] private UnityEvent<string, Player[]> _joinedRoomEvent;
        [HideInInspector][SerializeField] private UnityEvent<string> _errorCreateRoomEvent;
        [HideInInspector][SerializeField] private UnityEvent _leavingRoomEvent;
        [HideInInspector][SerializeField] private UnityEvent _leftRoomEvent;

        public UnityEvent LoadingEvent => _loadingEvent;
        public UnityEvent CreateRoomEvent => _createRoomEvent;
        public UnityEvent<string, Player[]> JoinedRoomEvent => _joinedRoomEvent;
        public UnityEvent<string> ErrorCreateRoomEvent => _errorCreateRoomEvent;
        public UnityEvent LeavingRoomEvent => _leavingRoomEvent;
        public UnityEvent LeftRoomEvent => _leftRoomEvent;

        private void Start()
        {
            Debug.Log("Connecting to master server");
            PhotonNetwork.ConnectUsingSettings();
        }
        
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master server");
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        
        public override void OnJoinedLobby()
        {
            Debug.Log("Joined server!");
            _loadingEvent.Invoke();
            PhotonNetwork.NickName = "Player " + UnityEngine.Random.Range(0, 1000).ToString("0000");
        }

        public void CreateRoom(string roomName)
        {
            if (string.IsNullOrEmpty(roomName))
                return;
            
            PhotonNetwork.CreateRoom(roomName);

            CreateRoomEvent.Invoke();
        }
        
        public override void OnJoinedRoom()
        { 
            _joinedRoomEvent.Invoke(PhotonNetwork.CurrentRoom.Name, PhotonNetwork.PlayerList);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            string errorText = "Error: " + message;
            ErrorCreateRoomEvent.Invoke(errorText);
            base.OnCreateRoomFailed(returnCode, message);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            _leavingRoomEvent.Invoke();
        }

        public override void OnLeftRoom()
        {
            _leftRoomEvent.Invoke();
        }
    }
}
