using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTree : MonoBehaviour {
    public int plantId = 0;
    public GameObject flowerTree;
    public GameObject plataneVertical;
    public GameObject saulePleureur;
    public GameObject bush;
    public GameObject dandelion;
    public GameObject fern;
    public Animator oksusuAnim;

    public void SetPlantId(int id) {
        plantId = id;
    }

    void OnTriggerStay(Collider col) {
        if (col.CompareTag("Floor")) {
            if(Input.GetKeyDown("f")) {
                oksusuAnim.SetBool("pickUp", true);
                switch(plantId) {
                    case 0 :
                        Instantiate(flowerTree, this.transform.position, Quaternion.identity);
                        break;
                    case 1 :
                        Instantiate(plataneVertical, this.transform.position, Quaternion.identity);
                        break;
                    case 2 :
                        Instantiate(saulePleureur, this.transform.position, Quaternion.identity);
                        break;
                    case 3 :
                        Instantiate(bush, this.transform.position, Quaternion.identity);
                        break;
                    case 4 :
                        Instantiate(dandelion, this.transform.position, Quaternion.identity);
                        break;
                    case 5 :
                        Instantiate(fern, this.transform.position, Quaternion.identity);
                        break;
                    default :
                        break;
                }
            }
        }
    }
}
