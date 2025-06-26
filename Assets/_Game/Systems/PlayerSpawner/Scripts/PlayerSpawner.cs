using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _playerPrefab;
    [SerializeField] private PlayerHUDRoot _playerHUDPrefab;

    private void Start()
    {
        var foundPlayer = FindFirstObjectByType<PlayerCharacter>();

        CreateAndBindUI(foundPlayer == null ? SpawnPlayer() : foundPlayer);
    }

    private PlayerCharacter SpawnPlayer()
    {
        return Instantiate(_playerPrefab, transform.position, Quaternion.identity);
    }

    private void CreateAndBindUI(PlayerCharacter player)
    {
        var spawnedUi = Instantiate(_playerHUDPrefab);
        spawnedUi.Bind(player);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one / 2f);
    }
}
