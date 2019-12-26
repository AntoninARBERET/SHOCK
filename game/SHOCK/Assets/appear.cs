using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void appearing(Vector3 v){
      transform.position=v;
      transform.gameObject.SetActive(true);
      GetComponent<AudioSource>().Play();
    }
}
