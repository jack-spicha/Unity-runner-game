using UnityEngine;
using TMPro;
using System.Collections.Generic;

public enum OperationType
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Power
}

[System.Serializable]
public class MathOperation
{
    public OperationType type;
    public double value;
}

public class GateManager : MonoBehaviour
{
    public Transform player;
    public GameManager gameManager;

    public GameObject gatePrefab;
    public Transform gateParent;

    public float maxX = 5f;
    public float gateY = 1f;
    public float deleteDistanceBehindPlayer = 10f;

    public MathOperation[] possibleOperations;

    private List<GameObject> gateRows = new List<GameObject>();

    private void Update()
    {
        if (!gameManager.gameRunning)
        {
            return;
        }

        // Delete gate rows that the player has passed
        for (int i = gateRows.Count - 1; i >= 0; i--)
        {
            GameObject row = gateRows[i];

            if (row == null)
            {
                gateRows.RemoveAt(i);
                continue;
            }

            if (row.transform.position.z <
                player.position.z - deleteDistanceBehindPlayer)
            {
                Destroy(row);
                gateRows.RemoveAt(i);
            }
        }
    }

    public void SpawnGateRowAt(float zPosition)
    {
        GameObject row = new GameObject("GateRow");

        row.transform.position = new Vector3(
            0f,
            0f,
            zPosition
        );

        row.transform.SetParent(gateParent);

        gateRows.Add(row);

        MathOperation[] operations = ChooseOperations();

        float[] xPositions =
        {
            -maxX,
            0f,
            maxX
        };

        for (int i = 0; i < 3; i++)
        {
            Vector3 position = new Vector3(
                xPositions[i],
                gateY,
                zPosition
            );

            GameObject gate = Instantiate(
                gatePrefab,
                position,
                Quaternion.identity,
                row.transform
            );

            MathGate mathGate = gate.GetComponent<MathGate>();

            mathGate.gateManager = this;
            mathGate.operationType = operations[i].type;
            mathGate.operationValue = operations[i].value;

            TMP_Text text = gate.GetComponentInChildren<TMP_Text>();

            if (text != null)
            {
                text.text = GetOperationText(operations[i]);
            }
        }
    }

    private MathOperation[] ChooseOperations()
    {
        List<MathOperation> beneficial = new List<MathOperation>();
        List<MathOperation> harmful = new List<MathOperation>();
        List<MathOperation> valid = new List<MathOperation>();

        foreach (MathOperation operation in possibleOperations)
        {
            if (!OperationIsAllowed(operation))
            {
                continue;
            }

            valid.Add(operation);

            double result = CalculateResult(
                gameManager.score,
                operation.type,
                operation.value
            );

            if (result > gameManager.score)
            {
                beneficial.Add(operation);
            }
            else if (result < gameManager.score)
            {
                harmful.Add(operation);
            }
        }

        MathOperation[] chosen = new MathOperation[3];

        // Guarantee at least one good operation
        chosen[0] = beneficial[
            Random.Range(0, beneficial.Count)
        ];

        // Guarantee at least one bad operation
        chosen[1] = harmful[
            Random.Range(0, harmful.Count)
        ];

        // Third can be any valid operation
        chosen[2] = valid[
            Random.Range(0, valid.Count)
        ];

        Shuffle(chosen);

        return chosen;
    }

    private bool OperationIsAllowed(MathOperation operation)
    {
        // × -1 only appears if the current score is negative
        if (operation.type == OperationType.Multiply &&
            operation.value == -1 &&
            gameManager.score >= 0)
        {
            return false;
        }

        // Never allow divide by zero
        if (operation.type == OperationType.Divide &&
            operation.value == 0)
        {
            return false;
        }

        return true;
    }

    private double CalculateResult(
        double score,
        OperationType type,
        double value
    )
    {
        switch (type)
        {
            case OperationType.Add:
                return score + value;

            case OperationType.Subtract:
                return score - value;

            case OperationType.Multiply:
                return score * value;

            case OperationType.Divide:
                return score / value;

            case OperationType.Power:
                return System.Math.Pow(score, value);
        }

        return score;
    }

    private string GetOperationText(MathOperation operation)
    {
        switch (operation.type)
        {
            case OperationType.Add:
                return "+" + operation.value;

            case OperationType.Subtract:
                return "-" + operation.value;

            case OperationType.Multiply:
                return "×" + operation.value;

            case OperationType.Divide:
                return "÷" + operation.value;

            case OperationType.Power:
                return "^" + operation.value;
        }

        return "";
    }

    private void Shuffle(MathOperation[] operations)
    {
        for (int i = 0; i < operations.Length; i++)
        {
            int randomIndex = Random.Range(
                i,
                operations.Length
            );

            MathOperation temp = operations[i];

            operations[i] = operations[randomIndex];
            operations[randomIndex] = temp;
        }
    }

    public void GateChosen(MathGate chosenGate)
    {
        GameObject row = chosenGate.transform.parent.gameObject;

        double newScore = CalculateResult(
            gameManager.score,
            chosenGate.operationType,
            chosenGate.operationValue
        );

        gameManager.SetScore(newScore);

        // Disable all gate colliders in this row
        MathGate[] gates = row.GetComponentsInChildren<MathGate>();

        foreach (MathGate gate in gates)
        {
            Collider gateCollider = gate.GetComponent<Collider>();

            if (gateCollider != null)
            {
                gateCollider.enabled = false;
            }
        }
    }
}