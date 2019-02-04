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
using UnityEngine;
using UnityEditor;
using Graphs = UnityEditor.Graphs;

namespace Klak.Wiring.Patcher
{
    // Custom editor styles
    public static class Styles
    {
        static GUIStyle _pinIn;

        public static GUIStyle pinIn
        {
            get {
                if (_pinIn == null) {
                    _pinIn = new GUIStyle(Graphs.Styles.triggerPinIn);
                    _pinIn.font = Graphs.Styles.varPinIn.font;
                    _pinIn.stretchWidth = false;
                    _pinIn.richText = true;
                }
                return _pinIn;
            }
        }

        static GUIStyle _pinOut;

        public static GUIStyle pinOut
        {
            get {
                if (_pinOut == null) {
                    _pinOut = new GUIStyle(Graphs.Styles.triggerPinOut);
                    _pinOut.font = Graphs.Styles.varPinOut.font;
                    _pinOut.stretchWidth = false;
                    _pinOut.richText = true;
                }
                return _pinOut;
            }
        }
        
        public static readonly GUIStyle varletContainer;
        public static readonly GUIStyle inspectorHeader;

        static Styles()
        {
            var builtinSkin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector);

            varletContainer = new GUIStyle(builtinSkin.GetStyle("IN BigTitle"))
            {
                margin = new RectOffset(0, 0, 5, 5),
                padding = new RectOffset(0, 0, 5, 5),
                stretchWidth = false,
                stretchHeight = false
            };
            
            inspectorHeader = new GUIStyle(builtinSkin.GetStyle("IN BigTitle Post"))
            {
                margin = new RectOffset(),
                padding = new RectOffset(),
                stretchWidth = false,
                stretchHeight = false
            };
        }

        public static string GetSlotTypeRichLabel(string type)
        {
            return ("<color=#444><i>" + type + "</i></color>");
        }
    }
}
