using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterFunction : MonoBehaviour
{
    public List<InputActionReference> InputList;
    public List<UnityEvent> EventList;
    private List<InputAction> InputActionList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputActionList = new List<InputAction>();
        foreach (var item in InputList)
        {
            InputActionList.Add(item);
        }
        for (int i = 0; i < InputActionList.Count; i++)
        {
            InputActionList[i].Enable();
            InputActionList[i].performed += ToggleMenu;
        }
    }

    private void ToggleMenu(InputAction.CallbackContext obj)
    {
        int i = 0;
        foreach (var item in InputActionList)
        {
            if (item == obj.action)
            {
                print(i);
                EventList[i].Invoke();
                return;
            }
            i++;
        }
    }
}
