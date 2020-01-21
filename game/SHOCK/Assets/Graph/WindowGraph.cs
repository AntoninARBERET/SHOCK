using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class WindowGraph : MonoBehaviour
{
    // Start is called before the first frame update
    private RectTransform graphContainer;
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private PulsationConvertor pc = null;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private List<GameObject> tmpObjects =null;
    private List<RectTransform> tmpLabels =null;
    private float YMinimum, YMaximum, graphHeight;


    private void Awake(){
      refresh();
    }

    public void refresh(){
      if(tmpObjects != null){
        for(int i=0; i < tmpObjects.Count; i++){
          Destroy(tmpObjects[i]);
        }
        /*for(int i=0; i < tmpLabels.Count; i++){
          RectTransform l =tmpLabels[i];
          Destroy(l.Find("Text").GetComponent<Text>());
          Destroy(l);
        }*/
      }
      tmpObjects = new List<GameObject>();
      tmpLabels = new List<RectTransform>();
      graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
      labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
      labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
      List<double> valueList;
      if(pc==null){
        valueList = new List<double>(){60, 60, 60, 60, 60, 66, 80, 100, 110};
      }
      else{
        valueList = pc.getHisto();
      }
      ShowGraph(valueList);
      CreateHorizontalLine((float)pc.getCalibrationValue(), new Color(0f,0.1f,0.7f));
      CreateHorizontalLine((float)pc.getStressValue(), new Color(0.8f,0f,0f));
    }



    private GameObject CreateCircle(Vector2 anchoredPosition){
      //UnityEngine.Debug.Log("Create circle");
      GameObject gameObject = new GameObject("circle", typeof(Image));
      gameObject.transform.SetParent(graphContainer, false);
      gameObject.GetComponent<Image>().sprite = circleSprite;
      RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
      rectTransform.anchoredPosition = anchoredPosition;
      rectTransform.sizeDelta = new Vector2(11,11);
      rectTransform.anchorMin = new Vector2(0,0);
      rectTransform.anchorMax = new Vector2(0,0);
      tmpObjects.Add(gameObject);
      return gameObject;
    }

    private void ShowGraph(List<double> valueList){

      float xSize = graphContainer.sizeDelta.x / (valueList.Count+1);
      YMaximum = (float)Math.Max(valueList.Max()+10, pc.getStressValue());
      UnityEngine.Debug.Log("max val : " +valueList.Max());
      YMinimum = (float)valueList.Min()-10;//40f;
      graphHeight = graphContainer.sizeDelta.y;
      Vector2 origin = new Vector2(xSize, 10);
      //CreateAxis(origin,YMinimum, YMaximum);
      GameObject lastCircleGameObject = null;

      for(int i=0; i < valueList.Count; i++){
        float xPosition =(float)( (i+1)* xSize);
        float yPosition = (float)(((valueList[i] -YMinimum) / (YMaximum-YMinimum))  * graphHeight);
        GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
        if(lastCircleGameObject!=null){
          CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
        }
        lastCircleGameObject = circleGameObject;

        RectTransform labelX = Instantiate(labelTemplateX);
        labelX.SetParent(graphContainer, false);
        labelX.gameObject.SetActive(true);
        labelX.anchoredPosition=new Vector2(xPosition, -15f);
        labelX.GetComponent<Text>().text = i.ToString();
        tmpObjects.Add(labelX.gameObject);
      }

      int separatorCount = 10;
      for (int i=0; i<separatorCount; i++){
        RectTransform labelY = Instantiate(labelTemplateY);
        labelY.SetParent(graphContainer, false);
        labelY.gameObject.SetActive(true);
        float normalizedValue = i * 1f / separatorCount;
        labelY.anchoredPosition=new Vector2(-18f, normalizedValue * graphHeight);
        labelY.GetComponent<Text>().text = (Mathf.RoundToInt(YMinimum + i * (YMaximum - YMinimum) / separatorCount)).ToString();
        tmpObjects.Add(labelY.gameObject);
      }


    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB){
      GameObject gameObject = new GameObject("dotConnection", typeof(Image));
      gameObject.GetComponent<Image>().color = new Color(1,1,1,.5f);
      Vector2 dir = (dotPositionB - dotPositionA).normalized;
      float distance = Vector2.Distance(dotPositionA, dotPositionB);
      gameObject.transform.SetParent(graphContainer, false);
      RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
      rectTransform.anchorMin = new Vector2(0, 0);
      rectTransform.anchorMax = new Vector2(0, 0);
      rectTransform.sizeDelta = new Vector2(distance, 3f);
      rectTransform.anchoredPosition = dotPositionA + dir*  distance*0.5f;
      float alpha =(float) (360*(Math.Atan ((dotPositionB[1] - dotPositionA[1]) /(dotPositionB[0] - dotPositionA[0])))/(2* Math.PI));
      //UnityEngine.Debug.Log("alpha " + alpha );
      rectTransform.localEulerAngles = new Vector3(0, 0, alpha);
      tmpObjects.Add(gameObject);
    }

    /*private void CreateAxis(Vector2 origin, float YMinimum, float YMaximum){
      //YAxis
      GameObject gameObject = new GameObject("yAxis", typeof(Image));
      RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
      gameObject.GetComponent<Image>().color = new Color(1,1,1,1f);
      Vector2 dir = new Vector2(0,1);
      float distance =  graphContainer.sizeDelta.y;
      gameObject.transform.SetParent(graphContainer, false);
      rectTransform.anchorMin = new Vector2(0, 0);
      rectTransform.anchorMax = new Vector2(0, 0);
      rectTransform.sizeDelta = new Vector2(distance, 3f);
      rectTransform.anchoredPosition = new Vector2(origin[0], 0) + dir * distance*.5f;
      rectTransform.localEulerAngles = new Vector3(0, 0, 90);



    }*/

    private void CreateHorizontalLine(float value, Color color){
      GameObject gameObject = new GameObject("line", typeof(Image));
      RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
      gameObject.GetComponent<Image>().color = color;
      Vector2 dir = new Vector2(1,0);
      float distance =  graphContainer.sizeDelta.x;
      gameObject.transform.SetParent(graphContainer, false);
      rectTransform.anchorMin = new Vector2(0, 0);
      rectTransform.anchorMax = new Vector2(0, 0);
      rectTransform.sizeDelta = new Vector2(distance, 3f);
      float yPosition = (float)(((value -YMinimum) / (YMaximum-YMinimum))  * graphHeight);
      rectTransform.anchoredPosition = new Vector2(0, yPosition) + dir * distance*.5f;
      rectTransform.localEulerAngles = new Vector3(0, 0, 0);
      tmpObjects.Add(gameObject);
    }
}
