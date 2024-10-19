using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float velocityX = 0f;
    [SerializeField] float velocityY = 10f;
    [SerializeField] float velocityZ = 0f;
    [SerializeField] float rotationVelocity = 40f;

    AudioSource rocketThrustSound;
    
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
            Debug.Log("Pressed space - thrusters active");
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!rocketThrustSound.isPlaying)
            {
                rocketThrustSound.Play();
            }
            else 
            {
                rocketThrustSound.Stop();
            }
        }
        
    }

    void ProcessRotation()
    {
        if (Input.GetKey("a"))
        {
            ApplyRotation(rotationVelocity);
        }
        else if (Input.GetKey("d"))
        {
            ApplyRotation(-rotationVelocity);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing physics rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing physics rotation
    }
}
