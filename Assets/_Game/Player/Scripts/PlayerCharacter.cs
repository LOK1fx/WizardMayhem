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

    private float _yaw;


    private void Start()
    {
        _currentStaff.Equip(this);

        _currentStaff.SetSpell(_startupSpellId);
    }

    private void Update()
    {
        ProcessInput();
        RotatePlayer();
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _currentStaff.TryStartUsage();
        if (Input.GetKeyUp(KeyCode.Mouse0))
            _currentStaff.TryStopUsage();

        _camera.SetInput(Input.GetAxis("Mouse Y"));
        _yaw += Input.GetAxis("Mouse X") * _camera.Sensitivity;

        if (_yaw % 360 == 0)
            _yaw = 0;
    }

    private void RotatePlayer()
    {
        transform.localRotation = Quaternion.Euler(Vector3.up * _yaw);
    }
}
