using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] TMP_Text victoryBanner;
    [SerializeField] TMP_Text loseBanner;

    [SerializeField] float loadLevelDelay = 2f;
    [SerializeField] float victoryDelay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip victorySound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem victoryParticles;
    
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    [SerializeField] bool cheatsEnabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DebugControls();
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) {return;}

        switch (other.gameObject.tag)
        {
            case "Starting":
                Debug.Log("This is the starting pad.");
                break;
            case "Finish":
                Debug.Log("This is the end.");
                SuccessSequence();
                break;
            default:
                CrashSequence();
                break;            
        }
    }


    void SuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        victoryParticles.Play();
        victoryBanner.text = "VICTORY";
        audioSource.PlayOneShot(victorySound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadLevel", victoryDelay);
    }

    void CrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        crashParticles.Play();
        loseBanner.text = "You Crashed :(";
        audioSource.PlayOneShot(crashSound);
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

    void DebugControls()
    {
        if(cheatsEnabled == true)
        {
            if(Input.GetKey("l"))
            {
                LoadLevel();
            }

            else if(Input.GetKey("c"))
            {
                collisionDisabled = !collisionDisabled;
            }
        }
    }
}
