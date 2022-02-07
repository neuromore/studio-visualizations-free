using System;
using System.Collections;
using System.Collections.Generic;
using extOSC;
using UnityEngine;

public class EXTOSCDataHandler : MonoBehaviour
{
    private OSCTransmitter oscTransmitter;
    private OSCReceiver oscReceiver;
    
    private void OnEnable()
    {
        oscTransmitter = GetComponent<OSCTransmitter>();
        oscReceiver = GetComponent<OSCReceiver>();
    }

    private void Start()
    {
        _init();
    }

    void _init()
    {
        oscReceiver.Bind("*", MessageRecieved);
    }
    

    protected void MessageRecieved(OSCMessage msg)
    {
        Debug.Log($"<color=green>message recieved Address : {msg.Address}</color>");

        if (msg.Values.Count < 1)
            return;
        // set ip adress
        if (String.Equals(msg.Address, OSCMapping.CONF_IP))
        {
            string ip = msg.Values[0].StringValue;
            if (!Networking.instance.tcpConnected)
            {
                Networking.instance.REMOTE_IP = ip;
                Networking.instance.CreateTCPSocket();
                return;
            }
        }

        if (!Networking.instance.tcpConnected)
            return;
        
        
        if (String.Equals (msg.Address, OSCMapping.SESSION_RUNNING)) {
            DataHolder.gIsRunning = msg.Values [0].BoolValue;
        }

        // heart rate
        else if (String.Equals (msg.Address, OSCMapping.BODYFEEDBACK_HR)) {
            DataHolder.gHeartRate = msg.Values [0].FloatValue;
        }
        // heart rate variability
        else if (String.Equals (msg.Address, OSCMapping.BODYFEEDBACK_HRV)) {
            DataHolder.gHRV = msg.Values [0].FloatValue;
        }
        // breathing rate
        else if (String.Equals (msg.Address, OSCMapping.BODYFEEDBACK_BR)) {
            DataHolder.gBreathingRate = msg.Values [0].FloatValue;
        }
        // custom feedback
        else if (DataHolder.gFeedbackValues.ContainsKey (msg.Address)) {
            DataHolder.gFeedbackValues[msg.Address] = msg.Values [0].FloatValue;
        }
    }

}
