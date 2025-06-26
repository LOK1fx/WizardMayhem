using UnityEngine;

public class SpellAffectable : MonoBehaviour, IFireSpellAffectable
{
    private float _currentBurnTimer;

    public void AffectFire(FireSpellData data)
    {
        _currentBurnTimer += Time.deltaTime;

        if (_currentBurnTimer >= data.TimeToBurn)
            Destroy(gameObject);
    }
}
