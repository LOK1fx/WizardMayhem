using UnityEngine;

public class WindSpell : SpellBase<WindSpellData>
{
    public override ESpellId Id => ESpellId.Wind;

    private Transform _camera;
    private bool _isActive;
    private Rigidbody _selectedRigidbody;

    public override void OnUpdate()
    {
        if (_isActive == false)
            return;

        if (Physics.Raycast(_camera.position, _camera.forward, out var hit, Data.Distance, Data.AffectableLayerMask))
        {
            if (hit.collider.TryGetComponent<Rigidbody>(out var rigidbody))
                _selectedRigidbody = rigidbody;
            else
                _selectedRigidbody = null;
        }
        else
        {
            _selectedRigidbody = null;
        }
    }

    public override void OnFixedUpdate()
    {
        if (_isActive == false || _selectedRigidbody == null)
            return;

        _selectedRigidbody.AddForce(_camera.forward * Data.Force, ForceMode.Force);
    }

    public override void StartExecution(PlayerCharacter sender)
    {
        _camera = sender.Camera.transform;
        _selectedRigidbody = null;

        _isActive = true;
    }

    public override void StopExecution()
    {
        _selectedRigidbody = null;
        _isActive = false;
    }
}
