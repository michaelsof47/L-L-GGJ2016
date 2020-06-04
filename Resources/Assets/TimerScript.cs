using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	public float time, originTime;
	Slider slide;
	public EnemyMovementScript enemyScript;
	public CrosshairScript cross;
	// Use this for initialization
	void Start () {
		time = enemyScript.gameLevel * 10 + 10;
		originTime = time;
		slide = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		if(EnemyMovementScript.gameOn){
			time -= Time.deltaTime;
			slide.value = (float)time / originTime;
			if(slide.value <= 0.001f) {
				cross.lost();
			}
		}
			
	}
}
