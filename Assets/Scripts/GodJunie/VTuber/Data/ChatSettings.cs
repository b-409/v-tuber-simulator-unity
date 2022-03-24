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
using GodJunie.VTuber.Game;

namespace GodJunie.VTuber.Data {
    public class ChatSettings : DataConfig<ChatSettings> {
        [HorizontalGroup("group", .25f)]
        [BoxGroup("group/일반 채팅")]
        [HideLabel]
        [SerializeField]
        private ChatProperties normalProperties;
        [HorizontalGroup("group", .25f)]
        [BoxGroup("group/코인 채팅")]
        [HideLabel]
        [SerializeField]
        private ChatProperties coinProperties;
        [HorizontalGroup("group", .25f)]
        [BoxGroup("group/슈퍼 채팅")]
        [HideLabel]
        [SerializeField]
        private ChatProperties superProperties;
        [HorizontalGroup("group", .25f)]
        [BoxGroup("group/블랙 채팅")]
        [HideLabel]
        [SerializeField]
        private ChatProperties blackProperties;

        public Dictionary<ChatType, ChatProperties> ChatProperties {
            get {
                return new Dictionary<ChatType, ChatProperties> {
                    { ChatType.Normal, this.normalProperties },
                    { ChatType.Coin, this.coinProperties },
                    { ChatType.Super, this.superProperties },
                    { ChatType.Black, this.blackProperties },
                };
            }
        }
    }
}