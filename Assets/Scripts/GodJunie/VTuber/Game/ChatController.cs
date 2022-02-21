/* 
 * 작성자 : 양준규
 * 최종 수정일 : 2022-02-14
 * 내용 : 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace GodJunie.VTuber.Game {
    public class ChatController : MonoBehaviour {
        [TitleGroup("Objects")]
        [SerializeField]
        [LabelText("채팅 텍스트")]
        private Text textChat;
        [TitleGroup("Objects")]
        [SerializeField]
        [LabelText("채팅 배경")]
        private Image imageBackground;

        private Action onClick;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void Init(string text, Data.ChatSettings.ChatProperties properties, Action onClick) {
            this.textChat.text = text;
            this.imageBackground.color = properties.BackgroundColor;
            this.onClick = onClick;
            this.gameObject.SetActive(true);
        }

        public void OnClick() {
            // 연출하고
            this.onClick?.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}