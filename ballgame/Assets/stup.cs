using UnityEngine;
using System.Collections;

public class stup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Application.Quit();
        }
        if(Input.anyKeyDown)
        {
            this.gameObject.transform.localPosition += new Vector3(0, 1, 0);
            
                
               // this.gameObject.GetComponent<Material>().color = new Color(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1));
        }
	}
}
