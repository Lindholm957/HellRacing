using System;
using System.Collections.Generic;
using MenuScene.Room;
using Photon.Pun;
using Photon.Realtime;
using Server;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MenuScene.Menus
{
    [RequireComponent(typeof(PlayerListUpdater))]
    public class RoomMenu : Menu
    {
        [SerializeField] private GameObject _startButton;
        [HideInInspector][SerializeField] private UnityEvent _leaveRoomEvent;
        [SerializeField] private TMP_Text _roomNameField;
        [SerializeField] private Transform _playerListTable;
        [SerializeField] private GameObject _playerNamePrefab;

        public UnityEvent LeaveRoomEvent => _leaveRoomEvent;
        
        private string _roomName;
        private Player[] _playersList;
        private List<PlayerListItem> _playersListItems;
        private PlayerListUpdater _playerListUpdater;

        private void Awake()
        {
            _playerListUpdater = GetComponent<PlayerListUpdater>();
            _playerListUpdater.PlayerEnteredEvent.AddListener(OnPlayerEnteredRoom);
            _playerListUpdater.PlayerLeftEvent.AddListener(OnPlayerLeft);
            _playerListUpdater.MasterSwitchedEvent.AddListener(OnMasterClientSwitched);
        }

        private void OnPlayerLeft(Player arg0)
        {
            for (int i = 0; i < _playersListItems.Count; i++)
            {
                if (arg0 == _playersListItems[i].Player)
                {
                    Destroy(_playersListItems[i].gameObject);
                    _playersListItems.RemoveAt(i);
                }
            }
        }
        
        private void OnMasterClientSwitched()
        {
            _startButton.SetActive(PhotonNetwork.IsMasterClient);
        }

        public void SetUpRoom(string name, Player[] playersList)
        {
            _startButton.SetActive(PhotonNetwork.IsMasterClient);
            
            _roomName = name;
            _roomNameField.text = _roomName;
            _playersListItems = new List<PlayerListItem>();

            _playersList = playersList;
            
            for (int i = 0; i < _playerListTable.childCount; i++)
            {
                Destroy(_playerListTable.GetChild(i).gameObject);
            }
            
            _playersListItems.RemoveRange(0, _playersListItems.Count);

            foreach (var player in _playersList)
            {
                var playerListItem = Instantiate(_playerNamePrefab, _playerListTable).GetComponent<PlayerListItem>();
                playerListItem.SetUp(player);
                _playersListItems.Add(playerListItem);
            }
        }

        private void OnPlayerEnteredRoom(Player newPlayer)
        {
            var playerListItem = Instantiate(_playerNamePrefab, _playerListTable).GetComponent<PlayerListItem>();
            playerListItem.SetUp(newPlayer);
            _playersListItems.Add(playerListItem);
        }

        public void LeaveRoom()
        {
            _leaveRoomEvent.Invoke();
        }
            
        public void StartGame()
        {
            PhotonNetwork.LoadLevel(1);
        }
    }
}