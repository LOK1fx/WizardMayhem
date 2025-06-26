using System;
using UnityEngine;

[RequireComponent(typeof(SpellManager))]
public class Staff : MonoBehaviour
{
    public event Action OnStartSpellSelection;
    public event Action OnEndSpellSelection;

    public ISpell CurrentSpell { get; private set; }
    public PlayerCharacter Owner { get; private set; }


    [SerializeField] private StaffData _data;

    private SpellManager _spellManager;

    private void Awake()
    {
        _spellManager = GetComponent<SpellManager>();

        _spellManager.RegisterSpell(new FireSpell());
        _spellManager.RegisterSpell(new WindSpell());
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

    public void StartChoosingSpell()
    {
        OnStartSpellSelection?.Invoke();
    }

    public void StopChoosingSpell()
    {
        OnEndSpellSelection?.Invoke();
    }
}
