using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIRadialMenu), typeof(CanvasGroup))]
public class UISpellSelectionMenu : MonoBehaviour
{
    [SerializeField] private UISpell _spellPrefab;

    private UIRadialMenu _radialMenu;
    private CanvasGroup _canvas;
    private PlayerCharacter _player;
    private readonly List<ESpellId> _spellsSelected = new List<ESpellId>();

    private void Awake()
    {
        _radialMenu = GetComponent<UIRadialMenu>();
        _canvas = GetComponent<CanvasGroup>();
    }

    public void Constuct(PlayerCharacter player)
    {
        _player = player;
    }

    public void AddSpells(List<SpellBase> spells)
    {
        foreach (var spell in spells)
        {
            AddSpell(spell);
        }
    }

    public void AddSpell(SpellBase spell)
    {
        var spellInstance = Instantiate(_spellPrefab, transform);
        spellInstance.Construct(spell, _canvas);

        _radialMenu.AddItem((int)spell.Id, spellInstance.GetComponent<RectTransform>());

        spellInstance.OnSelected.AddListener(OnSpellSelected);
    }

    public void Show()
    {
        _canvas.alpha = 1;
        _canvas.interactable = true;

        _spellsSelected.Clear();
    }

    public void Hide()
    {
        _canvas.alpha = 0f;
        _canvas.interactable = false;

        ApplySpell();

        _spellsSelected.Clear();
    }

    private void OnSpellSelected(ESpellId spell)
    {
        if (_spellsSelected.Contains(spell))
            return;

        _spellsSelected.Add(spell);
    }

    private void ApplySpell()
    {
        var combinedSpell = ESpellId.None;
        foreach (var selectedSpell in _spellsSelected)
        {
            combinedSpell |= selectedSpell;
        }

        _player.CurrentStuff.SetSpell(combinedSpell);
    }
}
