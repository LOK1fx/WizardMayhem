using System;
using UnityEngine;

[Serializable]
public abstract class SpellBase
{
    public abstract ESpellId Id { get; }
    public abstract string Name { get; }

    public Sprite Icon => _icon;

    [SerializeField] private Sprite _icon;

    [SerializeField] protected LayerMask affectableLayerMask;

    public abstract void StartExecution(PlayerCharacter sender);
    public abstract void StopExecution();
    public abstract void OnUpdate();
    public virtual void OnFixedUpdate() { }
}
