using UnityEngine;

[CreateAssetMenu(fileName = nameof(StaffData), menuName = "Create Staff Data")]
public class StaffData : ScriptableObject
{
    public string Name => _name;

    [SerializeField] private string _name;
}
