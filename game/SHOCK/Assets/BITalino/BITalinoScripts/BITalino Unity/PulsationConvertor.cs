﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;


public class PulsationConvertor : MonoBehaviour
{
  public BITalinoReader reader;
  public int channelRead = 0;
  public double divisor = 1;

  public int deltaT = 10;
  public double deltaPulse = 0;
  public double alpha = 0.05;

  private bool calibrated;
  private double beatRate;
  private double coef;

  private int upperNormalBPMBound = 100;
  private int lowerNormalBPM = 60;
  private int calibratingRound = 0;
  private int calibrateTreshold = 500;
  private int adjustmentRound = 0;
  private int adjustmentTreshold = 50;

  public string stressTracesFilePath=null;
  private double stressThreshold;
  private bool stressed;

  private float lastRecord;
  public string histTracesFilePath=null;
  List<double> histo;
    void Start()
    {
        beatRate = 0;
        calibrated=false;
        stressed=false;
        coef = 60/(reader.BufferSize / reader.manager.SamplingRate);
        lastRecord=0;
        if(stressTracesFilePath!=null){
          readStressTraces();
        }

        if(histTracesFilePath!=null){
          File.WriteAllText(histTracesFilePath,string.Empty);
        }
        histo = new List<double>();

        addPointToFile(107, "stress");
    }

    // Update is called once per frame
    void Update()
    {
      if (reader.asStart)
      {
          updateBeatRate();
          //UnityEngine.Debug.Log(beatRate + " BPM");
          recordBreatRate();
          if(!calibrated){
            checkCalibration();
          }
      }
    }

    void updateBeatRate(){
      int nPulses = 0;
      int i = 0;
      bool inPulse = false;
      for(int k = deltaT; k<reader.getBuffer().Length; k++)
      {
          BITalinoFrame f0 = reader.getBuffer()[k-deltaT];
          BITalinoFrame f1 = reader.getBuffer()[k];
          float intensity0 = (float) ((f0.GetAnalogValue(channelRead)) / divisor);
          float intensity1 = (float) ((f1.GetAnalogValue(channelRead)) / divisor);
          if (intensity1 - intensity0 > deltaPulse && !inPulse){
            inPulse = true;
            nPulses ++;
          }
          else if (intensity0 - intensity1 > deltaPulse && inPulse) {
            inPulse = false;
          }
          i++;
      }
      beatRate = nPulses * coef;
    }

    void checkCalibration(){
      if(beatRate > lowerNormalBPM && beatRate < upperNormalBPMBound){
        calibratingRound ++;
        if(calibratingRound>calibrateTreshold){
          calibrated = true;
          calibratingRound=0;
          UnityEngine.Debug.Log("calibrated around " + beatRate);
          if(stressTracesFilePath==null){
            stressThreshold=beatRate+0.2*beatRate;
          }
        }
        //UnityEngine.Debug.Log(calibratingRound+"calib round");
      }else{
        calibratingRound=0;
        adjustmentRound++;
        if(adjustmentRound > adjustmentTreshold){
          adjustmentRound=0;
          if(beatRate < lowerNormalBPM ){
            deltaPulse=deltaPulse - alpha;
            UnityEngine.Debug.Log("To low => adjustment");
          }else{
            deltaPulse=deltaPulse + alpha;
            UnityEngine.Debug.Log("To high => adjustment");
          }
        }
      }
    }

    void readStressTraces(){
      UnityEngine.Debug.Log("Start reading");
      int counter = 0;
      string line;
      List<double> normalValues = new List<double>();
      List<double> stressValues = new List<double>();
      // Read the file and display it line by line.
      System.IO.StreamReader file = new System.IO.StreamReader(stressTracesFilePath);
      while((line = file.ReadLine()) != null)
      {
          UnityEngine.Debug.Log(line);
          string[] values = line.Split(',');
          UnityEngine.Debug.Log(values[0]);
          if(values[1]=="normal"){
            normalValues.Add(double.Parse(values[0], System.Globalization.CultureInfo.InvariantCulture));
          }else{
            stressValues.Add(double.Parse(values[0], System.Globalization.CultureInfo.InvariantCulture));
          }
          counter++;

      }


      normalValues.Sort();
      stressValues.Sort();

      if(stressValues[0]>normalValues[normalValues.Count -1]){
          stressThreshold=normalValues[normalValues.Count -1] + (stressValues[0]-normalValues[normalValues.Count -1])/2;
      }else{
        double bestLimit = -1;
        int bestNumber = -1;
        int numS = 0;
        foreach(double s in stressValues){
          int num = 0;
          numS ++;
          foreach(double n in normalValues){
            if(n<s){
              num++;
            }
          }
          num = num - numS;
          if(num>bestNumber){
            bestNumber=num;
            bestLimit=s;
          }
        }
        stressThreshold=bestLimit;
      }
      file.Close();
    }

    public void addPointToFile(double val, string state){
      if(stressTracesFilePath!=null){
        File.AppendAllText(stressTracesFilePath,val+","+state);
      }

    }

    void  updateStressed(){
      if(beatRate>=stressThreshold){
        stressed = true;
      }else{
        stressed= false;
      }
    }

    public bool getStressed(){
      return stressed;
    }

    public bool getCalibrated(){
      return calibrated;
    }

    private void recordBreatRate(){
      if(histTracesFilePath != null && calibrated && Time.time - lastRecord>1){
        lastRecord=Time.time;
        File.AppendAllText(histTracesFilePath,""+beatRate+",");
        histo.Add(beatRate);
      }
    }

    public List<double> getHisto(){
      return histo;
    }
}
