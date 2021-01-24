using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.XR;

public class InputListener : MonoBehaviour
{
    List<InputDevice> inputDevices;  

    InputDeviceCharacteristics deviceCharacteristics;
    private void Awake()
    {
        inputDevices = new List<InputDevice>();
    }

    // Start is called before the first frame update
    void Start()
    {
        deviceCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(deviceCharacteristics, inputDevices);
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (InputDevice device in inputDevices)
        //{
        //}
        
    }
}
