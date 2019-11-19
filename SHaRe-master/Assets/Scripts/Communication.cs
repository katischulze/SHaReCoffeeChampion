using UnityEngine;
using System.Runtime.InteropServices;

public class Communication : MonoBehaviour
{

    //Import functions from the Dll for the USB communication
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)

    [DllImport("Communication")]
    public static extern void getPosition([In, Out] short[] data);

    [DllImport("Communication")]
    public static extern void StartSetup();

    [DllImport("Communication")]
    public static extern bool CheckIfPresentAndGetUSBDevicePath();

    [DllImport("Communication")]
    public static extern bool StopMicrocontroler();

#endif

    //Variable declaration
    private Quaternion outQuatOffset;
    private float forceOffset;

    public short[] dataIn;
    public Quaternion outQuat;
    public Quaternion rotQuat;
    public float force;
    public float ldr;
    public Vector3 RawAcceleration;
    

    //Functions
    void Awake () {
        DontDestroyOnLoad(this);
        //Set array size to 10:
        //  Data Structure:
        //  dataIn[0] -> Quaternion W
        //  dataIn[1] -> Quaternion X
        //  dataIn[2] -> Quaternion Y
        //  dataIn[3] -> Quaternion Z
        //  dataIn[4] -> Raw Acceleration X
        //  dataIn[5] -> Raw Acceleration Y
        //  dataIn[6] -> Raw Acceleration Z
        //  dataIn[7] -> Force
        //  dataIn[8] -> LDR
        //  dataIn[9] -> Internal counter for sync purposes
        dataIn = new short[10];

        //Initialize USB protocol
        StartSetup();

        //Read data from device once to offset values
        //Verifiy if MiWi dongle is attached to the computer
        if (CheckIfPresentAndGetUSBDevicePath())
        {
            //Get sensor data array -> short[] dataIn
            getPosition(dataIn);

            //Reconstruct Quaternion for Orientation
            short[] inQuat = { dataIn[0], dataIn[1], dataIn[2], dataIn[3] };
            outQuatOffset = placeQuat(inQuat);

            //Reads initial value of the force -> forceOffset
            forceOffset = dataIn[7];
        }

    }
	

	void Update () {
        //Verifiy if MiWi dongle is attached to the computer
        if (CheckIfPresentAndGetUSBDevicePath())
        {
            //Get sensor data array -> short[] dataIn
            getPosition(dataIn);

            //Reconstruct Quaternion for Orientation
            short[] inQuat = { dataIn[0], dataIn[1], dataIn[2], dataIn[3] };
            outQuat = placeQuat(inQuat);

            //Rotate Cube using the Quaternion from the device dataIn
            rotQuat = outQuat * Quaternion.Inverse(outQuatOffset);
            //this.transform.localRotation = rotQuat;
            //Returns a vector3 with the raw acceleration in the 3 axis (x,y,z);
            RawAcceleration = new Vector3(dataIn[4], dataIn[5], dataIn[6]);

            //Returns the force value in g.f [0,3500]
            force = (dataIn[7] - forceOffset) * (1000.0f / (600.0f));

            //Scale Cube using the force value
            float scaleFactor = 0.001f*force + 1;
            this.transform.localScale = Vector3.one * scaleFactor;

            //Return the ldr value (light sensor placed on the bottom surface)
            ldr = dataIn[8];
        }

    }

    void OnDestroy()
    {
        //Stop USB communication with the MiWi dongle
        StopMicrocontroler();
    }

    //Auxiliar Functions

    //Function: placeQuat
    //Description: Reorganize quaternion components due to specific IMU unit
    //Input: short[4] of the quaternion components from the device
    //Returns: Quaternion normalized and reordered to Unity axis
    public Quaternion placeQuat(short[] inQuat)
    {
        return new Quaternion((short)(inQuat[2]) / 16384.0f, (short)(inQuat[3]) / 16384.0f, (short)(-inQuat[1]) / 16384.0f, (short)(-inQuat[0]) / 16384.0f);
    }
}
