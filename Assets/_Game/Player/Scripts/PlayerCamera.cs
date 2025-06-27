using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    
    public float Sensitivity => _sensitivity;

    [SerializeField] private Transform _body;
    [Space]
    [SerializeField] private float _sensitivity = 8;

    [Space]
    [SerializeField] private float _minPitch = -90f;
    [SerializeField] private float _maxPitch = 90f;

    private float _pitch;
    private float _yaw;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);
        if (_yaw % 360 == 0)
            _yaw = 0;

        transform.localRotation = Quaternion.Euler(Vector3.right * _pitch);
        _body.localRotation = Quaternion.Euler(Vector3.up * _yaw);
    }

    public void SetInput(Vector2 mouseDelta)
    {
        if (Cursor.lockState == CursorLockMode.None)
            return;

        _pitch -= mouseDelta.y * _sensitivity;
        _yaw += mouseDelta.x * _sensitivity;
    }
}
