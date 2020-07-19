using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	public void OpenInGameMenu(GameObject menu) 
	{
		Time.timeScale = 0;
		menu.SetActive(true);
	}

	public void CloseInGameMenu(GameObject menu) 
	{
		Time.timeScale = 1;
		menu.SetActive(false);
	}


	public void OpenScene(int sceneIndex) 
	{
		Application.LoadLevel(sceneIndex);
		Time.timeScale = 1;
	}

	public void Quit() 
	{
		Application.Quit();
	}
}
