using MenuScene.Menus;
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

        public UnityEvent<string> CreateRoomPressedEvent => _createRoomPressedEvent;
        public UnityEvent LeaveRoomPressedEvent => _leaveRoomPressedEvent;

        private Menu _curMenu;
        
        private void Awake()
        {
            _curMenu = _loadingMenu;

            _createRoomMenu.CreateRoomEvent.AddListener(OnCreateRoomPressed);
            _roomMenu.LeaveRoomEvent.AddListener(OnLeaveRoomPressed);
        }

        private void OnCreateRoomPressed(string arg)
        {
            _createRoomPressedEvent.Invoke(arg); 
        }
        
        private void OnLeaveRoomPressed()
        {
            _leaveRoomPressedEvent.Invoke(); 
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
        
        public void ShowRoomMenu(string roomName)
        {
            _roomMenu.SetRoomName(roomName);
            OpenMenu(_roomMenu);
        }
        
        public void ShowErrorMenu(string message)
        {
            _errorMenu.SetErrorText(message);
            OpenMenu(_errorMenu);
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            _createRoomMenu.CreateRoomEvent.RemoveListener(OnCreateRoomPressed);
            _roomMenu.LeaveRoomEvent.RemoveListener(OnLeaveRoomPressed);
        }
    }
}
