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
        public enum ChatTouchType : int {  None = 0, Touch, SwipeLeft, SwipeRight };
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
    }
}