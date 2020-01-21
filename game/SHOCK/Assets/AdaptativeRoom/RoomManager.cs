using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CenterZone cz;
    GameObject previousRoom =null, nextRoom;
    private int roomNumber=0, remainingRooms=10;
    [SerializeField] public AdaptativeDoor doorN, doorE, doorS, doorW;
    private List<AdaptativeDoor> doors;
    private bool DoorsClosed;

    private bool doorReopened;
    private bool nextRoomCreated;

    private float gapBetweenRooms;
    [SerializeField] private GameObject templateRoom;

    private string cameFrom;

    int roomScenario=-1;
    bool scenarioRunning = false;
    bool lampValue = true;

    Vector3 firstRoomPos;

    [SerializeField] private PulsationConvertor pc;
    private bool stressed;

    void Start()
    {
      stressed = pc.getStressed();
      doors = new List<AdaptativeDoor>(){doorN, doorE, doorS, doorW};
      DoorsClosed=false;
      doorReopened=false;
      nextRoomCreated=false;
      gapBetweenRooms=3f;
      if(roomNumber!=0){
        roomScenario = chooseScenar(stressed);
        modifyRoom(stressed);
      }else{
        firstRoomPos=new Vector3(transform.position.x, transform.position.y, transform.position.z);
      }

      templateRoom = GameObject.Find("TemplateAdaptativeRoom");

    }

    // Update is called once per frame
    void Update()
    {
      stressed = pc.getStressed();
      if(!DoorsClosed && cz.getPlayerInRoom()){
        UnityEngine.Debug.Log("Nb " +roomNumber);

        closeAllDoors();
        removePreviousRoom();
        executeScenar();
        DoorsClosed= true;
        if(roomNumber==0){
          teleportRoom(new Vector3(0,-100,0));
        }
        if(remainingRooms==0){
          teleportRoomTo(firstRoomPos);
          foreach(AdaptativeDoor d in doors){
            d.setOpenable(false);
          }
          doorE.setOpenable(true);
          GameObject.Find("Opie").GetComponent<startGame>().lvl4done=true;
        }
      }
      else if(DoorsClosed && !nextRoomCreated && remainingRooms>0){
        checkDoorReopened();
      }
      if(scenarioRunning){
        executeScenar();
      }
    }

    void closeAllDoors(){
      foreach(AdaptativeDoor d in doors){
        d.closeDoor();
      }
    }

    void removePreviousRoom(){

      if(previousRoom!=null){

        Destroy(previousRoom);

      }
    }

    void checkDoorReopened(){
      AdaptativeDoor reopenedDoor = null;
      foreach(AdaptativeDoor d in doors){
        if(d.getOpen()){
          reopenedDoor = d;
        }
      }
      if(reopenedDoor!=null){
        foreach(AdaptativeDoor d in doors){
          d.setOpenable(false);
        }

        nextRoom = Instantiate(templateRoom);
        RoomManager nextRM = nextRoom.GetComponent<RoomManager>();
        nextRM.previousRoom=gameObject;
        UnityEngine.Debug.Log(nextRM.previousRoom);


        if(reopenedDoor == doorN){
            nextRoom.transform.position = transform.position + new Vector3(0,0,gapBetweenRooms);
            nextRM.doorS.openDoor();
            nextRM.doorN.closeDoor();
            cameFrom = "south";
        }
        else if(reopenedDoor == doorS){
            nextRoom.transform.position = transform.position + new Vector3(0,0,-1*gapBetweenRooms);
            nextRM.doorN.openDoor();
            nextRM.doorS.closeDoor();
            cameFrom = "north";
        }
        else if(reopenedDoor == doorE){
            nextRoom.transform.position = transform.position + new Vector3(gapBetweenRooms,0,0);
            nextRM.doorW.openDoor();
            nextRM.doorE.closeDoor();
            cameFrom = "west";
        }
        else if(reopenedDoor == doorW){
            nextRoom.transform.position = transform.position + new Vector3(-1*gapBetweenRooms,0,0);
            nextRM.doorE.openDoor();
            nextRM.doorW.closeDoor();
            cameFrom = "east";

        }
        nextRM.setParameters(roomNumber+1, remainingRooms-1, firstRoomPos);
        nextRoomCreated=true;
        //nextRM.modifyRoom(false);
      }
    }

    public void modifyRoom(bool stressed){
      //change item places
      switch(roomScenario){
        case 1 :
          GameObject bottle = transform.Find("Bottle").gameObject;
          bottle.transform.position = bottle.transform.position + new Vector3(Random.value,0,Random.value);
          GameObject books = transform.Find("Books").gameObject;
          books.transform.position = books.transform.position + new Vector3(Random.value,0,Random.value);
          GameObject chair = transform.Find("Chair").gameObject;
          chair.transform.position = chair.transform.position + new Vector3(Random.value,0,Random.value);


          /*GameObject chair = transform.Find("Chair").gameObject;
          chair.GetComponent<Rigidbody>().AddForce(fact1*Random.value, fact1*Random.value, fact1*Random.value);
          books.transform.position = books.transform.position + new Vector3(Random.value,0,Random.value);*/
          break;

      }


    }

    int chooseScenar(bool stressed){

      int scenar = -1;
      if(remainingRooms==0){
        //scenario 3, cat
        scenar=3;
      }
      else if(stressed){
        double val = Random.value;
        if(val < 0.35){
          //Scenario 0, nothing change
          scenar=0;
        }else if(val<0.50){
          //Scenario 2, throw items
          scenar=2;
        }else{
          //scenario 1, juste change places
          scenar=1;
        }
      }else{
        double val = Random.value;
        if(val < 0.3){
          //Scenario 0, nothing change
          scenar=0;
        }else if(val<0.60){
          //Scenario 2, throw items
          scenar=2;
        }else if(val<0.65){
          //Scenario 2, throw items
          scenar=3;
        }else{
          //scenario 1, juste change places
          scenar=1;
        }

      }
      return scenar;
    }

    public void setParameters(int nbRoom, int remaining, Vector3 initPos){
      roomNumber=nbRoom;
      remainingRooms = remaining;
      firstRoomPos=initPos;
    }

    private void executeScenar(){
      UnityEngine.Debug.Log(roomScenario);
      switch(roomScenario){
        case 2 :
          int fact1 = 1000;
          GameObject bottle = transform.Find("Bottle").gameObject;
          bottle.GetComponent<Rigidbody>().AddForce(fact1*Random.value, fact1*Random.value, fact1*Random.value);
          GameObject book1 = transform.Find("Books").Find("Book1").gameObject;
          GameObject book2 = transform.Find("Books").Find("Book2").gameObject;
          GameObject book3 = transform.Find("Books").Find("Book3").gameObject;
          book1.GetComponent<Rigidbody>().AddForce(fact1*Random.value, fact1*Random.value, fact1*Random.value);
          book2.GetComponent<Rigidbody>().AddForce(fact1*Random.value, fact1*Random.value, fact1*Random.value);
          book3.GetComponent<Rigidbody>().AddForce(fact1*Random.value, fact1*Random.value, fact1*Random.value);
          GameObject chair = transform.Find("Chair").gameObject;
          chair.GetComponent<Rigidbody>().AddForce(fact1*Random.value, fact1*Random.value, fact1*Random.value);

          break;
        case 3 :
          if(!scenarioRunning){
            scenarioRunning=true;
            switchLights(false);
            lampValue = false;
            GameObject cat = transform.Find("FakeCat").gameObject;
            cat.SetActive(true);
            int nbFakeCat=20;
            for(int i = 0; i<nbFakeCat; i++){
              GameObject tmpCat = Instantiate(cat);
              tmpCat.transform.SetParent(transform);
              tmpCat.transform.Rotate(Random.value*360,Random.value*360, Random.value*360);
              tmpCat.transform.position = cat.transform.position + new Vector3(2*Random.value-1,2*Random.value-0.5f,2*Random.value-1);
            }
          }
          else{
            if(Random.value<0.05){
              switchLights(!lampValue);
              lampValue = !lampValue;
            }
          }


          break;

      }
    }

      private void switchLights(bool val){
        List<string> points = new List<string>(){"Light1","Light2","Light3","Light4","Light5"};
        foreach(string p in points){
          transform.Find("Lamp").Find(p).gameObject.GetComponent<Light>().enabled=val;
        }

      }

      private void teleportRoom(Vector3 travel){
        transform.position = transform.position + travel;
        GameObject.Find("Opie").transform.position = GameObject.Find("Opie").transform.position +travel;

      }

      private void teleportRoomTo(Vector3 destination){
        Vector3 delta = destination - transform.position;
        transform.position = destination;
        GameObject.Find("Opie").transform.position = GameObject.Find("Opie").transform.position +delta;

      }


}
