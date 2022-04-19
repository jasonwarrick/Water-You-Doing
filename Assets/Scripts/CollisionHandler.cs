using UnityEngine;
using UnityEngine.SceneManagement; // Allows us to use SceneManager

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) { // Other is what we collided with
        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("Friendly hit");
                break;
            
            case "Fuel":
                Debug.Log("Fueled");
                break;
            
            case "Finish":
                Debug.Log("You won!");
                break;
            
            default:
                ObstacleHit();
                break;
        }
    }

    void ObstacleHit() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); // The parameter just finds the current scene, and the method reloads it
    }
}
