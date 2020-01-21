using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject textSpace;
    [SerializeField] private PulsationConvertor pc;
    private float calibratedSince;
    void Start()
    {

        calibratedSince = -1;
    }

    // Update is called once per frame
    void Update()
    {
      if(!pc.getCalibrated()){
        textSpace.SetActive(true);
        textSpace.GetComponent<Text>().text = "Calibration running...";
      }
      else if(calibratedSince==-1){
        textSpace.GetComponent<Text>().text = "Calibration done ! Normal beat rate around " + pc.getCalibrationValue()+" BPM";
        calibratedSince = Time.time;
      }
      else if(Time.time - calibratedSince > 8){
        textSpace.SetActive(false);
      }
    }
}
