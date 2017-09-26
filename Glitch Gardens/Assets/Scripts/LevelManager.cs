using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public float autoLoadNextLevelAfter;

    void Start()
    {
		print ("Level Manger Starting...");
		Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
    }

    public void LoadLevel(string name){
		print ("New Level load: " + name);
		SceneManager.LoadScene(name);
	}

	public void QuitRequest(){
		print ("Quit requested");
		Application.Quit ();
	}

    public void LoadNextLevel()
    {
		print ("Loading next level");
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		SceneManager.LoadScene(nextSceneIndex);
    }

}
