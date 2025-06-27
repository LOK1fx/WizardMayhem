using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpellManager))]
public class Staff : MonoBehaviour
{
    public event Action OnStartSpellSelection;
    public event Action OnEndSpellSelection;

    public SpellBase CurrentSpell { get; private set; }
    public PlayerCharacter Owner { get; private set; }


    [SerializeField] private List<ESpellId> _initialSpells = new();
    [SerializeField, SerializeReference, NonReorderable] private SpellBase[] _spells = new SpellBase[1]
    {
        new FireSpell(),
    };

    [SerializeField] private StaffData _data;

    private SpellManager _spellManager;

    private void Awake()
    {
        _spellManager = GetComponent<SpellManager>();

        _spellManager.RegisterSpells(_spells.ToList());
    }

    public void Equip(PlayerCharacter player)
    {
        Owner = player;
    }

    public void SetSpell(ESpellId spellId)
    {
        if (CurrentSpell != null)
            CurrentSpell.StopExecution();

        if (spellId == ESpellId.None)
            CurrentSpell = null;

        if (_spellManager.TryGetSpellInstance(spellId, out var spell))
            CurrentSpell = spell;
        else
            Debug.LogError($"{spellId} not found.");
    }

    public List<SpellBase> GetInitialSpells()
    {
        var result = new List<SpellBase>();

        foreach (var spell in _spells)
        {
            if (_initialSpells.Where(s => s == spell.Id).Any())
                result.Add(spell);
        }

        return result;
    }

    public bool TryStartUsage()
    {
        if (CurrentSpell == null || Owner == null)
            return false;

        CurrentSpell.StartExecution(Owner);
        return true;
    }

    public bool TryStopUsage()
    {
        if (CurrentSpell == null || Owner == null)
            return false;

        CurrentSpell.StopExecution();
        return true;
    }

    public void StartSpellChoosing()
    {
        OnStartSpellSelection?.Invoke();
    }

    public void StopSpellChoosing()
    {
        OnEndSpellSelection?.Invoke();
    }
}
