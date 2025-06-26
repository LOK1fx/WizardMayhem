using System.Collections.Generic;
using UnityEngine;

public class FireSpell : SpellBase<FireSpellData>
{
    public override ESpellId Id => ESpellId.Fire;

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
            out var hit, Data.BurnDistance, Data.AffectableLayerMask))
        {
            if (hit.collider.TryGetComponent<IFireSpellAffectable>(out var affectable))
                affectable.AffectFire(Data);
        }
    }

    public override void StopExecution()
    {
        Debug.Log("Fire spell stop");

        _isActive = false;
    }
}
