using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomContinuousMoveProvider : ContinuousMoveProviderBase
{
    [SerializeField]
    private InputActionProperty leftHandMoveAction;

    private Rigidbody playerRigidbody;

    protected override void Awake()
    {
        base.Awake();
        playerRigidbody = GetComponent<Rigidbody>();
        if (playerRigidbody == null)
        {
            Debug.LogError("Rigidbody component is required on the XR Rig.");
        }
    }

    protected override Vector2 ReadInput()
    {
        return new Vector2(0, 0);
    }

    private void FixedUpdate()
    {
        if (leftHandMoveAction.action == null || playerRigidbody == null)
            return;

        Vector2 input = leftHandMoveAction.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            moveDirection = transform.TransformDirection(moveDirection);
            Vector3 targetPosition = playerRigidbody.position + moveDirection * (moveSpeed * Time.fixedDeltaTime);
            playerRigidbody.MovePosition(targetPosition);
        }
    }
}
