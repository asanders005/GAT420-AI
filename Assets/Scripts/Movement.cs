using Unity.VisualScripting;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] public float maxSpeed = 5;
    [SerializeField] public float minSpeed = 5;
    [SerializeField] public float maxForce = 5;

    public Vector3 Velocity { get; set; }
    public Vector3 Acceleration { get; set; }
    public Vector3 Direction { get { return Velocity.normalized; } }

    public abstract void ApplyForce(Vector3 force);
}
