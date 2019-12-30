using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowNero : MonoBehaviour
{

  public Transform nero;
  public float sSpeed = 10.0f;
  public Vector3 dist;

  void FixedUpdate() {
      Vector3 dPos = nero.position + dist;
      Vector3 sPos = Vector3.Lerp(transform.position, dPos, sSpeed * Time.deltaTime);
      transform.position = sPos;
      transform.LookAt(nero.position);
  }
}

