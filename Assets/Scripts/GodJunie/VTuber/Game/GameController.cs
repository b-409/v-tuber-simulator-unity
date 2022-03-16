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
    public class GameController : MonoBehaviour {
        #region Serialized Members
        // Settings Fileds
        [TabGroup("group", "Settings")]
        [BoxGroup("group/Settings/생성 개수")]
        [HorizontalGroup("group/Settings/생성 개수/group", .5f)]
        [SerializeField]
        [LabelText("최소")]
        private int minCount;
        [HorizontalGroup("group/Settings/생성 개수/group", .5f)]
        [SerializeField]
        [LabelText("최대")]
        private int maxCount;

        [BoxGroup("group/Settings/생성 간격")]
        [HorizontalGroup("group/Settings/생성 간격/group", .5f)]
        [SerializeField]
        [LabelText("최소")]
        private float minInterval;
        [HorizontalGroup("group/Settings/생성 간격/group", .5f)]
        [SerializeField]
        [LabelText("최대")]
        private float maxInterval;

        [TabGroup("group", "Settings")]
        [SerializeField]
        [LabelText("목표 구독자 수")]
        private int goal;

        [TabGroup("group", "Settings")]
        [LabelText("설정 인스턴스")]
        [SerializeField]
        private Data.ChatSettings settings;

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

        private int poolCount = 10;
        private List<ChatController> chatsPool = new List<ChatController>();
        #endregion

        #region MonoBehaviour
        private void Awake() {
            
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if(isPlaying) {
                UpdateGauge(-gaugePerSecond * Time.deltaTime);
            }
        }

        private async void GenerateChatAsync() {
            while(isPlaying) {
                await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(minInterval, maxInterval)));
                for(int i = 0; i < Random.Range(minCount, maxCount); i++) {
                    GenerateChat();
                }
            }
        }
        #endregion

        private void SetUI() {
            this.textSubscribers.text = string.Format("구독자 수\n{0}/{1}", this.subscribers, this.goal);
            this.textGold.text = string.Format("획득 코인\n{0}개", this.gold);
        }

        public void GameStart() {
            isPlaying = true;
            this.gauge = gaugeStart;
            panelGameStart.SetActive(false);
            GenerateChatAsync();
        }

        private void OnGameEnd() {
            isPlaying = false;
        }

        private void GenerateChat() {
            var text = "";

            var chatProperties = this.settings.GetRandomChat();

            // Generate new chat
            // 1. 풀링해서 꺼낼 수 있는게 있는지 확인
            var chat = chatsPool.Where(e => !e.gameObject.activeSelf).FirstOrDefault();
            if(chat == default(ChatController)) {
                // 없었음
                chat = Instantiate(chatPrefab, chatContainer).GetComponent<ChatController>();
                chatsPool.Add(chat);
            }
            // Init 하고
            chat.Init(text, chatProperties, () => {
                this.gold += chatProperties.Gold;
                this.subscribers += chatProperties.Subscribers;
                UpdateGauge(chatProperties.Gauge);

                SetUI();

                if(this.subscribers >= this.goal) {
                    Debug.Log("스테이지 클리어!");
                }
            });
            // 0번에다가 넣음 (가장 아래로)
            chat.transform.SetSiblingIndex(0);
            if(chatsPool.Count(e => e.gameObject.activeSelf) > poolCount) {
                // 활성화된 채팅 수가 카운트를 넘어감
                // 가장 처음에 활성화된 애를 비활성화 해야함
                chatsPool.Where(e => e.gameObject.activeSelf).OrderBy(e => e.transform.GetSiblingIndex()).Last().gameObject.SetActive(false);
            }
        }

        private void UpdateGauge(float gauge) {
            this.gauge = Mathf.Clamp(this.gauge + gauge, 0, gaugeMax);
            imageGauge.fillAmount = this.gauge / this.gaugeMax;
            if(this.gauge <= 0f) {
                OnGameEnd();
            }
        }
    }
}