using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities {

    /// <summary>
    /// Sets bottomPosition to the bottom mesh position of gameObejct, if a mesh is found on the gameObejct and returns true.
    /// If no mesh is found, false is returned and bottomPosition is set to Vector3.zero.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="bottomPosition"></param>
    /// <returns>True if position was found</returns>
	public static bool GetBottomPosition(GameObject gameObject, out Vector3 bottomPosition)
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if(meshFilter != null)
        {
            bottomPosition = gameObject.transform.position;
            bottomPosition.y = gameObject.transform.position.y - meshFilter.mesh.bounds.extents.y * gameObject.transform.localScale.y;
            return true;
        }
        bottomPosition = Vector3.zero;
        return false;
    }

    /// <summary>
    /// Returns the offset of gameObjects position to the bottom of gameObjects mesh.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns>Bottom-Offset</returns>
    public static Vector3 GetBottomOffset(GameObject gameObject)
    {
        Vector3 bottomPosition;
        if(GetBottomPosition(gameObject, out bottomPosition))
        {
            return gameObject.transform.position - bottomPosition;
        }
        return bottomPosition;
    }

    public static bool GetBottomPositionLocal(GameObject gameObject, out Vector3 bottomPosition)
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            bottomPosition = gameObject.transform.localPosition;
            bottomPosition.y = gameObject.transform.localPosition.y - meshFilter.mesh.bounds.extents.y * gameObject.transform.localScale.y;
            return true;
        }
        bottomPosition = Vector3.zero;
        return false;
    }

    public static Vector3 GetBottomOffsetLocal(GameObject gameObject)
    {
        Vector3 bottomPosition;
        if (GetBottomPositionLocal(gameObject, out bottomPosition))
        {
            return gameObject.transform.localPosition - bottomPosition;
        }
        return bottomPosition;
    }

}
