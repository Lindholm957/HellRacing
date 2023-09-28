using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

namespace Server
{
    public class PlayerListUpdater : MonoBehaviourPunCallbacks
    {
        [HideInInspector][SerializeField] private UnityEvent<Player> _playerLeftEvent;
        [HideInInspector][SerializeField] private UnityEvent<Player> _playerEnteredEvent;

        public UnityEvent<Player> PlayerLeftEvent => _playerLeftEvent;
        public UnityEvent<Player> PlayerEnteredEvent => _playerEnteredEvent;

        
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("OnPlayerLeftRoom");
            _playerLeftEvent.Invoke(otherPlayer);
        }
        public override void OnLeftRoom()
        {
            Debug.Log("OnLeftRoom");
            Destroy(gameObject);
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            _playerEnteredEvent.Invoke(newPlayer);
        }
    }
}