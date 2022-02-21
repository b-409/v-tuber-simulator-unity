using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEngine;

namespace GodJunie {
    public abstract class DataConfig<T> : ScriptableObject where T : DataConfig<T>, new() {
#if UNITY_EDITOR
        private bool isLoadedFromAsset = true;
        public bool IsLoadedFromAsset { get { return isLoadedFromAsset; } }
        private static T _instance = null;
        public static T Instance {
            get {
                if(_instance == null) {
                    var guid = UnityEditor.AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T).Name)).FirstOrDefault();
                    Debug.Log(guid);
                    if(guid == default) {
                        _instance = new T();
                        _instance.isLoadedFromAsset = false;
                    } else {
                        string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                        Debug.Log(path);
                        _instance = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
                    }
                }
                return _instance;
            }
        }
        [InfoBox("���� ���Ŀ� �ڵ����� ������ ���̺��� �ҷ��ɴϴ�. �ߺ� ������� �ʵ��� �������ּ���!")]
        [Button("�����ϱ�", ButtonHeight = 50), HideIf("isLoadedFromAsset")]
        private void CreateTable() {
            string path = UnityEditor.EditorUtility.SaveFilePanel("�����ϱ�", "Assets", "", "asset");
            if(!path.StartsWith(Application.dataPath))
                return;
            path = path.Replace(Application.dataPath, "Assets");
            Debug.Log(path);
            try {
                _instance.isLoadedFromAsset = true;
                UnityEditor.AssetDatabase.CreateAsset(_instance, path);
            }
            catch(Exception e) {
                Debug.LogError(e.Message);
                return;
            }
        }
#endif
    }
}