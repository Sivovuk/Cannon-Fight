using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrippleShot : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * _speed);
    }
}
