using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

public class BoxRestriction : XRGrabInteractable
{
    [Header("References")]
    public Rigidbody boxRb;
    public ActionBasedController controller;

    [Header("Pushing Variables")]
    public float pushForce = 100f;
    public float maxDistance = 5f;

    // Friction & Mass
    public float staticFrictionCoefficient = 0.5f;
    public float boxMass = 10f;
    public float gravity = 9.8f;

    // Sliders and UI
    [Header("UI Sliders & Text")]
    public Slider pushForceSlider;
    public TextMeshProUGUI pushForceText;

    public Slider staticFrictionSlider;
    public TextMeshProUGUI staticFrictionText;

    public Slider boxMassSlider;
    public TextMeshProUGUI boxMassText;

    public Slider maxDistanceSlider;
    public TextMeshProUGUI maxDistanceText;

    public TextMeshProUGUI velocityText;
    public TextMeshProUGUI normalForceText;
    public TextMeshProUGUI frictionForceText;
    public Button resetButton;

    [Header("Reset Settings")]
    public Vector3 resetPosition;
    public Vector3 resetRotation;

    // XR Input devices
    private InputDevice leftHandDevice;
    private InputDevice rightHandDevice;

    // Internal
    private bool isPushing = false;
    private Vector3 handPosition;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Transform interactorTransform;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Store the initial position and rotation of the object when grabbed
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Store the interactor's transform
        interactorTransform = args.interactorObject.transform;

        // Disable tracking of position and rotation
        trackPosition = false;
        trackRotation = false;

        Debug.Log("Box grabbed. Initial position: " + initialPosition);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Re-enable tracking (optional, if needed for other interactions)
        trackPosition = true;
        trackRotation = true;

        // Clear the interactor's transform
        interactorTransform = null;

