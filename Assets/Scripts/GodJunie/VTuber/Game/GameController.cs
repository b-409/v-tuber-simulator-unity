/* 
 * 작성자 : 양준규
 * 최종 수정일 : 2022-02-14
 * 내용 : 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

using Random = UnityEngine.Random;

namespace GodJunie.VTuber.Game {
    using Data;

    public class GameController : MonoBehaviour {
        #region Serialized Members
        [TabGroup("group", "Settings")]
        [LabelText("설정 인스턴스")]
        [SerializeField]
        private ChatSettings settings;

        [BoxGroup("group/Settings/호감도 게이지")]
        [LabelText("초기 값")]
        [SerializeField]
        private float gaugeStart;
        [BoxGroup("group/Settings/호감도 게이지")]
        [LabelText("최대 값")]
        [SerializeField]
        private float gaugeMax;
        [BoxGroup("group/Settings/호감도 게이지")]
        [LabelText("게이지 감소량 (초당)")]
        [SerializeField]
        private float gaugePerSecond;

        [TabGroup("group", "Settings")]
        [LabelText("채팅 개수")]
        [SerializeField]
        private int chatCount;

        [TabGroup("group", "Settings")]
        [LabelText("슈퍼 채팅 터치 횟수")]
        [SerializeField]
        private int superChatTouchCount;

        // Obejcts
        // Game Start Menu
        [TabGroup("group", "Objects")]
        [TitleGroup("group/Objects/게임 시작")]
        [SerializeField]
        [LabelText("게임 시작 패널")]
        private GameObject panelGameStart;

        // Objects (about game)
        [TitleGroup("group/Objects/게임")]
        [SerializeField]
        [LabelText("채팅 컨테이너")]
        private Transform chatContainer;
        [TitleGroup("group/Objects/게임")]
        [SerializeField]
        [LabelText("채팅 프리팹")]
        private GameObject chatPrefab;

        [TitleGroup("group/Objects/UI")]
        [SerializeField]
        [LabelText("구독자 수 텍스트")]
        private Text textSubscribers;
        [TitleGroup("group/Objects/UI")]
        [SerializeField]
        [LabelText("코인 텍스트")]
        private Text textGold;
        [TitleGroup("group/Objects/UI")]
        [SerializeField]
        [LabelText("게이지 이미지")]
        private Image imageGauge;
        #endregion

        #region Private Members
        // 채팅 타이머
        private float generateTimer;
        // 구독자 수
        private int subscribers;
        // 획득 골드
        private int gold;
        // 호감도 게이지
        private float gauge;

        private bool isPlaying;

        private List<ChatController> chats = new List<ChatController>();
        private Dictionary<ChatType, ChatProperties> chatProperties;

        private int touchCount;
        #endregion

        #region MonoBehaviour
        private void Awake() {
            chatProperties = settings.ChatProperties;
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if(isPlaying) {
                UpdateGauge(-gaugePerSecond * Time.deltaTime);
                KeyboardInput();
            }
        }
        #endregion

        private void KeyboardInput() {
            if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                OnTouchBlack();
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)) {
                OnTouchChat();
            }
        }

        private void SetUI() {
            this.textSubscribers.text = string.Format("구독자 수\n{0}", this.subscribers);
            this.textGold.text = string.Format("획득 코인\n{0}개", this.gold);
        }

        public void GameStart() {
            isPlaying = true;
            this.gauge = gaugeStart;
            while(chats.Count < chatCount) {
                GenerateChat();
            }
            panelGameStart.SetActive(false);
        }

        private void OnGameEnd() {
            isPlaying = false;
        }

        private void UpdateGauge(float gauge) {
            this.gauge = Mathf.Clamp(this.gauge + gauge, 0, gaugeMax);
            imageGauge.fillAmount = this.gauge / this.gaugeMax;
            if(this.gauge <= 0f) {
                OnGameEnd();
            }
        }

        private void GenerateChat() {
            var rand = Random.Range(0, chatProperties.Sum(e => e.Value.Probs));
            ChatType type = ChatType.None;
            foreach(var pair in chatProperties) {
                if(rand < pair.Value.Probs) {
                    type = pair.Key;
                    break;
                } else {
                    rand -= pair.Value.Probs;
                }
            }

            if(type == ChatType.None) {
                Debug.LogError("타입 지정 실패");
                return;
            }

            var properties = chatProperties[type];

            ChatController chat;
            if(chats.Count < chatCount) {
                chat = Instantiate(chatPrefab, chatContainer).GetComponent<ChatController>();
            } else {
                chat = chats[0];
                chats.RemoveAt(0);
            }

            chats.Add(chat);
            chat.Init(type, properties.BackgroundColor, "");
            chat.transform.SetSiblingIndex(0);
        }


        private void OnSuccess(ChatController chat) {
            subscribers += chatProperties[chat.ChatType].SubscribersSuccess;
            gold += chatProperties[chat.ChatType].GoldSuccess;
            gauge += chatProperties[chat.ChatType].GaugeSuccess;

            SetUI();
            GenerateChat();
        }

        private void OnFail(ChatController chat) {
            subscribers += chatProperties[chat.ChatType].SubscribersFailed;
            gold += chatProperties[chat.ChatType].GoldFailed;
            gauge += chatProperties[chat.ChatType].GaugeFailed;

            SetUI();
            GenerateChat();
        }


        public void OnTouchChat() {
            var chat = chats[0];

            switch(chat.ChatType) {
            case ChatType.Normal:
            case ChatType.Coin:
                // Success
                OnSuccess(chat);
                break;
            case ChatType.Super:
                touchCount++;
                if(touchCount == superChatTouchCount) {
                    // Success
                    touchCount = 0;
                    OnSuccess(chat);
                }
                break;
            case ChatType.Black:
                // Failed
                OnFail(chat);
                break;
            }
        }

        public void OnTouchBlack() {
            var chat = chats[0];

            switch(chat.ChatType) {
            case ChatType.Normal:
            case ChatType.Coin:
            case ChatType.Super:
                // Success
                OnFail(chat);
                break;
            case ChatType.Black:
                // Failed
                OnSuccess(chat);
                break;
            }
        }
    }
}