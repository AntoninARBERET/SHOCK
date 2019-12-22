     using UnityEngine;
     using System.Collections;

     public class randomWalk : MonoBehaviour {

         float speed=0.05f;
         float area=1f;
         private Vector2 newWayPoint;
         private Vector3 wayPoint;
         private Vector3 oldWayPoint;
         float timeSmooth=1f;
         private float time;
         private CharacterController controller;
         // Use this for initialization
         void Start () {
             newWayPoint = Random.insideUnitCircle * area;
             wayPoint = new Vector3(newWayPoint.x, transform.position.y, newWayPoint.y);
             controller = GetComponent<CharacterController>();
             oldWayPoint = wayPoint;
         }

         // Update is called once per frame
         void Update () {


             SailRandomly();



         }

         void SailRandomly(){
             //rende lookat() più gradevole alla vista
             Vector3 smoothLookAt = Vector3.Slerp(oldWayPoint, wayPoint, time/timeSmooth);
             time += Time.deltaTime;
             smoothLookAt.y = wayPoint.y;

             //il bot si dirige verso "waypoint" finché non lo raggiunge, poi cambia direzione
             if(Vector3.Distance(transform.position, wayPoint)>20.0f && time/timeSmooth < 1.0f){
                 transform.LookAt(smoothLookAt);
                 controller.SimpleMove(transform.forward * speed);
             }
             else {
                 newWayPoint = Random.insideUnitCircle * area;
                 oldWayPoint = wayPoint;
                 wayPoint = new Vector3(newWayPoint.x, wayPoint.y, newWayPoint.y);
                 transform.LookAt(smoothLookAt);
                 controller.SimpleMove(transform.forward * speed);
                 time = 0;
             }
         }

         void OnGUI() {
             //GUI.Label(new Rect(Screen.width-400, 0, 400, 200), "" + oldWayPoint.ToString() + " | " + wayPoint.ToString() + " | " + time/timeSmooth);
         }
     }
