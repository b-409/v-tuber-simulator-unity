// System
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
// UnityEngine
using UnityEngine;
// UnityEditor
using UnityEditor;
// OdinIndpector
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;

namespace B409.Jade.Data.Editor {
    public class DataEditor : OdinMenuEditorWindow {
        private static string CharacterDataFolderPath = "Assets/Characters";
      
        [MenuItem("B409/Game Data")]
        private static void OpenWindow() {
            var window = GetWindow<DataEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1200, 700);
            window.Show();
        }

        protected override void OnDestroy() {
            base.OnDestroy();
        }

        protected override void OnBeginDrawEditors() {
            OdinMenuTreeSelection selected = this.MenuTree.Selection;

            SirenixEditorGUI.BeginHorizontalToolbar(); 

            {
                GUILayout.FlexibleSpace();
               
            }

            SirenixEditorGUI.EndHorizontalToolbar();
        }

        protected override OdinMenuTree BuildMenuTree() {
            var tree = new OdinMenuTree(true);

            tree.Config.DrawSearchToolbar = true;

            return tree;
        }

        private void AddDragHandles(OdinMenuItem menuItem) {
            menuItem.OnDrawItem += x => DragAndDropUtilities.DragZone(menuItem.Rect, menuItem.Value, false, false);
        }
    }
}