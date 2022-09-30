using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryHandler : MonoBehaviour
{
    public GameObject _targetPrefab;
    public List<GameObject> _spawnedObjects;
    public FactoryHandler _factory;
    [SerializeField] Transform[] _pointsOrdinary, _pointsElite, _pointsEpic;

    public void Spawn()
    {
        for (int i = 0; i< Random.Range(2, 5); i++)
        {
            var target = Instantiate(_targetPrefab, _pointsOrdinary[i].position, Quaternion.identity);
            _spawnedObjects.Add(target);
            target.GetComponent<TargetController>().Spawned("Ordinary", _factory);
        }
        for (int i = 0; i < Random.Range(2,5); i++)
        {
            var target = Instantiate(_targetPrefab, _pointsElite[i].position, Quaternion.identity);
            _spawnedObjects.Add(target);
            target.GetComponent<TargetController>().Spawned("Elite", _factory);
        }
        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            var target = Instantiate(_targetPrefab, _pointsEpic[i].position, Quaternion.identity);
            _spawnedObjects.Add(target);
            target.GetComponent<TargetController>().Spawned("Epic", _factory);
        }
    }
}
