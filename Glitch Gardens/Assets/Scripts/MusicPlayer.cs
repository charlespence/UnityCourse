using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public AudioClip[] levelMusicArray;

	private AudioSource audioSource;

	void Awake(){
		GameObject.DontDestroyOnLoad(gameObject);
	}

	void Start () {
		// Find and get the AudioSource
		audioSource = GetComponent<AudioSource> ();

		// Event Registration
		SceneManager.sceneLoaded -= OnSceneLoaded;
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode){
		
		AudioClip currentLevelMusic = levelMusicArray [scene.buildIndex];

		if (currentLevelMusic != null) {
			audioSource.clip = currentLevelMusic;
			audioSource.loop = true;
			audioSource.Play ();
		} else {
			print ("Level has no music specified. skipping");
		}
	}
}
