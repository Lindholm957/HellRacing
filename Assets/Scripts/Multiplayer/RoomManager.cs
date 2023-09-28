using System.IO;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Multiplayer
{
    [RequireComponent(typeof(PhotonView))]
    public class RoomManager : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex == 1)
            {
                string prefabName = Path.Combine("Prefabs", "Photon", "PlayerManager");
                PhotonNetwork.Instantiate(prefabName, Vector3.zero, Quaternion.identity);
            }
        }

        public void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
