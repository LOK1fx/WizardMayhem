using UnityEngine;

[CreateAssetMenu(fileName = nameof(FireSpell), menuName = "Create Fire Spell Data")]
public class FireSpellData : SpellData
{
    public float TimeToBurn => _timeToBurn;
    public float BurnRadius => _burnRadius;
    public float BurnDistance => _burnDistance;

    [SerializeField] private float _timeToBurn = 6f;
    [SerializeField] private float _burnRadius;
    [SerializeField] private float _burnDistance;
}
