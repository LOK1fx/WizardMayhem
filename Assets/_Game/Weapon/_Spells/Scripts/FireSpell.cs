using System.Collections.Generic;
using UnityEngine;

[Spell(ESpellId.Fire)]
public class FireSpell : SpellBase
{
    public override ESpellId Id => ESpellId.Fire;
    public override string Name => "Fire Spell";

    public float BurnDistance => _burnDistance;
    public float TimeToBurn => _timeToBurn;

    [SerializeField] private float _burnDistance;
    [SerializeField] private float _timeToBurn;

    private Transform _cameraTransform;
    private bool _isActive = false;

    public override void StartExecution(PlayerCharacter sender)
    {
        _cameraTransform = sender.Camera.transform;

        _isActive = true;

        Debug.Log("Fire spell started");
    }

    public override void OnUpdate()
    {
        if (_isActive == false)
            return;

        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward,
            out var hit, BurnDistance, affectableLayerMask))
        {
            if (hit.collider.TryGetComponent<IFireSpellAffectable>(out var affectable))
                affectable.AffectFire(this);
        }
    }

    public override void StopExecution()
    {
        Debug.Log("Fire spell stop");

        _isActive = false;
    }
}
