using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

namespace GodJunie.VTuber.Editor {
    public class DataManagementWindow : OdinMenuEditorWindow {
        [MenuItem("GodJunie/데이터 관리")]
        private static void OpenWindow() {
            var window = GetWindow<DataManagementWindow>("데이터 관리");

            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1200, 700);
        }

        private static OdinMenuStyle customMenuStyle = new OdinMenuStyle {
            BorderPadding = 0f,
            AlignTriangleLeft = true,
            TriangleSize = 16f,
            TrianglePadding = 0f,
            Offset = 20f,
            Height = 23,
            IconPadding = 0f,
            BorderAlpha = 0.323f
        };

        protected override OdinMenuTree BuildMenuTree() {
            OdinMenuTree tree = new OdinMenuTree() {
                { "채팅 설정", Data.ChatSettings.Instance }
            };

            tree.Config.DrawSearchToolbar = true;

            tree.DefaultMenuStyle = customMenuStyle;
            tree.AddObjectAtPath("에디터 설정", customMenuStyle);

            return tree;
        }
    }
}