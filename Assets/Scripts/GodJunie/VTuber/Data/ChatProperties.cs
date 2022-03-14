using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodJunie.VTuber.Data {
    using Game;

    [System.Serializable]
    public class ChatProperties {
        [BoxGroup("����")]
        [SerializeField]
        [HideLabel]
        [ColorUsage(true)]
        private Color backgroundColor;

        [HorizontalGroup("group")]
        [BoxGroup("group/�Ӽ�")]
        [HorizontalGroup("group/�Ӽ�/group")]
        [BoxGroup("group/�Ӽ�/group/Ȯ�� (�߷�)")]
        [HideLabel]
        [SerializeField]
        [PropertyRange(0, 10000)]
        private float probs;
        [BoxGroup("group/�Ӽ�/group/ȹ�� ����")]
        [HideLabel]
        [SerializeField]
        private int gold;
        [BoxGroup("group/�Ӽ�/group/ȹ�� ������")]
        [HideLabel]
        [SerializeField]
        private int subscribers;
        [BoxGroup("group/�Ӽ�/group/��ġ Ÿ��")]
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