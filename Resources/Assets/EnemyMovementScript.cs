using UnityEngine;
using System.Collections;

public class EnemyMovementScript : MonoBehaviour {

	public Animation awBubble;
	public Sprite enemyHurt;
	public static bool gameOn = true;
	public int gameLevel = 1;
	public bool inTrigger = false;
	public AudioSource[] sounds;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && inTrigger && gameOn) {

			//StartCoroutine(cross.animateRock());
			awBubble.Play();
			awBubble.gameObject.transform.position = transform.position + new Vector3(Random.Range(-1,1),1,0);
			StartCoroutine(ouch());
			//Debug.Log("ow");
			sounds[0].Play();
			sounds[1].Play();
		}
	}

	public IEnumerator randomMove() {
		while(gameOn) {
			yield return new WaitForSeconds(0.25f - gameLevel * 0.05f);
			float randomX = 99;
			Vector3 targetPos = new Vector3(99, 0, 0);
			while (targetPos.x > 8 || targetPos.x < -8) {
				randomX = Random.Range((float) -2 - gameLevel, (float) 3 + gameLevel);
				//randomY = Random.Range(-2,2);
				targetPos = transform.position + new Vector3(randomX, 0, 0);
				//Debug.Log(randomX);
			}

			float randomScale = Random.Range((float) 0.65f, (float) 1f);

			while(Vector3.Distance(targetPos, transform.position) > 1) {
				transform.position = Vector3.MoveTowards(transform.position, targetPos, (10 + gameLevel * 5) * Time.deltaTime);
				float newScale = Mathf.MoveTowards(transform.localScale.x, randomScale, 1 * Time.deltaTime);
				transform.localScale = new Vector3(newScale,newScale,newScale);
				yield return null;
			}
			yield return null;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player") inTrigger = true;
    }

	void OnTriggerExit2D(Collider2D col) {
		if(col.tag == "Player") inTrigger = false;
    }

	void OnTriggerStay2D(Collider2D col) {
		
	}

	IEnumerator ouch() {
		Sprite temp = GetComponent<SpriteRenderer>().sprite;
		GetComponent<SpriteRenderer>().sprite = enemyHurt;
		yield return new WaitForSeconds(0.5f);
		GetComponent<SpriteRenderer>().sprite = temp;
	}
}
