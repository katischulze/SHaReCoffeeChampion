using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShareInput {
    

    /// <summary>
    /// Returns how much force is applied to the Share Device.
    /// </summary>
    /// <returns>Pressing Force</returns>
    float GetForce();

    /// <summary>
    /// Returns the maximum force that can be applied to the input device.
    /// </summary>
    float MaxForce();

    /// <summary>
    /// Returns the maximum force that was applied to the input device.
    /// </summary>
    /// <returns></returns>
    float MaxAppliedForce();

    /// <summary>
    /// Resets the MaxAppliedForce to 0.
    /// </summary>
    void ResetMaxAppliedForce();

    /// <summary>
    /// Returns the rotation of the Share Device.
    /// </summary>
    /// <returns>Share Device rotation</returns>
    Quaternion GetRotation();

    /// <summary>
    /// Returns the angle the Share Device is tilted
    /// </summary>
    /// <returns></returns>
    float GetTiltAngle();

    /// <summary>
    /// Returns the raw acceleration.
    /// </summary>
    /// <returns></returns>
    Vector3 GetAccelerationRaw();

    /// <summary>
    /// Returns true if the device is picked up.
    /// </summary>
    /// <returns></returns>
    bool IsPickedUp();
    
}
