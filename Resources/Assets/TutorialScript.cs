using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour {

	public GameObject tutorialPanel;
	// Use this for initialization
	void Awake () {
		EnemyMovementScript.gameOn = false;
		if(GetComponent<CrosshairScript>().tutorial) tutorialPanel.SetActive(true);
	}

	public void hideTut() {
		//EnemyMovementScript.gameOn = true;
		tutorialPanel.SetActive(false);
		StartCoroutine(GetComponent<CrosshairScript>().getSet());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
