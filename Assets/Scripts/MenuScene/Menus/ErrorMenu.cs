using TMPro;
using UnityEngine;

namespace MenuScene.Menus
{
    public class ErrorMenu : Menu
    {
        [SerializeField] private TMP_Text _errorField;
        
        public void SetErrorText(string message)
        {
            _errorField.text = message;
        }
    }
}