        Debug.Log("Box released.");
    }

    void Start()
    {
        // Acquire devices
        leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Setup UI
        InitializeSliders();

        if (resetButton)
            resetButton.onClick.AddListener(ResetObjectPosition);

        if (resetPosition == Vector3.zero)
            resetPosition = transform.position;
        if (resetRotation == Vector3.zero)
            resetRotation = transform.eulerAngles;

        // Optionally set the box's mass on the Rigidbody
        if (boxRb != null)
            boxRb.mass = boxMass;

        // Display normal force once at start
        UpdateNormalForceDisplay();
    }

    void Update()
    {
        // Right-hand controller position
        handPosition = controller.transform.position;

        // Check A button on right-hand
        bool isAButtonPressed = false;
        if (rightHandDevice.isValid)
            rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isAButtonPressed);

        // Left-hand joystick
        Vector2 axisInput = Vector2.zero;
        if (leftHandDevice.isValid)
            leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out axisInput);

        // Condition for pushing
        if (isAButtonPressed
            && axisInput.sqrMagnitude > 0.01f
            && Vector3.Distance(handPosition, boxRb.position) < maxDistance)
        {
            isPushing = true;
        }
        else
        {
            isPushing = false;
        }

        // Display friction if A pressed
        if (isAButtonPressed)
        {
            DisplayFrictionForce();
        }
        else if (frictionForceText)
        {
            frictionForceText.text = "0.00 N";
        }

        // Velocity text
        if (velocityText != null)
        {
            float speed = boxRb.velocity.magnitude;
            velocityText.text = $"{speed:F2} m/s";
        }

        // Handle grabbing and pulling
        if (isSelected && interactorTransform != null)
        {
            // Calculate the interactor's position relative to the box's initial position
            Vector3 interactorOffset = interactorTransform.position - initialPosition;

            // Project the interactor's offset onto the box's local X-axis
            Vector3 localXAxis = transform.right; // The box's local X-axis
            float movementAmount = Vector3.Dot(interactorOffset, localXAxis);

            // Increase movement sensitivity (adjust the multiplier as needed)
            movementAmount *= 2; // This makes the box move more responsively

            Debug.Log("Interactor offset: " + interactorOffset + ", Movement amount: " + movementAmount);

            // Move the box along its local X-axis using Transform.Translate
            transform.Translate(Vector3.right * movementAmount * Time.deltaTime, Space.Self);

            // Lock the Y and Z positions to the initial position
            Vector3 newPosition = transform.position;
            newPosition.y = initialPosition.y;
            newPosition.z = initialPosition.z;
            transform.position = newPosition;

            // Lock rotation to the initial rotation
            transform.rotation = initialRotation;
        }
    }

    void FixedUpdate()
    {
        if (isPushing)
        {
            ApplyPushForce();
        }
        // otherwise the box glides by inertia
    }

    /// <summary>
    /// Apply push force, with an extra multiplier if staticFrictionCoefficient=0 => "frictionless."
    /// </summary>
    void ApplyPushForce()
    {
        Vector2 axisInput = Vector2.zero;
        if (leftHandDevice.isValid)
            leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out axisInput);

        if (axisInput.sqrMagnitude > 0.01f)
        {
            // forward direction from right-hand
            Vector3 localDirection = new Vector3(axisInput.x, 0, axisInput.y).normalized;
            Vector3 pushDirection = controller.transform.TransformDirection(localDirection);

            // If static friction is zero => big acceleration
            if (Mathf.Approximately(staticFrictionCoefficient, 0f))
            {
                // e.g., doubling pushForce to mimic frictionless acceleration
                float frictionlessPush = pushForce * 2f;
                boxRb.AddForce(pushDirection * frictionlessPush, ForceMode.Force);
            }
            else
            {
                // normal approach
                boxRb.AddForce(pushDirection * pushForce, ForceMode.Force);
            }
        }
    }

    // ---------- UI & Sliders ---------- //
    private void InitializeSliders()
    {
        // push force
        if (pushForceSlider)
        {
            pushForceSlider.minValue = 0f;
            pushForceSlider.maxValue = 200f;
            pushForceSlider.value = pushForce;
            pushForceSlider.onValueChanged.AddListener(UpdatePushForce);
            UpdatePushForce(pushForce);
        }

        // static friction
        if (staticFrictionSlider)
        {
            staticFrictionSlider.minValue = 0f;
            staticFrictionSlider.maxValue = 1f;
            staticFrictionSlider.value = staticFrictionCoefficient;
            staticFrictionSlider.onValueChanged.AddListener(UpdateStaticFriction);
            UpdateStaticFriction(staticFrictionCoefficient);
        }

        // box mass
        if (boxMassSlider)
        {
            boxMassSlider.minValue = 0f;
            boxMassSlider.maxValue = 50f;
            boxMassSlider.value = boxMass;
            boxMassSlider.onValueChanged.AddListener(UpdateBoxMass);
            UpdateBoxMass(boxMass);
        }

        // max distance
        if (maxDistanceSlider)
        {
            maxDistanceSlider.minValue = 0f;
            maxDistanceSlider.maxValue = 50f;
            maxDistanceSlider.value = maxDistance;
            maxDistanceSlider.onValueChanged.AddListener(UpdateMaxDistance);
            UpdateMaxDistance(maxDistance);
        }
    }

    public void UpdatePushForce(float newPushForce)
    {
        pushForce = newPushForce;
        if (pushForceText != null)
            pushForceText.text = $"{pushForce:F1}";
    }

    public void UpdateStaticFriction(float newFriction)
    {
        staticFrictionCoefficient = newFriction;
        if (staticFrictionText != null)
            staticFrictionText.text = $"{staticFrictionCoefficient:F2}";

        UpdateNormalForceDisplay();
    }

    public void UpdateBoxMass(float newMass)
    {
        boxMass = newMass;
        if (boxMassText != null)
            boxMassText.text = $"{boxMass:F1}";

        if (boxRb != null)
            boxRb.mass = boxMass;

        UpdateNormalForceDisplay();
    }

    public void UpdateMaxDistance(float newDistance)
    {
        maxDistance = newDistance;
        if (maxDistanceText != null)
            maxDistanceText.text = $"{maxDistance:F1}";
    }

    // ---------- Normal Force Display ---------- //
    private void UpdateNormalForceDisplay()
    {
        float normalForce = boxMass * gravity;
        if (normalForceText != null)
            normalForceText.text = $"{normalForce:F2} N (Normal)";
    }

    // ---------- Display Static Friction Force If "A" Pressed ---------- //
    private void DisplayFrictionForce()
    {
        if (!frictionForceText) return;

        float normalForce = boxMass * gravity;
        float frictionForce = staticFrictionCoefficient * normalForce;

        frictionForceText.text = $"{frictionForce:F2} N";
    }

    // ---------- Reset ---------- //
    public void ResetObjectPosition()
    {
        boxRb.position = resetPosition;
        boxRb.rotation = Quaternion.Euler(resetRotation);
        boxRb.velocity = Vector3.zero;
        boxRb.angularVelocity = Vector3.zero;

        if (velocityText != null)
            velocityText.text = "0.00 m/s";

        if (frictionForceText != null)
            frictionForceText.text = "0.00 N";
    }
}