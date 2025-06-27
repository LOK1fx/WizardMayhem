using UnityEngine;

public class PlayerHUDRoot : MonoBehaviour
{
    [SerializeField] private UISpellSelectionMenu _spellSelectionMenu;

    private PlayerCharacter _player;

    public void Bind(PlayerCharacter player)
    {
        _player = player;

        _player.CurrentStuff.OnStartSpellSelection += OnStartSpellSelection;
        _player.CurrentStuff.OnEndSpellSelection += OnEndSpellSelection;

        _spellSelectionMenu.Constuct(_player);
        _spellSelectionMenu.AddSpells(_player.CurrentStuff.GetInitialSpells());
        _spellSelectionMenu.Hide();
    }

    private void OnDestroy()
    {
        _player.CurrentStuff.OnStartSpellSelection -= OnStartSpellSelection;
        _player.CurrentStuff.OnEndSpellSelection -= OnEndSpellSelection;
    }

    private void OnEndSpellSelection()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _spellSelectionMenu.Hide();
    }

    private void OnStartSpellSelection()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _spellSelectionMenu.Show();
    }
}
