using Photon.Realtime;
using Server;
using UnityEngine;

namespace MenuScene
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private UIMenuManager _uiMenuManager;
        [SerializeField] private ServerManager _serverManager;

        private void Awake()
        {
            _serverManager.LoadingEvent.AddListener(OnJoinedLobby);
            _uiMenuManager.CreateRoomPressedEvent.AddListener(CreateRoomPressed);
            _serverManager.CreateRoomEvent.AddListener(OnStartCreatingRoom);
            _serverManager.JoinedRoomEvent.AddListener(OnJoinedRoom);
            _serverManager.ErrorCreateRoomEvent.AddListener(OnErrorToJoinRoom);
            _uiMenuManager.LeaveRoomPressedEvent.AddListener(LeaveRoomPressed);
            _serverManager.LeavingRoomEvent.AddListener(OnLeavingRoom);
            _serverManager.LeftRoomEvent.AddListener(OnLeftRoom);
        }

        private void OnJoinedLobby()
        {
            _uiMenuManager.ShowMainMenu();
        }
        
        private void CreateRoomPressed(string arg)
        {
            _serverManager.CreateRoom(arg);
        }
        
        private void OnStartCreatingRoom()
        {
            _uiMenuManager.ShowLoadingMenu();
        }
        
        private void OnJoinedRoom(string roomName, Player[] playerList)
        {
            _uiMenuManager.ShowRoomMenu(roomName, playerList);
        }
        
        private void OnErrorToJoinRoom(string arg)
        {
            _uiMenuManager.ShowErrorMenu(arg);
        }
        
        private void LeaveRoomPressed()
        {
            _serverManager.LeaveRoom();
        }
        
        private void OnLeavingRoom()
        {
            _uiMenuManager.ShowLoadingMenu();
        }
        private void OnLeftRoom()
        {
            _uiMenuManager.ShowMainMenu();
        }

        private void OnDestroy()
        {
            _serverManager.LoadingEvent.RemoveListener(OnJoinedLobby);
            _uiMenuManager.CreateRoomPressedEvent.RemoveListener(CreateRoomPressed);
            _serverManager.CreateRoomEvent.RemoveListener(OnStartCreatingRoom);
            _serverManager.JoinedRoomEvent.RemoveListener(OnJoinedRoom);
            _serverManager.ErrorCreateRoomEvent.RemoveListener(OnErrorToJoinRoom);
            _uiMenuManager.LeaveRoomPressedEvent.RemoveListener(LeaveRoomPressed);
            _serverManager.LeavingRoomEvent.RemoveListener(OnLeavingRoom);
            _serverManager.LeftRoomEvent.RemoveListener(OnLeftRoom);
        }
    }
}
