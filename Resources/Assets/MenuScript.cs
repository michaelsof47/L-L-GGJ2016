using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void quitGame() {
		Application.Quit();
	}

	public void startGame() {
		SceneManager.LoadScene(2);
	}

	public void toMenu() {
		SceneManager.LoadScene(1);
	}
}
