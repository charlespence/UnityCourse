using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {
	
	public GameObject enemyPrefab;
	public float speed = 5.0f;
	public float width = 5f;
	public float height = 2.75f;
	public float spawnDelay = 0.5f;
	
	float xMin;
	float xMax;
	bool movingRight = true;


	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundry = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightBoundry = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xMin = leftBoundry.x;
		xMax = rightBoundry.x;
		SpawnUntilFull ();
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	void Update(){
		MoveLaterally();
	}
	
	void MoveLaterally(){
	
		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);
		if(rightEdgeOfFormation >= xMax){
			movingRight = false;
		}
		else if(leftEdgeOfFormation <= xMin){
			movingRight = true;
		}
	
		if(movingRight){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else{
			transform.position += Vector3.left * speed * Time.deltaTime;	
		}
		
		if(AllMembersAreDead()){
			Debug.Log("Empty Formation");
			SpawnUntilFull();
		}
	}
	
	bool AllMembersAreDead(){
		foreach (Transform child in transform) {
			if(child.childCount > 0){
				return false;
			}
		}
		return true;
	}
	
	Transform NextFreePosition(){
		foreach (Transform child in transform) {
			if(child.childCount == 0){
				return child;
			}
		}
		return null;
	}

	void SpawnUntilFull(){
		Transform nextFreePosition = NextFreePosition();
		if(nextFreePosition){
			GameObject enemy = Instantiate (enemyPrefab, nextFreePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = nextFreePosition;
		}
		
		if(NextFreePosition()){
			Invoke("SpawnUntilFull", spawnDelay);
		}
	}

	void SpawnEnemies ()
	{
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
}
