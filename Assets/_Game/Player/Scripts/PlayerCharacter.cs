using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public PlayerCamera Camera => _camera;
    public Staff CurrentStuff => _currentStaff;

    [Header("Components")]
    [SerializeField] private PlayerCamera _camera;

    [Header("Weapon")]
    [SerializeField] private Staff _currentStaff;
    [SerializeField] private ESpellId _startupSpellId = ESpellId.None;


    private void Start()
    {
        _currentStaff.Equip(this);

        _currentStaff.SetSpell(_startupSpellId);
    }

    private void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _currentStaff.TryStartUsage();
        if (Input.GetKeyUp(KeyCode.Mouse0))
            _currentStaff.TryStopUsage();

        if (Input.GetKeyDown(KeyCode.Mouse1))
            _currentStaff.StartSpellChoosing();
        if (Input.GetKeyUp(KeyCode.Mouse1))
            _currentStaff.StopSpellChoosing();

        _camera.SetInput(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
    }
}
