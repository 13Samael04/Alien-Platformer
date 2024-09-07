using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Point : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    private int _initialSizePool = 5;
    private int _maxSizePool = 10;

    private ObjectPool<Coin> _pool;

    public bool IsCoin { get; private set; } = false;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_coinPrefab),
            actionOnGet: (obj) => ActivateCoin(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _initialSizePool,
            maxSize: _maxSizePool);
    }

    public void SpawnCoin()
    {
        _pool.Get();
    }

    private void ActivateCoin(Coin coin)
    {
        coin.Destroyed += Release;
        coin.transform.position = transform.position;
        coin.gameObject.SetActive(true);
        IsCoin = true;
    }

    private void Release(Coin coin)
    {
        coin.Destroyed -= Release;
        _pool.Release(coin);
        IsCoin = false;
    }
}
