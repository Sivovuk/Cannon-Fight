using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float _timeInterval;
    private float _timePass;

    [SerializeField]
    private GameObject _spawnPrefab;

    private void Update()
    {
        if (!PlayerManager.isGameFinish)
        {
            _timePass += Time.deltaTime;

            if (_timePass >= _timeInterval)
            {
                _timePass = 0;
                SpawnNewObject();
            }
        }
    }

    public void SpawnNewObject() 
    {
        GameObject spawn = Instantiate(_spawnPrefab, transform.position, Quaternion.identity);
        Destroy(spawn, 5);
    }
}
