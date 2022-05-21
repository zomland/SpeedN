using System;
using System.Collections;
using System.Collections.Generic;
using Base.MessageSystem;
using UnityEngine;
using Global;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Runtime.UIHandler
{
    public class UIButton_SceneTransfer : MonoBehaviour
    {
        [SerializeField] private Scenes toScene;
        [SerializeField] private Scenes fromScene;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonClick);
        }

        public void ButtonClick()
        {
            if (toScene == fromScene) return;
            Messenger.RaiseMessage(Message.LoadScene, toScene, fromScene);
        }
    }
}

