using UnityEngine;
using RollerBall;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(MinMaxInt))]
    [CustomPropertyDrawer(typeof(MinMaxFloat))]
    internal sealed class MinMax_PropertyDrawer : PropertyDrawer
    {
        private float Div(SerializedProperty property)
        {
            return (property.FindPropertyRelative("editorEditable").boolValue) ? 3f : 2f;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * Div(property);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var absMin = property.FindPropertyRelative("minLimit");
            var absMax = property.FindPropertyRelative("maxLimit");
            var min = property.FindPropertyRelative("min");
            var max = property.FindPropertyRelative("max");

            var editorEditable = property.FindPropertyRelative("editorEditable").boolValue;
            var isFloat = absMin.type == "float";
            var pos = position;
            var rows = Div(property);
            pos.height /= rows;
            var defaultPos = pos;

            pos = EditorGUI.PrefixLabel(pos, new GUIContent(ObjectNames.NicifyVariableName(property.name)));

            // values to pass in as ref in MinMaxSlider
            var ref_min = isFloat ? min.floatValue : min.intValue;
            var ref_max = isFloat ? max.floatValue : max.intValue;

            var minLimit = isFloat ? absMin.floatValue : absMin.intValue;
            var maxLimit = isFloat ? absMax.floatValue : absMax.intValue;

            EditorGUI.MinMaxSlider(pos, ref ref_min, ref ref_max, minLimit, maxLimit);

            if (isFloat)
            {
                min.floatValue = ref_min;
                max.floatValue = ref_max;
            }
            else
            {
                min.intValue = (int)ref_min;
                max.intValue = (int)ref_max;
            }

            //
            EditorGUI.indentLevel++;

            // Value
            pos = defaultPos;
            pos.y += pos.height; // Go to next row
            pos = EditorGUI.PrefixLabel(pos, new GUIContent("Range"));
            var widthLabel = pos.width / 3 / 2;
            var widthField = pos.width / 3;

            EditorGUI.indentLevel--;
            if (isFloat)
            {
                pos.width = widthLabel;
                EditorGUI.LabelField(pos, "Min");
                pos.x += pos.width;

                pos.width = widthField;
                min.floatValue = EditorGUI.FloatField(pos, min.floatValue);
                pos.x += pos.width;

                pos.width = widthLabel;
                EditorGUI.LabelField(pos, "Max");
                pos.x += pos.width;

                pos.width = widthField;
                max.floatValue = EditorGUI.FloatField(pos, max.floatValue);
            }
            else
            {
                pos.width = widthLabel;
                EditorGUI.LabelField(pos, "Min");
                pos.x += pos.width;

                pos.width = widthField;
                min.intValue = EditorGUI.IntField(pos, min.intValue);
                pos.x += pos.width;

                pos.width = widthLabel;
                EditorGUI.LabelField(pos, "Max");
                pos.x += pos.width;

                pos.width = widthField;
                max.intValue = EditorGUI.IntField(pos, max.intValue);
            }
            EditorGUI.indentLevel++;

            // ABS
            if (editorEditable)
            {
                pos = defaultPos;
                pos.y += pos.height * 2; // Go to next row
                pos = EditorGUI.PrefixLabel(pos, new GUIContent("Limit"));

                EditorGUI.indentLevel--;
                if (isFloat)
                {
                    pos.width = widthLabel;
                    EditorGUI.LabelField(pos, "Min");
                    pos.x += pos.width;

                    pos.width = widthField;
                    absMin.floatValue = EditorGUI.FloatField(pos, absMin.floatValue);
                    pos.x += pos.width;

                    pos.width = widthLabel;
                    EditorGUI.LabelField(pos, "Max");
                    pos.x += pos.width;

                    pos.width = widthField;
                    absMax.floatValue = EditorGUI.FloatField(pos, absMax.floatValue);
                }
                else
                {
                    pos.width = widthLabel;
                    EditorGUI.LabelField(pos, "Min");
                    pos.x += pos.width;

                    pos.width = widthField;
                    absMin.intValue = EditorGUI.IntField(pos, absMin.intValue);
                    pos.x += pos.width;

                    pos.width = widthLabel;
                    EditorGUI.LabelField(pos, "Max");
                    pos.x += pos.width;

                    pos.width = widthField;
                    absMax.intValue = EditorGUI.IntField(pos, absMax.intValue);
                }
            }
            else
            {
                EditorGUI.indentLevel--;
            }
        }
    }
}