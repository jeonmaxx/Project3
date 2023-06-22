using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    private SerializedProperty _obj;
    private SerializedProperty _type;
    private SerializedProperty _actionType;
    private SerializedProperty _range;
    private SerializedProperty _stackable;
    private SerializedProperty _image;
    private SerializedProperty _name;
    private SerializedProperty _seed;

    private void OnEnable()
    {
        _obj = serializedObject.FindProperty(nameof(Item.obj));
        _type = serializedObject.FindProperty(nameof(Item.type));
        _actionType = serializedObject.FindProperty(nameof(Item.actionType));
        _range = serializedObject.FindProperty(nameof(Item.range));
        _stackable = serializedObject.FindProperty(nameof(Item.stackable));
        _image = serializedObject.FindProperty(nameof(Item.image));
        _name = serializedObject.FindProperty(nameof(Item.itemName));
        _seed = serializedObject.FindProperty(nameof(Item.seed));
    }

    public override void OnInspectorGUI()
    {
        Item item = (Item)target;
        EditorGUILayout.PropertyField(_obj);
        EditorGUILayout.PropertyField(_type);
        EditorGUILayout.PropertyField(_actionType);
        EditorGUILayout.PropertyField(_range);
        EditorGUILayout.PropertyField(_stackable);
        EditorGUILayout.PropertyField(_image);
        EditorGUILayout.PropertyField (_name);

        if ((int)item.type == 2)
        {
            EditorGUILayout.PropertyField(_seed);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
