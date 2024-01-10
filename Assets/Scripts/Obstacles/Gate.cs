using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public enum OperationType
    { 
        addition,
        difference,
        multiplication,
        division
    }

    [Header("Operation")]
    [SerializeField] private OperationType gateOperation;
    public OperationType GateOperation { get => gateOperation; }
    [SerializeField] private int value;

    [Header("References")]
    [SerializeField] private TextMeshPro operationText;
    [SerializeField] private MeshRenderer forceField;
    [SerializeField] private Material[] operationTypeMaterial;

    private void Awake()
    {
        AssignOperation();
    }
    private void AssignOperation()
    {
        string finalText = "";

        if (gateOperation == OperationType.addition)
            finalText += "+";
        if (gateOperation == OperationType.difference)
            finalText += "-";
        if (gateOperation == OperationType.multiplication)
            finalText += "x";
        if (gateOperation == OperationType.division)
            finalText += "÷";

        finalText += value.ToString();
        operationText.text = finalText;

        if (gateOperation == OperationType.addition || gateOperation == OperationType.multiplication)
            forceField.material = operationTypeMaterial[0];
        else
            forceField.material = operationTypeMaterial[1];
    }

    public void ExecuteOperation()
{
    switch (gateOperation)
    {
        case OperationType.addition:
            GameEvents.instance.playerSize.Value += value;
            AudioManager.instance.PlayPositiveSound();
            break;

        case OperationType.difference:
            GameEvents.instance.playerSize.Value -= value;
            AudioManager.instance.PlayBadSound();
            break;

        case OperationType.multiplication:
            GameEvents.instance.playerSize.Value *= value;
            AudioManager.instance.PlayPositiveSound();
            break;

        case OperationType.division:
            GameEvents.instance.playerSize.Value /= value;
            AudioManager.instance.PlayBadSound();
            break;

        default:
            break;
    }

    GetComponent<BoxCollider>().enabled = false;
    forceField.gameObject.SetActive(false);
}

}