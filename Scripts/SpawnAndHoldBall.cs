using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class SpawnAndHoldBall : MonoBehaviour
{
    public GameObject ballPrefab;
    public InputActionReference activateAction;
    public XRDirectInteractor interactor;

    private GameObject spawnedBall = null;
    private XRGrabInteractable ballGrabInteractable;

    void Update()
    {
        if (activateAction.action != null && activateAction.action.ReadValue<float>() > 0.5f)
        {
            if (spawnedBall == null)
            {
                SpawnAndHold();
            }
        }
        else if (spawnedBall != null)
        {
            ReleaseBall();
        }
    }

    private void SpawnAndHold()
    {
        if (ballPrefab != null)
        {
            // Instantiate the ball at the interactor's attach transform
            spawnedBall = Instantiate(ballPrefab, interactor.attachTransform.position, interactor.attachTransform.rotation);
            ballGrabInteractable = spawnedBall.GetComponent<XRGrabInteractable>();

            if (ballGrabInteractable != null)
            {
                // Ensure proper interaction and physics behavior
                ballGrabInteractable.attachTransform = interactor.attachTransform;

                // Begin manual interaction
                interactor.StartManualInteraction(ballGrabInteractable);
            }
        }
        else
        {
            Debug.LogWarning("Ball Prefab is not assigned.");
        }
    }

    private void ReleaseBall()
    {
        if (spawnedBall != null && ballGrabInteractable != null)
        {
            ballGrabInteractable.interactionLayers = InteractionLayerMask.GetMask("ThrownBall");

            // End manual interaction to properly release the ball
            interactor.EndManualInteraction();

            // Cleanup
            ballGrabInteractable = null;
            spawnedBall = null;
        }
    }
}
