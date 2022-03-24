using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodJunie.VTuber.Data {
    using Game;

    [System.Serializable]
    public class ChatProperties {
        [VerticalGroup("group")]
        [BoxGroup("group/배경색")]
        [SerializeField]
        [HideLabel]
        [ColorUsage(true)]
        private Color backgroundColor;

        [BoxGroup("group/확률 (중량)")]
        [HideLabel]
        [SerializeField]
        [PropertyRange(0, 10000)]
        private float probs;

        [HorizontalGroup("group/group", .5f)]
        [BoxGroup("group/group/성공 시")]
        [BoxGroup("group/group/성공 시/획득 골드")]
        [HideLabel]
        [SerializeField]
        private int goldSuccess;
        [BoxGroup("group/group/성공 시/획득 구독자")]
        [HideLabel]
        [SerializeField]
        private int subscribersSuccess;
        [BoxGroup("group/group/성공 시/획득 게이지")]
        [HideLabel]
        [SerializeField]
        private float gaugeSuccess;

        [HorizontalGroup("group/group", .5f)]
        [BoxGroup("group/group/실패 시")]
        [BoxGroup("group/group/실패 시/차감 골드")]
        [HideLabel]
        [SerializeField]
        private int goldFailed;
        [BoxGroup("group/group/실패 시/차감 구독자")]
        [HideLabel]
        [SerializeField]
        private int subscribersFailed;
        [BoxGroup("group/group/실패 시/차감 게이지")]
        [HideLabel]
        [SerializeField]
        private float gaugeFailed;


        public Color BackgroundColor { get => backgroundColor; }
        public float Probs { get => probs; }
        public int GoldSuccess { get => goldSuccess; }
        public int SubscribersSuccess { get => subscribersSuccess; }
        public float GaugeSuccess { get => gaugeSuccess; }
        public int GoldFailed { get => goldFailed; }
        public int SubscribersFailed { get => subscribersFailed; }
        public float GaugeFailed { get => gaugeFailed; }
    }
}