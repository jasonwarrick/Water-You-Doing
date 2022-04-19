using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) { // other is what we collided with
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
                Debug.Log("Object Hit");
                break;
        }
    }
}
