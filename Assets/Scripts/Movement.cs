using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - For tuning, typically set in the editor
    [SerializeField] float thrustSpeed = 350f;
    [SerializeField] float rotateSpeed = 350f;
    [SerializeField] AudioClip mainEngine; // Source: https://www.youtube.com/watch?v=KHl1CPL3K1A

    // CACHE - e.g. references for readability or speed
    Rigidbody playerRB;
    AudioSource playerAudio;

    // STATE - Private instance (member) variables
    bool turnFlag = false; // false = turning left; true = turning right

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            if (!playerAudio.isPlaying) {
                playerAudio.PlayOneShot(mainEngine);
            }
            playerRB.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        } else {
            playerAudio.Stop();
        }
    }

    void ProcessRotation() {
        if (Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow)) { // favors the most recent key press
            turnFlag = true;
        } else if (Input.GetKey(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow)) {
            turnFlag = false;
        }

        if (Input.GetKeyUp(KeyCode.D) | Input.GetKeyUp(KeyCode.RightArrow)) { // switch back to the held key if the recent one is released
            turnFlag = false;
        } else if (Input.GetKeyUp(KeyCode.A) | Input.GetKeyUp(KeyCode.LeftArrow)) {
            turnFlag = true;
        }

        if (!turnFlag && Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)) { // call ApplyRotation respective to the active turn key
            ApplyRotation(rotateSpeed);
        }
        else if (turnFlag && Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow)) {
            ApplyRotation(-rotateSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame) {
        playerRB.freezeRotation = true; // freezing the rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime); // Vector3.forward is shorthand for (0, 0, 1)
        playerRB.freezeRotation = false; // unfreeze the rotation so the physics can take back over
    }
}
