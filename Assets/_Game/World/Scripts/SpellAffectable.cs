using UnityEngine;

public class SpellAffectable : MonoBehaviour, IFireSpellAffectable
{
    private float _currentBurnTimer;

    public void AffectFire(FireSpell spell)
    {
        _currentBurnTimer += Time.deltaTime;

        if (_currentBurnTimer >= spell.TimeToBurn)
            Destroy(gameObject);
    }
}
