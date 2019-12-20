using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOpie : MonoBehaviour
{
  public GameObject Player;
  public float movementSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
{
    transform.LookAt(Player.transform);
    transform.position += transform.forward * movementSpeed * Time.deltaTime;
}
}
