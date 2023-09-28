using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace MenuScene.Room
{
    public class PlayerListItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerNameField;

        public Player Player;
        
        public void SetUp(Player newPlayer)
        {
            Player = newPlayer;
            _playerNameField.text = Player.NickName;
        }
    }
}