using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float health = 200f;
	public GameObject projectile;
	public float projectileSpeed;
	public float shotsPerSeconds;

	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if(missile){
			Debug.Log("Enemy Hit!");
			health -= missile.GetDamage();
			missile.Hit();
			if(health <= 0){
				Destroy(gameObject);
			}
		}
	}
	
	void Update(){
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability){
			Fire();
		}
	}
		
	void Fire(){
		Vector3 startPosition = transform.position + new Vector3(0, -.6f, 0);
		GameObject missle = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		missle.rigidbody2D.velocity = new Vector3(0, -projectileSpeed, 0);
	}
}
