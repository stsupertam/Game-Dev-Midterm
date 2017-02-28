using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRender : MonoBehaviour {

    public LayerMask floorMask;

    private GameObject Building;
    private Transform [] all_floors;
    private RaycastHit hit;


    void Floor_testing () {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            transform.position = new Vector3(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            transform.position = new Vector3(0, 6, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            transform.position = new Vector3(0, 16, 0);
        }
    }


    Transform [] Floor () {
        Building = GameObject.Find ("Building");
        Transform [] floor = new Transform[Building.gameObject.transform.childCount];
        for(int i = 0; i < floor.Length; i++) {
            floor [i] = Building.gameObject.transform.GetChild(i);
        }
        return floor;
    }


    void Awake () {
        all_floors = Floor ();
    }


    void Start () {
        StartCoroutine ("RenderFloorsWithDelay", .5f);
    }

    IEnumerator RenderFloorsWithDelay (float delay) {
        while (true) {
            yield return new WaitForSeconds (delay);
            RenderFloors ();
        }
    }


    void RenderFloors() {
        if (Physics.Raycast (transform.position, Vector3.down, out hit, Mathf.Infinity, floorMask )) {
            string [] floorNumber = hit.transform.name.Split('_');
            for (int i = 0; i < int.Parse(floorNumber[1]); i++ ){
                all_floors [i].GetComponent<Renderer>().enabled = true;
                    for(int j = 0; j < all_floors [i].gameObject.transform.childCount; j++) {
                        all_floors [i].gameObject.transform.GetChild(j).GetComponent<Renderer>().enabled = true;
                        }
            }
            for (int i = int.Parse(floorNumber [1]); i <= all_floors.Length - 1; i++ ){
                all_floors [i].GetComponent<Renderer>().enabled = false;
                    for(int j = 0; j < all_floors [i].gameObject.transform.childCount; j++){
                        all_floors [i].gameObject.transform.GetChild(j).GetComponent<Renderer>().enabled = false;
                        }
            }
        }
    }


    void Update () {
        Floor_testing();
    }
}
