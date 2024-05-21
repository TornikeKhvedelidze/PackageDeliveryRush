using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace CustomInspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ValueDropdownAttribute : PropertyAttribute
    {
        public string MethodPath { get; }

        public ValueDropdownAttribute(string methodName)
        {
            MethodPath = methodName;
        }
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class HideIfAttribute : PropertyAttribute
    {
        public string ConditionalFieldName { get; }
        public bool Condition { get; }

        public HideIfAttribute(string fieldName, bool condition = true)
        {
            ConditionalFieldName = fieldName;
            Condition = condition;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ValueDropdownAttribute))]
    [CustomPropertyDrawer(typeof(HideIfAttribute))]
    public class ValueDropdownDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return IsHidden(property) ? 0f : EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsHidden(property))
            {
                return;
            }

            if (property.propertyType == SerializedPropertyType.String)
            {
                ValueDropdownAttribute valueDropdownAttribute = attribute as ValueDropdownAttribute;

                string[] parts = valueDropdownAttribute.MethodPath.Split('.');

                if (parts.Length != 2)
                {
                    Debug.LogError("Invalid method name format");
                    return;
                }

                string className = parts[0];
                string methodName = parts[1];

                Type classType = Type.GetType(className);

                if (classType == null)
                {
                    Debug.LogError("Class not found: " + className);
                    return;
                }

                MethodInfo methodInfo = classType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);

                if (methodInfo == null)
                {
                    Debug.LogError("Method not found: " + methodName);
                    return;
                }

                string[] options = (string[])methodInfo.Invoke(property.serializedObject.targetObject, null);

                int selectedIndex = Mathf.Max(0, Array.IndexOf(options, property.stringValue));
                selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, options);
                property.stringValue = options[selectedIndex];
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        private bool IsHidden(SerializedProperty property)
        {
            HideIfAttribute hideIfAttribute = attribute as HideIfAttribute;

            if (hideIfAttribute != null)
            {
                SerializedProperty conditionProperty = property.serializedObject.FindProperty(hideIfAttribute.ConditionalFieldName);

                if (conditionProperty != null && conditionProperty.propertyType == SerializedPropertyType.Boolean)
                {
                    return conditionProperty.boolValue == hideIfAttribute.Condition;
                }
            }

            return false;
        }
    }
#endif
}
