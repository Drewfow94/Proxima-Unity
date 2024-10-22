using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for fine tuning
    // CACHE - object references
    // STATE - private instance variables
    Rigidbody rb;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationVelocity = 40f;
    [SerializeField] AudioClip engineThrust;
    [SerializeField] ParticleSystem thrusterOne;
    [SerializeField] ParticleSystem thrusterTwo;
    [SerializeField] ParticleSystem thrusterThree;

    AudioSource rocketThrustSound;
    [SerializeField] bool cheatsEnabled = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rocketThrustSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if (Input.GetKey("space"))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    void RotationControls()
    {
        if (Input.GetKey("a"))
        {
            RotateLeft();

        }
        else if (Input.GetKey("d"))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    void ThrustingSound()
    {
        if (!rocketThrustSound.isPlaying)
        {
            rocketThrustSound.PlayOneShot(engineThrust);
        }
    }

    void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!thrusterOne.isPlaying && !thrusterTwo.isPlaying && !thrusterThree.isPlaying)
        {
            thrusterOne.Play();
            thrusterTwo.Play();
            thrusterThree.Play();
        }
        ThrustingSound();
    }

    void StopThrust()
    {
        rocketThrustSound.Stop();
        thrusterOne.Stop();
        thrusterTwo.Stop();
        thrusterThree.Stop();
    }

    void ProcessRotation()
    {
        RotationControls();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationVelocity);
        if (!thrusterOne.isPlaying && !thrusterThree.isPlaying)
        {
            thrusterOne.Play();
            thrusterThree.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationVelocity);
        if (!thrusterTwo.isPlaying && !thrusterThree.isPlaying)
        {
            thrusterTwo.Play();
            thrusterThree.Play();
        }

    }

    void StopRotation()
    {
        thrusterOne.Stop();
        thrusterTwo.Stop();
        thrusterThree.Stop();
    }


    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing physics rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing physics rotation
    }

}
