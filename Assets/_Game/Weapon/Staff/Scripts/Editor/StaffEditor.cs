using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Staff))]
public class StaffEditor : Editor
{
    private ESpellId _selectedSpellId = ESpellId.None;
    private bool _showSpellAdder = false;

    private void OnEnable()
    {
        var availableSpellIds = SpellRegistry.GetAllSpellIds().ToArray();
        if (availableSpellIds.Length > 0 && _selectedSpellId == ESpellId.None)
            _selectedSpellId = availableSpellIds[0];
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space(4f);
        EditorGUILayout.LabelField("Spell Manager", EditorStyles.largeLabel);
        EditorGUILayout.Space(2f);

        DrawCurrentSpells();

        EditorGUILayout.Space();

        _showSpellAdder = EditorGUILayout.Foldout(_showSpellAdder, "Add new spell");

        if (_showSpellAdder)
            DrawSpellAdder();
    }

    private void DrawCurrentSpells()
    {
        EditorGUILayout.LabelField("Spells:", EditorStyles.boldLabel);

        var spellsProperty = serializedObject.FindProperty("_spells");
        
        if (spellsProperty != null)
        {
            for (int i = 0; i < spellsProperty.arraySize; i++)
            {
                var spellProperty = spellsProperty.GetArrayElementAtIndex(i);
                
                EditorGUILayout.BeginHorizontal();
                
                var spellTypeName = "Empty";
                ESpellId spellId = ESpellId.None;
                
                if (spellProperty.managedReferenceValue != null)
                {
                    spellTypeName = spellProperty.managedReferenceValue.GetType().Name;

                    if (spellProperty.managedReferenceValue is SpellBase spell)
                        spellId = spell.Id;
                }
                
                EditorGUILayout.LabelField($"Spell {i + 1}: {spellTypeName} ({spellId})");
                
                if (GUILayout.Button("Remove", GUILayout.Width(60)))
                {
                    RemoveSpellAtIndex(i);
                    break;
                }
                
                EditorGUILayout.EndHorizontal();
            }
        }
    }

    private void DrawSpellAdder()
    {
        EditorGUILayout.BeginVertical("box");

        var availableSpellIds = SpellRegistry.GetAllSpellIds().ToArray();
        
        if (availableSpellIds.Length == 0)
        {
            EditorGUILayout.HelpBox("There is no spells.", MessageType.Warning);
        }
        else
        {
            var spellNames = new string[availableSpellIds.Length];
            for (int i = 0; i < availableSpellIds.Length; i++)
            {
                var spellId = availableSpellIds[i];
                spellNames[i] = $"{spellId}";
            }

            var selectedIndex = Array.IndexOf(availableSpellIds, _selectedSpellId);
            if (selectedIndex == -1)
                selectedIndex = 0;

            var newSelectedIndex = EditorGUILayout.Popup("Spell Type:", selectedIndex, spellNames);
            if (newSelectedIndex != selectedIndex)
                _selectedSpellId = availableSpellIds[newSelectedIndex];

            if (_selectedSpellId != ESpellId.None)
            {
                EditorGUILayout.HelpBox($"Spell to add: {_selectedSpellId}", MessageType.Info);

                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("Add spell"))
                {
                    AddSpell(_selectedSpellId);
                    _selectedSpellId = ESpellId.None;
                }
                
                if (_selectedSpellId == ESpellId.None && availableSpellIds.Length > 0)
                    _selectedSpellId = availableSpellIds[0];
                
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndVertical();
    }

    private void AddSpell(ESpellId spellId)
    {
        var newSpell = SpellRegistry.CreateSpell(spellId);
        
        if (newSpell != null)
        {
            var spellsProperty = serializedObject.FindProperty("_spells");
            spellsProperty.arraySize++;
            
            var newSpellProperty = spellsProperty.GetArrayElementAtIndex(spellsProperty.arraySize - 1);
            newSpellProperty.managedReferenceValue = newSpell;
            
            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            Debug.LogError($"Error adding spell instance: {spellId}");
        }
    }

    private void RemoveSpellAtIndex(int index)
    {
        var spellsProperty = serializedObject.FindProperty("_spells");
        
        if (index >= 0 && index < spellsProperty.arraySize)
        {
            spellsProperty.DeleteArrayElementAtIndex(index);

            serializedObject.ApplyModifiedProperties();
        }
    }
}