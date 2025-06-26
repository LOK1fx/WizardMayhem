using UnityEngine;

[CreateAssetMenu(fileName = nameof(WindSpellData), menuName = "Create wind spell data")]
public class WindSpellData : SpellData
{
    public float Distance => _distance;
    public float Force => _force;

    [SerializeField] private float _distance;
    [SerializeField] private float _force;
}
