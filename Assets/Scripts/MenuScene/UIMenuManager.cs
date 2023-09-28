using MenuScene.Menus;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

namespace MenuScene
{
    public class UIMenuManager : MonoBehaviour
    {
        [HideInInspector][SerializeField] private UnityEvent<string> _createRoomPressedEvent;
        [HideInInspector][SerializeField] private UnityEvent _leaveRoomPressedEvent;
        [SerializeField] private LoadingMenu _loadingMenu;
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private CreateRoomMenu _createRoomMenu;
        [SerializeField] private RoomMenu _roomMenu;
        [SerializeField] private ErrorMenu _errorMenu;
        [SerializeField] private FindRoomMenu _findRoomMenu;

        public UnityEvent<string> CreateRoomPressedEvent => _createRoomPressedEvent;
        public UnityEvent LeaveRoomPressedEvent => _leaveRoomPressedEvent;

        private Menu _curMenu;
        
        private void Awake()
        {
            _curMenu = _loadingMenu;

            _createRoomMenu.CreateRoomEvent.AddListener(OnCreateRoomPressed);
            _roomMenu.LeaveRoomEvent.AddListener(OnLeaveRoomPressed);
            _findRoomMenu.GoBackEvent.AddListener(OnBackToMenuPressed);
            _findRoomMenu.RoomJoining.AddListener(OnRoomJoining);
        }

        private void OnCreateRoomPressed(string arg)
        {
            _createRoomPressedEvent.Invoke(arg); 
        }
        
        private void OnLeaveRoomPressed()
        {
            _leaveRoomPressedEvent.Invoke(); 
        }
        
        private void OnBackToMenuPressed()
        {
            ShowMainMenu();
        }
        
        private void OnRoomJoining()
        {
            ShowLoadingMenu();
        }

        private void OpenMenu(Menu newMenu)
        {
            _curMenu.Hide();
            newMenu.Show();
            _curMenu = newMenu;
        }

        public void ShowLoadingMenu()
        {
            OpenMenu(_loadingMenu);
        }
        public void ShowMainMenu()
        {
            OpenMenu(_mainMenu);
        }

        public void ShowCreateRoomMenu()
        {
            OpenMenu(_createRoomMenu);
        }
        
        public void ShowRoomMenu(string roomName, Player[] playersList)
        {
            OpenMenu(_roomMenu);
            _roomMenu.SetUpRoom(roomName, playersList);
        }
        
        public void ShowErrorMenu(string message)
        {
            _errorMenu.SetErrorText(message);
            OpenMenu(_errorMenu);
        }
        
        public void ShowFindRoomMenu()
        {
            OpenMenu(_findRoomMenu);
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            _createRoomMenu.CreateRoomEvent.RemoveListener(OnCreateRoomPressed);
            _roomMenu.LeaveRoomEvent.RemoveListener(OnLeaveRoomPressed);
            _findRoomMenu.GoBackEvent.RemoveListener(OnBackToMenuPressed);
        }
    }
}
