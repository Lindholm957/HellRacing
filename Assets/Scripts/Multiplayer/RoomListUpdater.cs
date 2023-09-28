using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

namespace Multiplayer
{
    public class RoomListUpdater : MonoBehaviourPunCallbacks
    {
        [HideInInspector][SerializeField] private UnityEvent<List<RoomInfo>> _roomsUpdatedEvent;
        public UnityEvent<List<RoomInfo>> RoomsUpdatedEvent => _roomsUpdatedEvent;
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            _roomsUpdatedEvent.Invoke(roomList);
        }
    }
}