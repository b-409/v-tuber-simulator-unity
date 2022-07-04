using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GodJunie.VTuber.Game {
    public class ChatBlock : MonoBehaviour {
        [SerializeField]
        private TMP_Text chatText;
        [SerializeField]
        private Image imageIcon;
        [SerializeField]
        private Image imageBg;

        [SerializeField]
        private Sprite bgNormal;
        [SerializeField]
        private Sprite bgCoin;
        [SerializeField]
        private Sprite bgSuper;
        [SerializeField]
        private Sprite bgBad;

        [SerializeField]
        private Sprite iconNormal;
        [SerializeField]
        private Sprite iconCoin;
        [SerializeField]
        private Sprite iconSuper;
        [SerializeField]
        private Sprite iconBad;

        [SerializeField]
        private RectTransform superChatGauge;
       
        private ChatType chatType;
        private int touchCount;


        public ChatType ChatType => chatType;
        public int TouchCount => touchCount;

        [Button]
        public void Init(string text, ChatType chatType) {
            this.gameObject.SetActive(false);

            chatText.text = text;

            this.chatType = chatType;

            switch(chatType) {
            case ChatType.None:
                break;
            case ChatType.Normal:
                this.imageBg.sprite = bgNormal;
                this.imageIcon.sprite = iconNormal;
                break;
            case ChatType.Coin:
                this.imageBg.sprite = bgCoin;
                this.imageIcon.sprite = iconCoin;
                break;
            case ChatType.Super:
                this.imageBg.sprite = bgSuper;
                this.imageIcon.sprite = iconSuper;
                break;
            case ChatType.Bad:
                this.imageBg.sprite = bgBad;
                this.imageIcon.sprite = iconBad;
                break;
            default:
                break;
            }

            this.superChatGauge.gameObject.SetActive(chatType == ChatType.Super);
            this.superChatGauge.anchorMin = new Vector2(0f, 0f);
            this.superChatGauge.sizeDelta = Vector2.zero;
            this.touchCount = 0;

            this.transform.SetSiblingIndex(0);
            this.gameObject.SetActive(true);
        }

        public void ReadSuperChat() {
            this.touchCount++;
            this.superChatGauge.anchorMin = new Vector2(0.1f * touchCount, 0f);
            this.superChatGauge.sizeDelta = Vector2.zero;
        }
    }
}