using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MenuScene.Room
{
    public class RoomListItem : MonoBehaviour
    {
        [HideInInspector][SerializeField] private UnityEvent _roomJoiningEvent;
        [SerializeField] private TMP_Text _roomName;

        public RoomInfo _info;
        
        public UnityEvent RoomJoiningEvent => _roomJoiningEvent;
        
        public void SetUp(RoomInfo roomInfo)
        {
            _info = roomInfo;
            _roomName.text = _info.Name;
        }

        public void OnClick()
        {
            PhotonNetwork.JoinRoom(_info.Name);
            _roomJoiningEvent.Invoke();
        }
    }
}