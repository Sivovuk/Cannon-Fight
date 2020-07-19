using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private int _touchRestrict;
	[SerializeField]
	private float _offsetX = 0.5f;
	[SerializeField]
	private bool isPressed = false;

	[SerializeField]
	private Animator _animatorTracks;

	private List<Touch> _touch = new List<Touch>();

	Vector2 _mousePosition;
	float offset;

	private void Update()
	{
		/*	Android controlls	*/
		if (!PlayerManager.isGameFinish)
		{
			for (int i = 0; i < Input.touchCount; i++)
			{
				if (_touch.Count > 0 && Input.GetTouch(i).fingerId == _touch[0].fingerId)
				{
					_touch[0] = Input.GetTouch(i);
				}

				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);

				if (hit.collider != null)
				{
					var collider = hit.collider;

					if (collider.CompareTag(gameObject.tag))
					{
						if (_touch.Count <= 0)
						{
							_touch.Add(Input.GetTouch(i));
						}

					}
				}
			}

			if (_touch.Count > 0 && _touch[0].phase == TouchPhase.Moved)
			{
				Movement(_touch[0].position);
			}
			if (_touch.Count > 0 && _touch[0].phase == TouchPhase.Ended)
			{
				_touch.Clear();
			}
		}

		/*	PC controlls	*/
		//if (Input.GetMouseButton(0) && !isPressed)
		//{
		//	RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		//	if (hit.collider != null)
		//	{
		//		Debug.LogError(hit.collider.tag);
		//		var collider = hit.collider;
		//		//test.text = collider.tag;
		//		if (collider.CompareTag(gameObject.tag))
		//		{
		//			isPressed = true;
		//			//_mousePosition = Input.mousePosition;
		//			Vector2 _temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//			offset = transform.position.x - _temp.x;
		//			if (offset > 0)
		//			{
		//				offset = -offset;
		//			}
		//			else if (offset < 0)
		//			{
		//				offset = Mathf.Abs(offset); ;
		//			}
		//		}
		//	}
		//}

		//if (Input.GetMouseButtonUp(0) && isPressed)
		//{
		//	isPressed = false;
		//	offset = 0;
		//	_mousePosition = new Vector2(0, 0);
		//}

		//if (isPressed)
		//{
		//	_mousePosition = Input.mousePosition;
		//	Movement(_mousePosition);
		//}
	}

	private void Movement(Vector2 position)
	{
		float _screenWidth = Screen.width;

		Vector3 _offsetPosition = Camera.main.ScreenToWorldPoint(new Vector3(_screenWidth, 0, 0));
		_offsetPosition = new Vector3(_offsetPosition.x - _offsetX, 0, 0);

		_mousePosition = Camera.main.ScreenToWorldPoint(position);

		if (_mousePosition.x > -_offsetPosition.x && _mousePosition.x < _offsetPosition.x)
		{
			transform.position = new Vector3(_mousePosition.x - offset, transform.position.y, -3);
			_animatorTracks.Play("Move");
		}
	}

}
