/* 
 * 작성자 : 양준규
 * 최종 수정일 : 2022-02-16
 * 내용 : 인게임에서 등장하는 채팅의 종류 / 배경색 / 확률 등을 관리
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace GodJunie.VTuber.Data {
    public class ChatSettings : DataConfig<ChatSettings> {
        [System.Serializable]
        public class ChatProperties {
            [BoxGroup("배경색")]
            [SerializeField]
            [HideLabel]
            [ColorUsage(true)]
            private Color backgroundColor;

            [HorizontalGroup("group")]
            [BoxGroup("group/속성")]
            [HorizontalGroup("group/속성/group")]
            [BoxGroup("group/속성/group/확률 (중량)")]
            [HideLabel]
            [SerializeField]
            [PropertyRange(0, 10000)]
            private float probs;
            [BoxGroup("group/속성/group/획득 코인")]
            [HideLabel]
            [SerializeField]
            private int gold;
            [BoxGroup("group/속성/group/획득 구독자")]
            [HideLabel]
            [SerializeField]
            private int subscribers;

            public Color BackgroundColor { get => backgroundColor; }
            public float Probs { get => probs; }
            public int Gold { get => gold; }
            public int Subscribers { get => subscribers; }

        }

        [System.Serializable]
        public class ProbsDrawer {
            public ProbsDrawer(List<ChatProperties> list, ChatProperties chat) {
                this.list = list;
                this.chat = chat;
                this.probs = this.chat.Probs;
            }

            private List<ChatProperties> list;
            private ChatProperties chat;

            [ProgressBar(0, "Max", Height = 20 , ColorGetter = "GetColor", CustomValueStringGetter = "@GetLabel()")]
            [HideLabel]
            [ShowInInspector]
            public float probs { get; private set; }

            private Color GetColor() {
                return this.chat.BackgroundColor;
            }

            private float Max {
                get {
                    return this.list.Sum(chat => chat.Probs);
                }
            }

            private string GetLabel() {
                return string.Format("{0}/{1} ({2:0.0}%)", this.probs, Max, this.probs * 100 / Max);
            }
        }

        [LabelText("채팅 프로퍼티 리스트")]
        [SerializeField]
        [ListDrawerSettings(Expanded = true, AddCopiesLastElement = true, DraggableItems = true)]
        private List<ChatProperties> chatPropertiesList;

        public ChatProperties GetRandomChat() {
            var rand = Random.Range(0, chatPropertiesList.Sum(chat => chat.Probs));
            foreach(var chat in this.chatPropertiesList) {
                if(rand < chat.Probs) {
                    return chat;
                } else {
                    rand -= chat.Probs;
                }
            }
            return null;
        }

        [ShowInInspector]
        [ListDrawerSettings(Expanded = true)]
        [LabelText("확률 한 눈에 보기")]
        public List<ProbsDrawer> probs {
            get {
                return this.chatPropertiesList.Select(chat => new ProbsDrawer(this.chatPropertiesList, chat)).ToList();
            }
        }
    }
}