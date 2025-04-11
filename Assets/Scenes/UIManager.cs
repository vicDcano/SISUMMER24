using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public SpringPhysics springPhysics; // Reference to the SpringPhysics script
    public Slider springConstantSlider;
    public Slider appliedForceSlider;
    public TMP_InputField springConstantInput;
    public TMP_InputField appliedForceInput;
    public TMP_Text displacementText;
    public TMP_Text springForceText;
    public TMP_Text appliedForceText;
    public TMP_Text equilibriumText;
    public Toggle showDisplacement;
    public Toggle showSpringForce;
    public Toggle showAppliedForce;
    public Toggle showEquilibrium;
    public Button resetButton;

    void Start()
    {
        // Initialize slider values
        springConstantSlider.value = springPhysics.springConstant;
        appliedForceSlider.value = springPhysics.appliedForce;
        springConstantInput.text = springConstantSlider.value.ToString();
        appliedForceInput.text = appliedForceSlider.value.ToString();

        // Add listeners for sliders
        springConstantSlider.onValueChanged.AddListener(UpdateSpringConstant);
        appliedForceSlider.onValueChanged.AddListener(UpdateAppliedForce);

        // Add listeners for input fields
        springConstantInput.onEndEdit.AddListener(UpdateSpringConstantFromInput);
        appliedForceInput.onEndEdit.AddListener(UpdateAppliedForceFromInput);

        // Add listeners for toggles
        showDisplacement.onValueChanged.AddListener(ToggleDisplacement);
        showSpringForce.onValueChanged.AddListener(ToggleSpringForce);
        showAppliedForce.onValueChanged.AddListener(ToggleAppliedForce);
        showEquilibrium.onValueChanged.AddListener(ToggleEquilibrium);

        // Add listener for reset button
        resetButton.onClick.AddListener(ResetSimulation);
    }

    void Update()
    {
        // Update live values
        if (showDisplacement.isOn)
            displacementText.text = $"Displacement: {springPhysics.GetDisplacement():F2} m";
        if (showSpringForce.isOn)
            springForceText.text = $"Spring Force: {springPhysics.GetSpringForce():F2} N";
        if (showAppliedForce.isOn)
            appliedForceText.text = $"Applied Force: {springPhysics.GetAppliedForce():F2} N";
        if (showEquilibrium.isOn)
            equilibriumText.text = $"Equilibrium: {springPhysics.GetEquilibriumPosition():F2} m";
    }

    void UpdateSpringConstant(float value)
    {
        springPhysics.SetSpringConstant(value);
        springConstantInput.text = value.ToString();
    }

    void UpdateAppliedForce(float value)
    {
        springPhysics.SetAppliedForce(value);
        appliedForceInput.text = value.ToString();
    }

    void UpdateSpringConstantFromInput(string value)
    {
        if (float.TryParse(value, out float result))
        {
            result = Mathf.Clamp(result, springConstantSlider.minValue, springConstantSlider.maxValue);
            springConstantSlider.value = result;
            springPhysics.SetSpringConstant(result);
        }
        else
        {
            springConstantInput.text = springConstantSlider.value.ToString();
            Debug.LogWarning("Invalid input for Spring Constant. Please enter a number.");
        }
    }

    void UpdateAppliedForceFromInput(string value)
    {
        if (float.TryParse(value, out float result))
        {
            result = Mathf.Clamp(result, appliedForceSlider.minValue, appliedForceSlider.maxValue);
            appliedForceSlider.value = result;
            springPhysics.SetAppliedForce(result);
        }
        else
        {
            appliedForceInput.text = appliedForceSlider.value.ToString();
            Debug.LogWarning("Invalid input for Applied Force. Please enter a number.");
        }
    }

    void ToggleDisplacement(bool isOn)
    {
        displacementText.gameObject.SetActive(isOn);
    }

    void ToggleSpringForce(bool isOn)
    {
        springForceText.gameObject.SetActive(isOn);
    }

    void ToggleAppliedForce(bool isOn)
    {
        appliedForceText.gameObject.SetActive(isOn);
    }

    void ToggleEquilibrium(bool isOn)
    {
        equilibriumText.gameObject.SetActive(isOn);
    }

    void ResetSimulation()
    {
        springPhysics.ResetSimulation();
        appliedForceSlider.value = 0;
        appliedForceInput.text = "0";
    }
}