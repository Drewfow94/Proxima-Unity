using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 2f;
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Starting":
                Debug.Log("This is the starting pad.");
                break;
            case "Finish":
                Debug.Log("This is the end.");
                Invoke("LoadLevel", loadLevelDelay);
                break;
            case "Fuel":
                Debug.Log("You've gathered fuel.");
                break;
            default:
                CrashSequence();
                break;            
        }
    }

    void CrashSequence()
    {
        // add SFX upon crash
        // add  particle fx on crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadLevelDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
