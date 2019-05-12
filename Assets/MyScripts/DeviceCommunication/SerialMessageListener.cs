/**
    Message listener to parse quaternion from IMU
 */

using UnityEngine;
using System.Collections;

public class SerialMessageListener : MonoBehaviour
{
    public GameObject elbowLink;
    public Vector3 elbowRotationOffset;

    public GameObject wristLink;
    public Vector3 wristRotationOffset;

    public Transform handOrientation;
    Quaternion correctedHand;
    Quaternion correctedWrist;
    Quaternion correctedElbow;

    float q_u_real, q_u_i, q_u_j, q_u_k; // Upper arm components
    float q_l_real, q_l_i, q_l_j, q_l_k; // Lower arm components

    int messageCounter = 0;

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        // Debug.Log("Values received: " + msg);
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
        
        Quaternion elbowLinkRotation = new Quaternion(q_u_i, q_u_j, q_u_k, q_u_real); // Unity Quaternion takes the arguments (x,y,z,w)
        Quaternion wristLinkRotation = new Quaternion(q_l_i, q_l_j, q_l_k, q_l_real);

        Quaternion convertedElbowQuaternion = MapToUnityCoordinateSystem(elbowLinkRotation);
        Quaternion convertedWristQuaternion = MapToUnityCoordinateSystem(wristLinkRotation);

        // if(messageCounter <1)
        // {
        //     calibrateHand(handOrientation.rotation, convertedWristQuaternion);
        //     calibrateIMUs(correctedHand, convertedElbowQuaternion);
        //     wristLink.transform.rotation = correctedWrist;
        //     elbowLink.transform.rotation = correctedElbow * Quaternion.Euler(elbowRotationOffset);
        // }

        elbowLink.transform.rotation = convertedElbowQuaternion.normalized * Quaternion.Euler(elbowRotationOffset);
        wristLink.transform.rotation = convertedWristQuaternion.normalized * Quaternion.Euler(wristRotationOffset);
  
        // messageCounter++;
    }

    Quaternion MapToUnityCoordinateSystem(Quaternion quat) {
        //elbowRotationOffset = new Vector3(0,-23.2f,0);
        return new Quaternion(
              quat.y,   
            - quat.z,   
            - quat.x,   
              quat.w
        );
    }

    void calibrateHand(Quaternion quatHand, Quaternion quatWrist)
    {
        // Work out the difference in yaw
        Quaternion orientationDifference = quatWrist * Quaternion.Inverse(quatHand); 

        // We'll average-out the two and meet in the middle:
        // correcting wrist heading halfway to elbow heading,
        // and elbow heading halfway to wrist heading.
        Quaternion hCorrectionRate = Quaternion.Lerp(Quaternion.identity, orientationDifference, 0.5f);
        Quaternion wCorrectionRate = Quaternion.Inverse(hCorrectionRate);

        //correctedHand = hCorrectionRate * wristLink.transform.rotation;
        correctedHand = wCorrectionRate * quatWrist;
    }

    void calibrateIMUs(Quaternion quatWrist, Quaternion quatElbow)
    {
        // Work out the difference in yaw
        Quaternion orientationDifference = quatElbow * Quaternion.Inverse(quatWrist); 

        // We'll average-out the two and meet in the middle:
        // correcting wrist heading halfway to elbow heading,
        // and elbow heading halfway to wrist heading.
        Quaternion wCorrectionRate = Quaternion.Lerp(Quaternion.identity, orientationDifference, 0.5f);
        Quaternion eCorrectionRate = Quaternion.Inverse(wCorrectionRate);

        correctedWrist = wCorrectionRate * quatWrist;
        correctedElbow = eCorrectionRate * quatElbow;
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
       // else
        // Debug.Log("Connection attempt failed or disconnection detected");
    }
}
