using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{

	#region INT
	[SerializeField]
	private int _health;
	[SerializeField]
	private int _bulletIndex;
	#endregion

	[Space]

	#region FLOAT
	[SerializeField]
	private float _timePass;
	#endregion

	[Space]

	#region BOOLEAN
	[SerializeField]
	private bool isReadyToFire = true;
	[SerializeField]
	private bool isTrippleShotActivate = false;
	public static bool isGameFinish = false;
	#endregion

	//[Space]

	//#region List, Array

	//   #endregion

	[Space]

	#region Components
	[SerializeField]
	private GameObject _otherPlayer;
	[SerializeField]
	private GameObject _explosionPrefab;
	[SerializeField]
	private GameObject _ammoPrifab;
	[SerializeField]
	private GameObject _ammoTrippleShotPrefab;
	[SerializeField]
	private GameObject _trippleShot;
	[SerializeField]
	private Transform _shootingPoint;
	[SerializeField]
	private ParticleSystem _shootingEffect;
	[SerializeField]
	private ParticleSystem _damageEffect;
	[SerializeField]
	private Animator _animator;
	[SerializeField]
	private AudioSource _shootSound;
	[SerializeField]
	private AudioSource _hitSound;
	[Space]
	[SerializeField]
	private GameObject _inGameMenu;
	[SerializeField]
	private Button _continueButton;
	[SerializeField]
	private TextMeshProUGUI _winText;
	#endregion

	//[Space]

	//#region Scripts

	//   #endregion

	private void Update()
	{
		if (!isReadyToFire && !isGameFinish) 
		{
			_timePass += Time.deltaTime;
			if (_timePass >= 0.5f) 
			{
				isReadyToFire = true;
				_timePass = 0;
			}
		}

		if (_health < 3 && !_damageEffect.isPlaying) 
		{
			_damageEffect.Play();
		}
	}

	public void Shooting() 
	{
		if (isReadyToFire && !isGameFinish)
		{
			GameObject spawn;
			if (!isTrippleShotActivate)
			{
				spawn = Instantiate(_ammoPrifab, _shootingPoint.transform.position, _ammoPrifab.transform.localRotation);
				spawn.GetComponent<Ammo>().SetPlayer(this.gameObject, _bulletIndex);
			}
			else 
			{
				spawn = Instantiate(_ammoTrippleShotPrefab, _shootingPoint.transform.position, _ammoTrippleShotPrefab.transform.localRotation);
				for (int i = 0; i < spawn.transform.childCount; i++) 
				{
					spawn.transform.GetChild(i).GetComponent<Ammo>().SetPlayer(this.gameObject, _bulletIndex);
				}
				if (_bulletIndex == 1)
				{
					spawn.transform.eulerAngles = new Vector3(0, 0, 180);
				}
				isTrippleShotActivate = false;
				_trippleShot.SetActive(false);
			}
			

			_animator.SetTrigger("Fire");
			_shootingEffect.Play();
			_shootSound.Play();
			isReadyToFire = false;
		}
	}

	public void PlayerDamage() 
	{
		_health -= 1;
		_hitSound.Play();

		if (_health <= 0) 
		{
			GameObject spawn = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
			_winText.text = _otherPlayer.tag + " Wins!";
			_winText.enabled = true;
			_inGameMenu.SetActive(true);
			_continueButton.interactable = false;
			Destroy(gameObject, 0.25f);
		}
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ammo")) 
		{
			Destroy(collision.gameObject);

			PlayerDamage();
		}
	}

	public void ActivateTrippleShot() 
	{
		isTrippleShotActivate = true;
		_trippleShot.SetActive(true);
	}
}
