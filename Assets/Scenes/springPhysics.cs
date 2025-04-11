using UnityEngine;
public class SpringPhysics : MonoBehaviour
{
    public Transform wall;
    public LineRenderer springLine;
    public float springConstant = 200f;
    public float appliedForce = 0f;
    public float equilibriumPosition = 2f;
    public float damping = 0.5f;

    private Rigidbody rb;
    private Vector3 displacement;
    private float springForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(equilibriumPosition, 0, 0);
        
    }
    void FixedUpdate()
    {
        displacement = transform.position - new Vector3(equilibriumPosition, 0, 0);
        float x = displacement.x;

        springForce = -springConstant * x;

        Vector3 velocity = rb.velocity;
        float dampingForce = -damping * velocity.x;
        float totalForce = springForce + appliedForce + dampingForce;
        rb.AddForce(new Vector3(totalForce, 0, 0));

        UpdateSpringVisual();


    }
    void UpdateSpringVisual()
    {
        springLine.SetPosition(0, wall.position);
        springLine.SetPosition(1, transform.position);

    }
    public void SetSpringConstant(float k)
    {
        springConstant = k;
    }
    public void SetAppliedForce(float force)
    {
        appliedForce = force;
    }
    public float GetDisplacement()
    {
        return displacement.x;
    }
    public float GetSpringForce()
    {
        return springForce;
    }
    public float GetAppliedForce()
    {
        return appliedForce;
    }
    public float GetEquilibriumPosition()
    {
        return equilibriumPosition;
    }


    public void ResetSimulation()
    {
        transform.position = new Vector3(equilibriumPosition, 0, 0);
        rb.velocity = Vector3.zero;
        appliedForce = 0f;
    }
}