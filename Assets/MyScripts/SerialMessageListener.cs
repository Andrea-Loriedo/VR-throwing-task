/**
    Message listener to parse quaternion from IMU
 */

using UnityEngine;
using System.Collections;

/**
 * When creating your message listeners you need to implement these two methods:
 *  - OnMessageArrived
 *  - OnConnectionEvent
 */
public class SerialMessageListener : MonoBehaviour
{
    public GameObject upperArm;
    public GameObject lowerArm;

    float q_u_real, q_u_i, q_u_j, q_u_k; // Upper arm components
    float q_l_real, q_l_i, q_l_j, q_l_k; // Lower arm components

    Quaternion upperArmRotation;
    Quaternion lowerArmRotation;

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        Debug.Log("Values received: " + msg);
        // parse the message to obtain x, y, z
        string values = msg; // Read the serial message
        string[] quat = values.Split(','); // Separate values

        for(int i = 0; i < 9; i++){
            if(quat[i] != "") //Check if all values are recieved
            {
                q_u_real = float.Parse(quat[i++]);
                q_u_i = float.Parse(quat[i++]);
                q_u_j = float.Parse(quat[i++]);
                q_u_k = float.Parse(quat[i++]);

                q_l_real = float.Parse(quat[i++]);
                q_l_i = float.Parse(quat[i++]);
                q_l_j = float.Parse(quat[i++]);
                q_l_k = float.Parse(quat[i++]);
            }
        }

        upperArmRotation = new Quaternion(q_u_real, q_u_i, q_u_j, q_u_k);
        lowerArmRotation = new Quaternion(q_l_real, q_l_i, q_l_j, q_l_k);
        
        upperArm.transform.rotation = upperArmRotation;
        lowerArm.transform.rotation = lowerArmRotation;
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}