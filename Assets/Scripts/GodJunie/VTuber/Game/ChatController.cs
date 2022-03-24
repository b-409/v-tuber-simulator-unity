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
using UnityEngine.EventSystems;

namespace GodJunie.VTuber.Game {
    using Data;

    public class ChatController : MonoBehaviour {
        [TitleGroup("Objects")]
        [SerializeField]
        [LabelText("채팅 텍스트")]
        private Text textChat;
        [TitleGroup("Objects")]
        [SerializeField]
        [LabelText("채팅 배경")]
        private Image imageBackground;


        public ChatType ChatType { get; private set; }

        private void Awake() {
           
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void Init(ChatType type, Color backgroundColor, string text) {
            this.ChatType = type;
            this.imageBackground.color = backgroundColor;
            this.textChat.text = text;

            this.gameObject.SetActive(true);
        }
    }
}