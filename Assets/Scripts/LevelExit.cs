using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1f;


   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int intnextSceneIndex = currentSceneIndex + 1;

        if (intnextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            intnextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(intnextSceneIndex);
    }
    

}

