using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShootCooldownScript : MonoBehaviour
{
    [SerializeField]
    private float timePassShot;
    [SerializeField]
    private bool isFired;
    [SerializeField]
    private Button _shootButton;
    [SerializeField]
    private GameObject _shootCooldown;
    [SerializeField]
    private TextMeshProUGUI _shootCooldownText;

    private void Update()
    {
        if (isFired && !PlayerManager.isGameFinish) 
        {
            Calculations();
        }
    }

    public void Calculations() 
	{
        
        timePassShot -= Time.deltaTime;
        float temp = timePassShot / 0.5f;
        _shootCooldown.GetComponent<Image>().fillAmount = temp;
        temp = (float)System.Math.Round(temp, 2);
        _shootCooldownText.text = temp.ToString();


        if (timePassShot <= 0)
        {
            isFired = false;
            timePassShot = 0.5f;
            _shootCooldown.SetActive(false);
            _shootCooldown.GetComponent<Image>().fillAmount = 1;
            _shootButton.interactable = true;
        }
    }

    public void Shoot() 
    {
        if (!isFired) 
        {
            isFired = true;
            _shootButton.interactable = false;
            _shootCooldown.SetActive(true);
        }
    }
}
