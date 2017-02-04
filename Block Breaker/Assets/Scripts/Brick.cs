using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	public GameObject smoke;
	public Sprite[] hitSprites;
	public static int breakableCount = 0;
	public AudioClip crack;
	
	private bool isBreakable;
	private LevelManager levelManager;
	private int timesHit;

	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		if(isBreakable){
			breakableCount++;
		}
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		AudioSource.PlayClipAtPoint(crack, transform.position, 0.08f);
		if(isBreakable){
			HandleHits();
		}
	}
	
	void HandleHits(){
		timesHit++;
		if(timesHit >= (hitSprites.Length + 1))
		{
			breakableCount--;
			levelManager.BrickDestroyed();
			PuffSmoke();
			Destroy(gameObject);
		}
		else{
			LoadSprites();
		}
	}
	
	void PuffSmoke(){
		GameObject smokePuff = (GameObject) Instantiate(smoke, transform.position, Quaternion.identity);
		smokePuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	void LoadSprites(){
		int spriteIndex = timesHit - 1;
		if(hitSprites[spriteIndex]){
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}else{
			Debug.LogError("Missing Sprite at index: " + spriteIndex);
		}
	}
}
