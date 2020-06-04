using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrosshairScript : MonoBehaviour {

	public EnemyMovementScript enemyScript;
	public GameObject cross, rock, panel, currentRock;
	public Animation slingAnim;
	public GameObject winPanel, losePanel, readyText;
	public GameObject nextButton, restartButton;
	public bool tutorial;
	// Use this for initialization
	void Start () {
		EnemyMovementScript.gameOn = false;
		if(!tutorial) StartCoroutine(getSet());
	}

	public IEnumerator getSet(){
		readyText.SetActive(true);
		yield return new WaitForSeconds(2);
		readyText.SetActive(false);
		EnemyMovementScript.gameOn = true;
		cross.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(40 + (enemyScript.gameLevel - 1) * 50,60 + (enemyScript.gameLevel - 1) * 50),
			Random.Range(-10 - (enemyScript.gameLevel - 1) * 50, -50 - (enemyScript.gameLevel - 1) * 50)));
		StartCoroutine(enemyScript.randomMove());
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(cross.GetComponent<Rigidbody2D>().velocity);

		/*Vector3 mousePos = Input.mousePosition;
		mousePos.z = 10;
		Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
		cross.transform.position = newPos;*/

		if(Input.GetMouseButtonDown(0) && EnemyMovementScript.gameOn) {
		Debug.Log(enemyScript.inTrigger);
			//Debug.Log(panel.transform.childCount );
			slingAnim.Play();
			if(panel.transform.childCount > 0) {
				StartCoroutine(animateRock(panel.transform.GetChild(0).transform.position, false, enemyScript.inTrigger));
				Transform firstChild = panel.transform.GetChild(0);
				Destroy(firstChild.gameObject);
			}
			else {
				StartCoroutine(animateRock(new Vector3(100,0,0), true, enemyScript.inTrigger));
			}
		}
	}

	public IEnumerator animateRock(Vector3 rockUIpos, bool changeLevel, bool inTrigger) {
		Vector3 pos = cross.transform.position;
		GameObject newRock = GameObject.Instantiate(rock) as GameObject;
		newRock.transform.position = rockUIpos;
		newRock.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
		Vector3 originRockPos = currentRock.transform.position;
		while(Vector3.Distance(currentRock.transform.position, pos) > 0.5f) {
			currentRock.transform.position = Vector3.MoveTowards(currentRock.transform.position, pos, 24 * Time.deltaTime);
			float newScale = Mathf.MoveTowards(currentRock.transform.localScale.x, 0, 6 * Time.deltaTime);
			currentRock.transform.localScale = new Vector3(newScale,newScale,newScale);
			yield return null;
		}
		Destroy(currentRock);
		currentRock = newRock;

		if(!inTrigger) {
			lost();
		}
		else {
			if(!changeLevel) {
				while(Vector3.Distance(newRock.transform.position, originRockPos) > 0.5f) {
					newRock.transform.position = Vector3.MoveTowards(newRock.transform.position, originRockPos, 120 * Time.deltaTime);
					yield return null;
				}
				newRock.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
			}
			else {
				win();
			}
		}
	}

	public void nextLevel(){
		if(enemyScript.gameLevel < 3) {
			SceneManager.LoadScene(enemyScript.gameLevel + 2);
		}
		else 
			SceneManager.LoadScene(0);
	}

	public void lost() {
		EnemyMovementScript.gameOn = false;
		cross.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		losePanel.SetActive(true);
	}

	public void win() {
		EnemyMovementScript.gameOn = false;
			cross.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		if(enemyScript.gameLevel < 3) {
			winPanel.SetActive(true);
		}
		else {
			restartButton.SetActive(true);
			nextButton.SetActive(false);
			winPanel.SetActive(true);
		}
	}
}
