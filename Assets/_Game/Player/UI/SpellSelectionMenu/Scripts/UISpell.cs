using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISpell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent<ESpellId> OnSelected { get; } = new UnityEvent<ESpellId>();
    public UnityEvent<ESpellId> OnDeselected { get; } = new UnityEvent<ESpellId>();

    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] Image _image;

    private SpellBase _spell;
    private CanvasGroup _canvas;

    public void Construct(SpellBase spell, CanvasGroup canvas)
    {
        _spell = spell;
        _canvas = canvas;

        _label.text = spell.Name;
        _image.sprite = spell.Icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_canvas.interactable == false)
            return;

        OnSelected?.Invoke(_spell.Id);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_canvas.interactable == false)
            return;

        OnDeselected?.Invoke(_spell.Id);
    }
}
