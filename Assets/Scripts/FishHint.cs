using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHint : MonoBehaviour
{
    [SerializeField] private float _bobTime = 1f;
    [SerializeField] private float _bobSize = 0.2f;

    private GameObject _hintPrefab;
    private float _timeSinceLastBob;

    private IEnumerator Bob()
    {
        transform.localScale = Vector3.one * _bobSize;
        yield return new WaitForEndOfFrame();
        transform.localScale = Vector3.zero;
    }

    private void Awake()
    {
        _timeSinceLastBob = 0;
        transform.localScale = Vector3.zero;
        _hintPrefab = Resources.Load<GameObject>("FishHint");
    }

    private void Update()
    {
        _timeSinceLastBob += Time.deltaTime;

        if (_timeSinceLastBob > _bobTime)
        {
            _timeSinceLastBob = 0f;
            StartCoroutine(Bob());
        }
    }
}
