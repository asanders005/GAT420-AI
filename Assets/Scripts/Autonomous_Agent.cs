using UnityEngine;

public class Autonomous_Agent : AI_Agent
{
    [Header("Perception")]
    public Perception seekPerception;
    public Perception fleePerception;

    [Header("Wander")]
    [SerializeField] float distance;
    [SerializeField] float displacement;
    [SerializeField] float radius;

    float angle;

    private void Update()
    {
        //movement.ApplyForce(Vector3.forward * 10);

        //Debug.DrawRay(transform.position, transform.forward * perception.maxDistance, Color.green);

        // SEEK
        if (seekPerception != null)
        {
            var gameObjects = seekPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Seek(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }

        // FLEE
        if (fleePerception != null)
        {
            var gameObjects = fleePerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Flee(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }

        // WANDER
        if (movement.Acceleration.sqrMagnitude == 0)
        {
            angle += Random.Range(-displacement, displacement);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 point = rotation * (Vector3.forward * radius);

            Vector3 forward = movement.Direction * distance;
            Vector3 force = GetSteeringForce(forward + point);

            movement.ApplyForce(force);
        }

        //foreach (var go in gameObjects)
        //{
        //    Debug.DrawLine(transform.position, go.transform.position, Color.magenta);
        //}

        float size = 25;
        transform.position = Utilities.Wrap(transform.position, new Vector3(-size, -size, -size), new Vector3(size, size, size));
    }

    private Vector3 Seek(GameObject go)
    {
        Vector3 direction = go.transform.position - transform.position;
        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    private Vector3 Flee(GameObject go)
    {
        Vector3 direction = transform.position - go.transform.position;
        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    private Vector3 GetSteeringForce(Vector3 direction)
    {
        Vector3 desired = direction.normalized * movement.maxSpeed;
        Vector3 steer = desired - movement.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, movement.maxForce);

        return force;
    }
}
