using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    private List<ISpell> _allSpells = new();

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

    public void RegisterSpell<T>(SpellBase<T> spell) where T : SpellData
    {
        _allSpells.Add(spell);
    }

    public bool TryGetSpellInstance(ESpellId id, out ISpell spell)
    {
        spell = _allSpells
            .Where(s => s.Id == id)
            .FirstOrDefault();

        return spell != null;
    }
}
