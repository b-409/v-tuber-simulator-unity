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

    [RequireComponent(typeof(EventTrigger))]
    public class ChatController : MonoBehaviour {
        [TitleGroup("Objects")]
        [SerializeField]
        [LabelText("채팅 텍스트")]
        private Text textChat;
        [TitleGroup("Objects")]
        [SerializeField]
        [LabelText("채팅 배경")]
        private Image imageBackground;

        private Action onComplete;

        private ChatProperties properties;

        private EventTrigger trigger;
        private Vector2 startPos;
        private bool isDragging;
        private int pointerId;

        private void Awake() {
            this.trigger = GetComponent<EventTrigger>();
            var pointerDownEntry = new EventTrigger.Entry();
            pointerDownEntry.eventID = EventTriggerType.PointerDown;
            pointerDownEntry.callback.AddListener(data => OnPointerDown(data as PointerEventData));

            var dragEntry = new EventTrigger.Entry();
            dragEntry.eventID = EventTriggerType.Drag;
            dragEntry.callback.AddListener(data => OnDrag(data as PointerEventData));

            this.trigger.triggers.Add(pointerDownEntry);
            this.trigger.triggers.Add(dragEntry);
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void Init(string text, ChatProperties properties, Action onComplete) {
            this.textChat.text = text;
            this.properties = properties;
            this.imageBackground.color = properties.BackgroundColor;

            isDragging = false;

            this.onComplete = onComplete;

            this.gameObject.SetActive(true);
        }

        public void OnPointerDown(PointerEventData data) {
            switch(properties.TouchType) {
            case ChatTouchType.Touch:
                // 성공
                this.onComplete?.Invoke();
                this.gameObject.SetActive(false);
                break;
            case ChatTouchType.SwipeLeft:
            case ChatTouchType.SwipeRight:
                if(!isDragging) {
                    isDragging = true;
                    startPos = data.position;
                    pointerId = data.pointerId;
                }
                break;
            }
        }

        public void OnDrag(PointerEventData data) {
            if(!isDragging) return;

            if(data.pointerId == this.pointerId) {
                if(properties.TouchType == ChatTouchType.SwipeLeft) {
                    if((data.position - startPos).x < -100) {
                        onComplete?.Invoke();
                        this.gameObject.SetActive(false);
                    }
                }
                if(properties.TouchType == ChatTouchType.SwipeRight) {
                    if((data.position - startPos).x > 100) {
                        onComplete?.Invoke();
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}