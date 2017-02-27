using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {

    public float viewRadius;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    private GameObject Targets;
    private Transform [] all_target;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    Transform [] Target () {
        Targets = GameObject.Find ("Targets");
        Transform [] target = new Transform[Targets.gameObject.transform.childCount];
        for(int i = 0; i < target.Length; i++) {
            target [i] = Targets.gameObject.transform.GetChild(i);
        }
        return target;
    }

    void Awake () {
        all_target = Target ();
    }

    void Start () {
        StartCoroutine ("RenderTargetsWithDelay", .2f);
    }

    IEnumerator RenderTargetsWithDelay (float delay) {
        while (true) {
            yield return new WaitForSeconds (delay);
            RenderTargets ();
        }
    }

    void CheckOutOfViewRadius (Collider [] targetsInViewRadius) {
        for(int i = 0; i < all_target.Length; i++){
            int pos = System.Array.IndexOf (targetsInViewRadius, all_target[i]);
            if (pos < 0){
                all_target[i].GetComponent<Renderer>().enabled = false;
            }
        }
    }

    void RenderTargets() {
        visibleTargets.Clear ();
        Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);
        CheckOutOfViewRadius(targetsInViewRadius);

        for (int i = 0; i < targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius [i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance (transform.position, target.position);

            if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
                visibleTargets.Add (target);
                target.GetComponent<Renderer>().enabled = true;
            }
            else{
                target.GetComponent<Renderer>().enabled = false;
            }
        }
    }


    public Vector3 DirFromAngle (float angleInDegrees, bool angleIsGlobal) {
        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3 (Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
