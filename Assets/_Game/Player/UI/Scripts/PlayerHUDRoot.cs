using UnityEngine;

public class PlayerHUDRoot : MonoBehaviour
{
    private PlayerCharacter _player;

    public void Bind(PlayerCharacter player)
    {
        _player = player;

        _player.CurrentStuff.OnStartSpellSelection += OnStartSpellSelection;
        _player.CurrentStuff.OnEndSpellSelection += OnEndSpellSelection;
    }

    private void OnDestroy()
    {
        _player.CurrentStuff.OnStartSpellSelection -= OnStartSpellSelection;
        _player.CurrentStuff.OnEndSpellSelection -= OnEndSpellSelection;
    }

    private void OnEndSpellSelection()
    {
        throw new System.NotImplementedException();
    }

    private void OnStartSpellSelection()
    {
        throw new System.NotImplementedException();
    }
}
