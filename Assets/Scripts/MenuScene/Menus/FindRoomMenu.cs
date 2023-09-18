using System;
using System.Collections.Generic;
using Photon.Realtime;
using Server;
using UnityEngine;
using UnityEngine.Events;

namespace MenuScene.Menus
{
    [RequireComponent(typeof(RoomListUpdater))]
    public class FindRoomMenu : Menu
    {
        [HideInInspector][SerializeField] private UnityEvent _goBackEvent;
        [HideInInspector][SerializeField] private UnityEvent _roomJoining;
        [SerializeField] private Transform _roomListTable;
        [SerializeField] private GameObject _roomPrefab;
        [SerializeField] private List<RoomListItem> _roomListItems = new List<RoomListItem>();

        private RoomListUpdater _roomListUpdater;
        public UnityEvent GoBackEvent => _goBackEvent;
        public UnityEvent RoomJoining => _roomJoining;

        private void Awake()
        {
            _roomListUpdater = GetComponent<RoomListUpdater>();
            _roomListUpdater.RoomsUpdatedEvent.AddListener(OnRoomsUpdated);
        }

        private void OnRoomsUpdated(List<RoomInfo> arg)
        {
            UpdateRooms(arg);
        }

        public void GoBack()
        {
            _goBackEvent.Invoke();
        }

        private void UpdateRooms(List<RoomInfo> roomList)
        {
            for (int i = 0; i < _roomListTable.childCount; i++)
            {
                RoomListItem room = _roomListTable.GetChild(i).GetComponent<RoomListItem>();
                room.RoomJoiningEvent.RemoveListener(OnRoomJoining);
                Destroy(_roomListTable.GetChild(i).gameObject);
            }
            
            _roomListItems.RemoveRange(0, _roomListItems.Count);
            
            for (int i = 0; i < roomList.Count; i++)
            {
                RoomListItem room = Instantiate(_roomPrefab, _roomListTable).GetComponent<RoomListItem>();
                room.SetUp(roomList[i]);
                _roomListItems.Add(room);
                room.RoomJoiningEvent.AddListener(OnRoomJoining);
            }
        }

        private void OnRoomJoining()
        {
            _roomJoining.Invoke();
        }
    }
}