using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIRadialMenu : MonoBehaviour
{
    [SerializeField] private Dictionary<int, RectTransform> _items = new();

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void AddItem(int id, RectTransform item)
    {
        _items.Add(id, item);

        UpdatePositions();
    }

    public void RemoveItem(int id)
    {
        _items.Remove(id);

        UpdatePositions();
    }

    private void UpdatePositions()
    {
        if (_items.Count == 0)
            return;

        var index = 0;
        foreach (var item in _items.Values)
        {
            var position = GetPointPosition(index);
            item.position = position;

            index++;
        }
    }

    private Vector3 GetPointPosition(int index)
    {
        var radius = Mathf.Min(_rectTransform.rect.width, _rectTransform.rect.height) / 2f;

        var angle = (float)index / _items.Count * Mathf.PI * 2f + Mathf.PI / 2f;
        var offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        var point = _rectTransform.position + offset;

        return point;
    }



#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        if (_rectTransform == null)
            _rectTransform = GetComponent<RectTransform>();

        if (_items.Count == 0)
            return;

        for (int i = 0; i < _items.Count; i++)
        {
            var position = GetPointPosition(i);

            Gizmos.color = Color.white;
            Gizmos.DrawSphere(position, 10f);
        }
    }

#endif
}
