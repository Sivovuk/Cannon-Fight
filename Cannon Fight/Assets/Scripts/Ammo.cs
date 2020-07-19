using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ammo : MonoBehaviour
{
    public int _index;
    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _player;

    private void Start()
    {
        Destroy(this.gameObject, 4);
    }

    private void Update()
    {
        if (_index == 1 && transform.parent == null)
        {
            transform.Translate(Vector2.up * Time.deltaTime * _speed);
            transform.GetChild(0).eulerAngles = new Vector3(0, 0, 90);
        }
        else if (_index == -1 && transform.parent == null)
        {
            transform.Translate(Vector2.down * Time.deltaTime * _speed);
        }
        else if (transform.parent != null) 
        {
            transform.Translate(Vector2.down * Time.deltaTime * _speed);
        }
        else
        {
            Debug.LogError("Wrong index passed! => Ammo");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ammo")) 
        {
            DestroyAmmo();
        }

        if (collision.CompareTag("TrippleShot")) 
        {
            _player.GetComponent<PlayerManager>().ActivateTrippleShot();
            Destroy(collision.gameObject);
            DestroyAmmo();
        }
    }

    public void DestroyAmmo() 
    {
        _speed = 0;
        GameObject spawn = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(spawn, 2f);
        Destroy(this.gameObject, 0.25f);
    }

    public void SetPlayer(GameObject player, int bulletIndex) 
    {
        _player = player;
        _index = bulletIndex;
    }
}
