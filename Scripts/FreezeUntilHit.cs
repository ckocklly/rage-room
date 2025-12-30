using UnityEngine;

public class FreezeUntilHit : MonoBehaviour
{
    public float minSpeed = 5f; // The minimum speed the ball must have to unfreeze parts

    private Rigidbody[] parts;
    private Light pointLight;

    private void Start()
    {
        // Get all Rigidbody components in the lamp parts
        parts = GetComponentsInChildren<Rigidbody>();

        // Find the Point Light component
        pointLight = GetComponentInChildren<Light>();

        // Freeze all parts by disabling gravity and freezing position/rotation
        foreach (Rigidbody part in parts)
        {
            part.isKinematic = true;

            // Ensure each part has a collider and add this script to its GameObject
            if (part.GetComponent<Collider>() != null)
            {
                part.gameObject.AddComponent<ChildCollisionHandler>().Initialize(this);
            }
        }
    }

    public void UnfreezeParts()
    {
        foreach (Rigidbody part in parts)
        {
            part.isKinematic = false;
        }

        // Turn off the Point Light
        if (pointLight != null)
        {
            pointLight.enabled = false;
        }
    }
}

public class ChildCollisionHandler : MonoBehaviour
{
    private FreezeUntilHit parentScript;

    public void Initialize(FreezeUntilHit script)
    {
        parentScript = script;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null && rb.velocity.magnitude >= parentScript.minSpeed && collision.gameObject.CompareTag("Ball"))
        {
            parentScript.UnfreezeParts();
        }
    }
}