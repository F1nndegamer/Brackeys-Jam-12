using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class OceanGenerator : MonoBehaviour
{
    private const int NUM_OF_THREADS = 8;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _tilePrefab;

    private GameObject _playerTile;
    private Dictionary<Vector2Int, GameObject> _tiles;

    private GameObject GenerateTile(Vector2Int position)
    {
        return Instantiate(_tilePrefab, new Vector3(position.x * _tilePrefab.transform.localScale.x, position.y * _tilePrefab.transform.localScale.y, 0f), Quaternion.identity);
    }

    private void AddTile(Vector2Int position)
    {
        if (_tiles.ContainsKey(position))
        {
            _tiles[position].SetActive(true);
        }
        else
        {
            _tiles.Add(position, GenerateTile(position));
        }
    }
    private void Awake()
    {
        _tiles = new Dictionary<Vector2Int, GameObject>();

        AddTile(new Vector2Int(0, 0));

        _playerTile = _tiles[Vector2Int.zero];
    }
    private void Update()
    {
        Vector2Int playerPos = new Vector2Int(Mathf.RoundToInt(_player.transform.position.x / _tilePrefab.transform.localScale.x), Mathf.RoundToInt(_player.transform.position.y / _tilePrefab.transform.localScale.y));

        if (_tiles.ContainsKey(playerPos))
        {
            _playerTile = _tiles[playerPos];
        }

        foreach (KeyValuePair<Vector2Int, GameObject> pair in _tiles)
        {
            pair.Value.SetActive(false);
        }

        if (Mathf.Abs(_player.transform.position.x - playerPos.x * _tilePrefab.transform.localScale.x) <= _tilePrefab.transform.localScale.x)
        {
            AddTile(new Vector2Int(playerPos.x, playerPos.y));
        }
        if (Mathf.Abs(_player.transform.position.x - (playerPos.x + 1) * _tilePrefab.transform.localScale.x) <= _tilePrefab.transform.localScale.x) {
            AddTile(new Vector2Int(playerPos.x + 1, playerPos.y));
        }
        if (Mathf.Abs(_player.transform.position.x- (playerPos.x - 1) * _tilePrefab.transform.localScale.x) <= _tilePrefab.transform.localScale.x)
        {
            AddTile(new Vector2Int(playerPos.x - 1, playerPos.y));
        }
    }
}
