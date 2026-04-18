using System.Collections.Generic;
using UnityEngine.InputSystem;


public static class InputDeviceHelper
{
//Assign ps4 controllers for each player based on their control scheme
    public static void AssignPlayerDevices(InputSystem_Actions inputActions, string controlScheme)
    {
        List<InputDevice> myDevices = new List<InputDevice>();
        
        //Adding keyboard in default
        if (Keyboard.current != null)
        {
            myDevices.Add(Keyboard.current);
        }

        //Adding controllers based on the given control scheme
        if (Gamepad.all.Count > 0)
        {
            if (controlScheme == "Player1")
            {
                myDevices.Add(Gamepad.all[0]);
            }
            else if (controlScheme == "Player2" && Gamepad.all.Count > 1)
            {
                myDevices.Add(Gamepad.all[1]);
            }
        }
        
        //Update the devices on the new input system
        if (myDevices.Count > 0)
        {
            inputActions.devices = myDevices.ToArray();
        }
    }
}