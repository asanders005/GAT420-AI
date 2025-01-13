using UnityEngine;

public class Autonomous_Agent : AI_Agent
{
    public Perception perception;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * perception.maxDistance, Color.green);

        var gameObjects = perception.GetGameObjects();
        foreach (var go in gameObjects)
        {
            Debug.DrawLine(transform.position, go.transform.position, Color.magenta);
        }
    }
}
