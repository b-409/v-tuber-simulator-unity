using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodJunie.VTuber.Data {
    using Game;

    [System.Serializable]
    public class ChatProperties {
        [BoxGroup("硅版祸")]
        [SerializeField]
        [HideLabel]
        [ColorUsage(true)]
        private Color backgroundColor;

        [HorizontalGroup("group")]
        [BoxGroup("group/加己")]
        [HorizontalGroup("group/加己/group")]
        [BoxGroup("group/加己/group/犬伏 (吝樊)")]
        [HideLabel]
        [SerializeField]
        [PropertyRange(0, 10000)]
        private float probs;
        [BoxGroup("group/加己/group/裙垫 内牢")]
        [HideLabel]
        [SerializeField]
        private int gold;
        [BoxGroup("group/加己/group/裙垫 备刀磊")]
        [HideLabel]
        [SerializeField]
        private int subscribers;
        [BoxGroup("group/加己/group/裙垫 霸捞瘤")]
        [HideLabel]
        [SerializeField]
        private float gauge;
        [BoxGroup("group/加己/group/磐摹 鸥涝")]
        [HideLabel]
        [SerializeField]
        private ChatTouchType touchType;

        public Color BackgroundColor { get => backgroundColor; }
        public float Probs { get => probs; }
        public int Gold { get => gold; }
        public int Subscribers { get => subscribers; }
        public float Gauge { get => gauge; }
        public ChatTouchType TouchType { get => touchType; }
    }

}