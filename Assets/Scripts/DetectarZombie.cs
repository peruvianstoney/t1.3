using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarZombie : MonoBehaviour
{
    // Start is called before the first frame update
    public static int contador=0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            contador++;
            Debug.Log("ZOMBIE SALTADO: "+contador);
        }
    }
}
