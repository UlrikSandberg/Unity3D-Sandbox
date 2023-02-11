using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private Movement playerMovement;

    [SerializeField] private float resetLevelDelay = 1.0f;
    [SerializeField] private float nextLevelDelay = 1.0f;
    [SerializeField] private AudioClip collision;
    [SerializeField] private AudioClip success;

    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private ParticleSystem collisionParticles;
    

    private AudioSource _audioSource;

    private bool isTransitioning = false;
    
    private void Start()
    {
        playerMovement = GetComponent<Movement>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning)
            return;
        
        switch (collision.gameObject.tag)
        {
            case "LaunchPad":
                Debug.Log("Standing on launchpad");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        isTransitioning = true;
        collisionParticles.Play();
        _audioSource.PlayOneShot(collision);
        playerMovement.enabled = false;
        Invoke("ReloadCurrentLevel", resetLevelDelay);
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;     
        successParticles.Play();
        playerMovement.enabled = false;
        _audioSource.PlayOneShot(success);
        Invoke("LoadNextLevel", nextLevelDelay);
    }
    
    private void ReloadCurrentLevel()
    {
        _audioSource.Stop();
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;   
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        var nextIndex = currentIndex == SceneManager.sceneCountInBuildSettings - 1
            ? 0
            : currentIndex + 1;
        SceneManager.LoadScene(nextIndex);
    }
}
