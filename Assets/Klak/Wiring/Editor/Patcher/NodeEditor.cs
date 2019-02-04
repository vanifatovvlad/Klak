//
// Klak - Utilities for creative coding with Unity
//
// Copyright (C) 2016 Keijiro Takahashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Klak.Wiring.Patcher
{
    // Inspector GUI for the specialized node
    [CustomEditor(typeof(Node))]
    class NodeEditor : Editor
    {
        private string[] _addComponentPath;
        
        // Node component editor
        Editor _editor;

        void OnEnable()
        {
            if (_editor == null)
                _editor = CreateEditor(((Node)target).runtimeInstance);
            
            _addComponentPath = ((Node)target).runtimeInstance.GetType()
                .GetCustomAttributes(typeof(AddComponentMenu), true)
                .Select(o => (AddComponentMenu)o)
                .Select(o => o.componentMenu)
                .Select(o =>
                {
                    const string Prefix = "Klak/Wiring/";
                    return o.StartsWith(Prefix) ? o.Substring(Prefix.Length) : o;
                })
                .ToArray();            
        }

        void OnDestroy()
        {
            DestroyImmediate(_editor);
        }

        public override bool RequiresConstantRepaint()
        {
            return _editor != null ? _editor.RequiresConstantRepaint() : false;
        }

        protected override void OnHeaderGUI()
        {
            var node = (Node)target;

            if (_editor == null || !node.isValid) return;

            // Retrieve the header title (type name).
            var instance = node.runtimeInstance;
            var title = ObjectNames.NicifyVariableName(instance.GetType().Name);

            // Show the header title.
            GUILayout.BeginHorizontal(Styles.inspectorHeader);
            GUILayout.Space(14);
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            EditorGUILayout.LabelField(title, EditorStyles.boldLabel);

            foreach (var path in _addComponentPath)
            {
                EditorGUILayout.LabelField(path, EditorStyles.miniLabel);
            }

            GUILayout.Space(10);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            EditorGUILayout.Space();
        }

        public override void OnInspectorGUI()
        {
            var node = (Node)target;

            if (_editor == null || !node.isValid) return;

            // Show the node name field.
            var instance = node.runtimeInstance;
            instance.name = EditorGUILayout.TextField("Name", instance.name);

            EditorGUILayout.Space();

            // Node properties
            _editor.OnInspectorGUI();
        }
    }
}
