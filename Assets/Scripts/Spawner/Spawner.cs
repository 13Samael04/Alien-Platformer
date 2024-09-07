using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Point> _points;
    [SerializeField] private float _delay = 1f;

    private void Start()
    {
        StartCoroutine(nameof(Spawn), _delay);
    }

    private Point GetRandomPoints()
    {
        return _points[Random.Range(0, _points.Count)];
    }

    private IEnumerator Spawn(float delay)
    {
        WaitForSeconds waitTime = new WaitForSeconds(delay);
        Point point = new Point();

        while (true)
        {
            point = GetRandomPoints();

            if (point.IsCoin == false)
            {
                point.SpawnCoin();
            }

            yield return waitTime;
        }
    }
}
