using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody rocketRB;
    AudioSource rocketAudioSource;
    int loadingTime = 2;

    bool isControlEnabled = true;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] float rotatationSpeed = 1f;
    [SerializeField] float thrustSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        rocketAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            Thrust();
            Rotate();
        }
    }
    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("Thrusting");
            rocketRB.AddRelativeForce(Vector3.up * thrustSpeed);

            if (!rocketAudioSource.isPlaying)
            {
                rocketAudioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            rocketAudioSource.Stop();
        }
    }
        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    print("No problem");
                    break;
                case "Finish":
                    print("You win!");
                Invoke("LoadNextScene", loadingTime);
                isControlEnabled = false;
                break;
                default:
                print("you lose");
                Invoke("LoadFirstLevel", loadingTime);
                isControlEnabled = false;
                break;
            }
        }

    void Rotate()
    {
        rocketRB.freezeRotation = true; //this says that we will take manual control of the rotation
        if (Input.GetKey(KeyCode.A))
        {
          //print("Rotating Left");
            transform.Rotate(Vector3.forward*rotatationSpeed);
        }
        else
            if (Input.GetKey(KeyCode.D))
        {
          //print("Rotating Right");
            transform.Rotate(-Vector3.forward*rotatationSpeed);
        }
        rocketRB.freezeRotation = false;
    }
    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }


}
