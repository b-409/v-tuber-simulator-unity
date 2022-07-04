// System
using System.Collections;
using System.Collections.Generic;
// UnityEngine
using UnityEngine;
// Editor
using Sirenix.OdinInspector;

namespace GodJunie.VTuber.Data {
    [CreateAssetMenu(fileName = "CharacterData", menuName = "B409/Character Data")]
    public class CharacterData : ScriptableObject {
        [HorizontalGroup("group", 100f)]
        [VerticalGroup("group/group")]
        [BoxGroup("group/group/thumbnail")]
        [PreviewField(Alignment = ObjectFieldAlignment.Center, Height = 100f)]
        [HideLabel]
        [SerializeField]
        private Sprite thumbnail;

        [SerializeField]
        private Sprite background;

        [SerializeField]
        private int id;
        [SerializeField]
        private new string name;
        [SerializeField]
        private string description;

        [SerializeField]
        private string height;
        [SerializeField]
        private string weight;
        [SerializeField]
        private string age;
        [SerializeField]
        private string birthday;
        [SerializeField]
        private string hometown;
        [SerializeField]
        private string hobby;

        [SerializeField]
        private GameObject prefab;

        public int Id => id;
        public string Name => name;
        public string Description => description;
        public GameObject Prefab => prefab;
        public Sprite Thumbnail => thumbnail;
        public Sprite Background => background;
    }
}