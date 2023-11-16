using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unbrick : MonoBehaviour
{
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material triggedMaterial;
    public new MeshRenderer renderer;
    private bool isTrigged;

    void Start()
    {
        renderer.material = defaultMaterial;
        isTrigged = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        renderer.material = triggedMaterial;

        if(!isTrigged && other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().RemoveBrick();
            SoundManager.instance.PlayClip(AudioType.FX_Pickup);
            isTrigged = true;
        }
    }
}
