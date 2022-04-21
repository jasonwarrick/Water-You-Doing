using UnityEngine;
using UnityEngine.SceneManagement; // Allows us to use SceneManager

public class CollisionHandler : MonoBehaviour {
    int levelCap;
    int currentSceneIndex;
    [SerializeField] float reloadDelay = 1f;
    

    private void Start() {
        GetComponent<Movement>().enabled = true;
        levelCap = SceneManager.sceneCountInBuildSettings - 1; // Total number of scenes -1, to minimize variables and calculations
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void OnCollisionEnter(Collision other) { // Other is what we collided with
        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("Friendly hit");
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
        StopControl();
        Invoke("LoadNextLevel", reloadDelay);
    }

    void Crash() {
        StopControl();
        Invoke("ReloadLevel", reloadDelay);
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
