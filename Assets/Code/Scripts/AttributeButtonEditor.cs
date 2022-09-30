using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

using Object = UnityEngine.Object;

namespace BoxDefence
{
    [CustomEditor(typeof(Object),  true, isFallback = false)]
    [CanEditMultipleObjects]
    public class AttributeButtonEditor : Editor
    {
        #region Fields

        private const int SPACE_PIXELS = 10;

        private const int MAX_HEIGHT = 40;
        private const int MAX_WIDTH = 400;

        #endregion

        #region Public Methods

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(SPACE_PIXELS);

            StartDrawButtons();
        }

        #endregion

        #region Private Methods

        private void StartDrawButtons()
        {
            foreach (var target in targets)
            {
                IEnumerable<MethodInfo> methodsInfo = GetMethodInfo(target);
                if (methodsInfo == null)
                    continue;

                DrawMethodButtons(methodsInfo);
            }
        }
        private IEnumerable<MethodInfo> GetMethodInfo(Object target)
        {
            Type type = target.GetType();
            IEnumerable<MethodInfo> allMethodsInfo = type.GetMethods();
            IEnumerable<MethodInfo> attributeMethodsInfo = allMethodsInfo.Where(methods =>
                methods.GetCustomAttributes().
                Any(any => any.GetType() == typeof(EditorButtonAttribute)));

            return attributeMethodsInfo;
        }
        private void DrawMethodButtons(IEnumerable<MethodInfo> methodsInfo)
        {
            foreach (var methodInfo in methodsInfo)
            {
                if (methodInfo != null)
                {
                    var attribute = (EditorButtonAttribute)methodInfo.
                        GetCustomAttribute(typeof(EditorButtonAttribute));

                    DrawMethodButton(methodInfo, attribute);
                }
            }
        }
        private void DrawMethodButton(MethodInfo methodInfo, EditorButtonAttribute attribute)
        {
            GUILayoutOption[] layoutOptions = new GUILayoutOption[]
                            {
                                GUILayout.Height(MAX_HEIGHT),
                                GUILayout.Width(MAX_WIDTH)
                            };

            if (GUILayout.Button(attribute.Name, layoutOptions))
                methodInfo.Invoke(target, null);
        }

        #endregion
    }
}
