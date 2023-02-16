using UnityEngine;
[System.Serializable]
public class PointInTime
{
 public SerializedVector3 position;
 public SerializedVector3 rotation;

 public PointInTime(Vector3 _position, Vector3 _rotation)
 {
  position = _position;
  rotation = _rotation;
 }
}
