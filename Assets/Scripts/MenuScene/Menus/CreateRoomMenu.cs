using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MenuScene.Menus
{
    public class CreateRoomMenu : Menu
    {
        [HideInInspector][SerializeField] private UnityEvent<string> _createRoomEvent;
        [SerializeField] private TMP_InputField _inputField;

        public UnityEvent<string> CreateRoomEvent => _createRoomEvent;

        public void CreateRoom()
        {
            _createRoomEvent.Invoke(_inputField.text);
        }
    }
}