using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MenuScene.Menus
{
    public class RoomMenu : Menu
    {
        [HideInInspector][SerializeField] private UnityEvent _leaveRoomEvent;
        [SerializeField] private TMP_Text _roomNameField;

        public UnityEvent LeaveRoomEvent => _leaveRoomEvent;
        
        private string _roomName;

        public void SetRoomName(string name)
        {
            _roomName = name;
            _roomNameField.text = _roomName;
        }

        public void LeaveRoom()
        {
            _leaveRoomEvent.Invoke();
        }
    }
}