using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMesh : MonoBehaviour
{
    public GameObject mesh;
    public GameObject staticMesh;
    public Animator anim;
    private bool changed = false;

    public void Update() {
        if(!changed && !anim.GetBool("growth")) {
            staticMesh.SetActive(true);
            mesh.SetActive(false);
            changed = true;
        }
    }
}