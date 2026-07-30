using UnityEngine;

public class MathGate : MonoBehaviour
{
    public GateManager gateManager;

    public OperationType operationType;
    public double operationValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gateManager.GateChosen(this);
        }
    }
}