using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disappear : MonoBehaviour
{
    public Transform opie;
    private int nextlevel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator waitForAWhile(){
          yield return new WaitForSeconds(1f);
          opie.gameObject.GetComponent<startGame>().setLevel(nextlevel);
          transform.gameObject.SetActive(false);
      }
      public void disappearing(){
        StartCoroutine(waitForAWhile());

      }
      public void setNextLevel(int l){
        nextlevel=l;
      }
}
