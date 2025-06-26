using System;
using UnityEngine;

[Flags]
public enum ESpellId : int
{
    None = 0,
    Fire = 1 << 0,
    Wind = 1 << 1,
    Reserved = 1 << 2,
}

[CreateAssetMenu(fileName = nameof(SpellData), menuName = "Create Spell Data")]
public class SpellData : ScriptableObject
{
    public ESpellId SpellId => _spellId;
    public string Name => _name;
    public LayerMask AffectableLayerMask => _affectableLayerMask;

    [SerializeField] private ESpellId _spellId;
    [SerializeField] private string _name;
    [SerializeField] private LayerMask _affectableLayerMask;
}
