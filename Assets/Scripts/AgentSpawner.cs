using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    [SerializeField] AI_Agent[] agents;
    [SerializeField] LayerMask layerMask;

    int agentIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) agentIndex = ++agentIndex % agents.Length;

        if ((Input.GetButton("Fire1") && Input.GetKey(KeyCode.Space)) || Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
            {
                Instantiate(agents[agentIndex], hitInfo.point, Quaternion.identity);
            }
        }
    }
}
