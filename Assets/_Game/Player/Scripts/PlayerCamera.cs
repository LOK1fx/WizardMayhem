using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float Sensitivity => _sensitivity;
    [SerializeField] private float _sensitivity = 8;

    [Space]
    [SerializeField] private float _minPitch = -90f;
    [SerializeField] private float _maxPitch = 90f;

    private float _pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);
        transform.localRotation = Quaternion.Euler(Vector3.right * _pitch);
    }

    public void SetInput(float verticalDelta)
    {
        _pitch -= verticalDelta * _sensitivity;
    }
}
