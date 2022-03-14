using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodJunie.VTuber.Data {
    using Game;

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
        [BoxGroup("group/속성/group/터치 타입")]
        [HideLabel]
        [SerializeField]
        private ChatTouchType touchType;

        public Color BackgroundColor { get => backgroundColor; }
        public float Probs { get => probs; }
        public int Gold { get => gold; }
        public int Subscribers { get => subscribers; }
        public ChatTouchType TouchType { get => touchType; }
    }

}