using UnityEngine;

namespace MenuScene
{
    public class Menu : MonoBehaviour
    {        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}