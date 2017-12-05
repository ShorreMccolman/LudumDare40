using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject aboutPopup;

	void Start()
	{
		SoundManager.Instance.StartMenuMusic ();
		Time.timeScale = 1.0f;
	}

	public void StartGame()
	{
		SoundManager.Instance.PlaySoundEffect ("Click");
		SceneManager.LoadScene ("Main");
	}

	public void OpenAbout()
	{
		SoundManager.Instance.PlaySoundEffect ("Click");
		aboutPopup.SetActive (true);
	}

	public void CloseAbout()
	{
		SoundManager.Instance.PlaySoundEffect ("Click");
		aboutPopup.SetActive (false);
	}

	public void QuitGame()
	{
		SoundManager.Instance.PlaySoundEffect ("Click");
		Application.Quit ();
	}
}
