using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    private readonly List<SpellBase> _allSpells = new();

    private void Update()
    {
        foreach (var spell in _allSpells)
        {
            spell.OnUpdate();
        }
    }

    private void FixedUpdate()
    {
        foreach(var spell in _allSpells)
        {
            spell.OnFixedUpdate();
        }
    }

    public void RegisterSpell(SpellBase spell)
    {
        _allSpells.Add(spell);
    }

    public void RegisterSpells(List<SpellBase> spells)
    {
        _allSpells.AddRange(spells);
    }

    public bool TryGetSpellInstance(ESpellId id, out SpellBase spell)
    {
        spell = _allSpells
            .Where(s => s.Id == id)
            .FirstOrDefault();

        return spell != null;
    }
}
