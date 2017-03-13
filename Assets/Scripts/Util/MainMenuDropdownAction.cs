using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

[RequireComponent(typeof(Dropdown))]
public class MainMenuDropdownAction : MonoBehaviour {

    [SerializeField]
    private Enums.DropdownType dropdownType;
    private Dropdown dropdown;

    private void HandleSideEnum(Enums.State type)
    {
       GameManager.Instance.Settings.AIState = (type == Enums.State.Cross)? Enums.State.Nought : Enums.State.Cross;
    }

    private void HandleModeEnum(Enums.Complexity type)
    {
        GameManager.Instance.Settings.AIMode = type;
    }

    void Awake()
    {
        dropdown = (dropdown == null) ? GetComponent<Dropdown>() : dropdown;
        dropdown.ClearOptions();
        switch (dropdownType)
        {
            case Enums.DropdownType.SET_SIDE:
                var allNameState = Enum.GetNames(typeof(Enums.State)).ToList();
                dropdown.AddOptions(allNameState);
                dropdown.onValueChanged.AddListener((value) => HandleSideEnum((Enums.State)value));
                break;
            case Enums.DropdownType.SET_COMPLEXITY:
                var allNameComplexity = Enum.GetNames(typeof(Enums.Complexity)).ToList();
                dropdown.AddOptions(allNameComplexity);
                dropdown.onValueChanged.AddListener((value) => HandleModeEnum((Enums.Complexity)value));
                break;
            default:
                break;
        }
    }
}
