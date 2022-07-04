using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GodJunie.VTuber.Game {
    public class GameController : MonoBehaviour {
        [SerializeField]
        private ChatBlock chatBlockPrefab;
        [SerializeField]
        private Transform chatContainer;
        [SerializeField]
        private int poolCount;

        [SerializeField]
        private float startGauge = 50f;
        [SerializeField]
        private float deltaGaugeMin = 10f;
        [SerializeField]
        private float deltaGaugeMax = 100f;
        [SerializeField]
        private AnimationCurve deltaGaugeCurve;

        [SerializeField]
        private float successGauge = 20f;
        [SerializeField]
        private int coinChatGold = 1;
        [SerializeField]
        private float superChatGauge = 10f;

        [SerializeField]
        private float badChatRate = 10f;
        [SerializeField]
        private float superChatRate = 10f;
        [SerializeField]
        private float coinChatRate = 30f;
        [SerializeField]
        private float normalChatRate = 50f;

        [SerializeField]
        private Image imageGaugeFill;
        [SerializeField]
        private TMP_Text textScore;
        [SerializeField]
        private TMP_Text textGold;


        private bool gameStart;
        private bool isPlaying;
        private int gold;
        private int score;
        private float gauge;


        private Queue<ChatBlock> chatBlockPool;
        private ChatBlock targetChat;


        // Start is called before the first frame update
        void Start() {
            InitChat();
            this.gauge = startGauge;
            this.imageGaugeFill.fillAmount = this.gauge / 100f;
            gameStart = false;
            isPlaying = false;
        }

        // Update is called once per frame
        void Update() {
            if(isPlaying) {
                this.gauge -= Time.deltaTime * this.deltaGaugeCurve.Evaluate(this.score);

                this.imageGaugeFill.fillAmount = this.gauge / 100f;

                if(this.gauge <= 0f) {
                    this.isPlaying = false;
                }
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.BlackChat();
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)) {
                this.ReadChat();
            }
        }

        private void InitChat() {
            chatBlockPool = new Queue<ChatBlock>();

            for(int i = 0; i < poolCount; i++) {
                var chatBlock = Instantiate(chatBlockPrefab, chatContainer);
                chatBlock.Init("", GetRandChatType());
                this.chatBlockPool.Enqueue(chatBlock);
            }
        }

        public void ReadChat() {
            if(!this.gameStart) {
                this.gameStart = true;
                this.isPlaying = true;
            }

            if(!this.isPlaying)
                return;

            if(this.targetChat == null) {
                this.targetChat = chatBlockPool.Dequeue();
            }

            switch(this.targetChat.ChatType) {
            case ChatType.Normal:
                this.GetGauge(successGauge);
                GetScore(1);
                RestoreTargetChat();
                break;
            case ChatType.Coin:
                this.GetGauge(successGauge);
                GetScore(1);
                GetGold(1);
                RestoreTargetChat();
                break;
            case ChatType.Super:
                this.targetChat.ReadSuperChat();
                if(this.targetChat.TouchCount == 10) {
                    this.GetGauge(successGauge);
                    GetScore(10);
                    GetGold(10);
                    RestoreTargetChat();
                } else {
                    this.GetGauge(this.deltaGaugeCurve.Evaluate(this.score) * 0.1f);
                }
                break;
            case ChatType.Bad:
                this.GetGauge(-100f);
                RestoreTargetChat();
                break;
            default:
                throw new System.Exception("Undefined Chat Type");
            }
        }

        public void BlackChat() {
            if(!this.gameStart) {
                this.gameStart = true;
                this.isPlaying = true;
            }

            if(!this.isPlaying)
                return;

            if(this.targetChat == null) {
                this.targetChat = chatBlockPool.Dequeue();
            }

            switch(this.targetChat.ChatType) {
            case ChatType.Normal:
            case ChatType.Coin:
            case ChatType.Super:
                this.GetGauge(-50f);
                break;
            case ChatType.Bad:
                GetScore(1);
                this.GetGauge(successGauge);
                break;
            default:
                throw new System.Exception("Undefined Chat Type");
            }

            RestoreTargetChat();
        }

        private void GetGauge(float gauge) {
            this.gauge = Mathf.Clamp(this.gauge + gauge, 0f, 100f);
        }

        private void RestoreTargetChat() {
            this.targetChat.Init("", GetRandChatType());
            chatBlockPool.Enqueue(this.targetChat);
            this.targetChat = null;
        }

        private ChatType GetRandChatType() {
            float rand = Random.Range(0f, 100f);

            if(rand < badChatRate) return ChatType.Bad;
            rand -= badChatRate;
            if(rand < coinChatRate) return ChatType.Coin;
            rand -= coinChatRate;
            if(rand < superChatRate) return ChatType.Super;
            return ChatType.Normal;
        }

        private void GetScore(int score) {
            this.score += score;
            this.textScore.text = this.score.ToString("n0");
        }

        private void GetGold(int gold) {
            this.gold += gold;
            this.textGold.text = this.gold.ToString("n0");
        }
    }
}