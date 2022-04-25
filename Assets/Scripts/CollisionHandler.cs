using UnityEngine;
using UnityEngine.SceneManagement; // Allows us to use SceneManager

public class CollisionHandler : MonoBehaviour {
    [SerializeField] float hitDelay = 1f;
    [SerializeField] float winDelay = 1.5f;
    [SerializeField] AudioClip hitAudio; // Source: https://mixkit.co/free-sound-effects/thud/ (Falling on Metal Roof)
    [SerializeField] AudioClip winAudio; // Source: https://pixabay.com/sound-effects/tada-fanfare-a-6313/

    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] ParticleSystem winParticles;

    AudioSource playerAudio;
    
    int levelCap;
    int currentSceneIndex;
    bool isTransitioning;

    private void Start() {
        isTransitioning = false;
        GetComponent<Movement>().enabled = true;
        levelCap = SceneManager.sceneCountInBuildSettings - 1; // Total number of scenes -1, to minimize variables and calculations
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerAudio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) { // Other is what we collided with
        if (isTransitioning) { return; }

        switch (other.gameObject.tag) {
            case "Friendly":
                break;
            
            case "Finish":
                Finish();
                break;
            
            default:
                Crash();
                break;
        }
    }

    void Finish() {
        winParticles.Play();
        playerAudio.Stop();
        playerAudio.PlayOneShot(winAudio);
        StopControl();
        Invoke("LoadNextLevel", winDelay);
        isTransitioning = true;
    }

    void Crash() {
        hitParticles.Play();
        playerAudio.Stop();
        playerAudio.PlayOneShot(hitAudio);
        StopControl();
        Invoke("ReloadLevel", hitDelay);
        isTransitioning = true;
    }

    void StopControl() {
        GetComponent<Movement>().enabled = false;
    }

    void LoadNextLevel() {
        if (currentSceneIndex >= levelCap) {
            SceneManager.LoadScene(0); // The parameter just finds the current scene, and the method reloads it
        } else {
            SceneManager.LoadScene(currentSceneIndex + 1); // The parameter just finds the current scene, and the method reloads it
        }
    }

    void ReloadLevel() {
        SceneManager.LoadScene(currentSceneIndex); // The parameter just finds the current scene, and the method reloads it
    }
}
