using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSonarShader_KeyPressed : MonoBehaviour
{
    // All the renderers that will have the sonar data sent to their shaders.
    private Renderer[] ObjectRenderers;

    // Throwaway values to set position to at the start.
    private static readonly Vector4 GarbagePosition = new Vector4(-5000, -5000, -5000, -5000);

    // Make sure only 1 object initializes the queues.
    private static bool NeedToInitQueues = true;

    // Will call the SendSonarData for each object.
    //private delegate void Delegate();
    //private static Delegate RingDelegate;

    private void Start()
    {
        // Get renderers that will have effect applied to them
        ObjectRenderers = GetComponentsInChildren<Renderer>();

    }

    /// <summary>
    /// Starts a sonar ring from this position with the given intensity.
    /// </summary>
    public void StartSonarRing(Vector4 position)
    {
        // Put values into the queue
        position.w = Time.timeSinceLevelLoad;
        
    }

    void OnCollisionEnter(Collision collision)
    {   
        // Start sonar ring from the contact point
        if (Input.GetKeyDown(KeyCode.Space ))
        {
            print("apertado");
            StartSonarRing(collision.contacts[0].point);
        }
    }
  
}
