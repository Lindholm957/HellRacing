using System.IO;
using Photon.Pun;
using UnityEngine;

namespace Multiplayer
{
    [RequireComponent(typeof(PhotonView))]
    public class PlayerManager : MonoBehaviour
    {
        private PhotonView _photonView;

        private void Start()
        {
            Vector3 startPos;
            if (PhotonNetwork.IsMasterClient)
            {
                startPos = GameController.Instance.StartPositions[0].GetPosition();
            }
            else
            {
                startPos = GameController.Instance.StartPositions[1].GetPosition();
            }
            CreatePlayerCar(startPos);
        }

        public void CreatePlayerCar(Vector3 startPos)
        {
            _photonView = GetComponent<PhotonView>();

            if (_photonView.IsMine)
            {
                var rotation = Quaternion.Euler(0, 180, 0);
                string prefabName = Path.Combine("Prefabs", "Cars", "SunLineGTE_Drift");
                PhotonNetwork.Instantiate(prefabName, startPos, rotation);
            }
        }
    }
}