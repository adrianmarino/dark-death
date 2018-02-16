using System;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Util.Component.UI;
using Button = UnityEngine.UI.Button;

namespace Fps.UI
{
    public class NewMatchForm : MonoBehaviour
    {
        private void Start()
        {
            matchNameField.ActivateInputField();
        }

        public void OnCreate()
        {
            GameManager.Instance.StartMatch(matchName, (uint)maxPlayers.Value);
        }

        public void OnChange()
        {
            ComponentUtil.tryGet<Button>(createButton, it => it.interactable = !String.IsNullOrEmpty(matchName));
        }

        #region Input Fields

        public void SetMatchName(string value)
        {
            matchName = value;
        }

        #endregion

        #region Attributes
        
        [SerializeField] private InputField matchNameField;
        
        [SerializeField] private string matchName;

        [SerializeField] private Button createButton;

        [SerializeField] private SliderField maxPlayers;

        #endregion
        
    }
}
