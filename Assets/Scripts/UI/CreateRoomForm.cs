using System;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Util.Component.UI;
using Button = UnityEngine.UI.Button;

namespace Fps.UI
{
    public class CreateRoomForm : MonoBehaviour
    {
        private void Start()
        {
            roomNameField.ActivateInputField();
        }

        public void OnCreate()
        {
            networkService.CreateRoom(roomName, roomPassword, (uint)roomSize.Value);
        }

        public void OnChange()
        {
            ComponentUtil.tryGet<Button>(createButton, it => it.interactable =  ValidInput());
        }

        private bool ValidInput()
        {
            return !String.IsNullOrEmpty(roomName) && !String.IsNullOrEmpty(roomPassword);
        }

        #region Input Fields

        public void SetRoomName(string value)
        {
            roomName = value;
        }

        public void SetRoomPassword(string value)
        {
            roomPassword = value;
        }
        
        #endregion

        #region Attributes
        
        [SerializeField] private InputField roomNameField;
        
        [SerializeField] private string roomName;

        [SerializeField] private string roomPassword;
        
        [SerializeField] private Button createButton;

        [SerializeField] private SliderField roomSize;

        [SerializeField] private NetworkService networkService;

        #endregion
    }
}
