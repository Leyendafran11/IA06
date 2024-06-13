using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Musculos : MonoBehaviour
{
    private NavMeshAgent nv;
    private Vector3 destino;
    private Animator anim;
    private bool enMovimiento;
    private bool enGiro;

	private void Awake()
	{
		nv = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enMovimiento = false;
	}

    public void mover(Vector3 destino)
    {
        this.destino = destino;
        StartCoroutine("irDestino");
    }

    public void rotar()
    {
		StartCoroutine("darVuelta");
	}

    public void darZarpazo()
    {
        anim.SetTrigger("Ataque");
    }

    public void parar()
    {
        nv.SetDestination(this.transform.position);
        enMovimiento = false;
        enGiro = false; 
    }


	void Update()
    {
        if (enMovimiento)
        {
            anim.SetFloat("Velocidad", 1);
        }
        else
        {
            anim.SetFloat("Velocidad", 0);
        }
        
    }

    public bool seMueve()
    {
        return enMovimiento;
    }

    public bool seGira()
    {
        return enGiro;
    }

    private IEnumerator irDestino()
    {
        enMovimiento = true;
		nv.SetDestination(this.destino);

		while(Vector3.Distance(this.transform.position, this.destino) > 2.0f)
		{
            yield return new WaitForEndOfFrame();
		}
		
		nv.SetDestination(this.transform.position);
		enMovimiento = false;
	}

    private IEnumerator darVuelta()
    {
        enGiro = true;
        int rotacion = 0;

        while (rotacion < 360 && enGiro==true) {
            rotacion+=5;
            transform.Rotate(Vector3.up,5);
			yield return new WaitForEndOfFrame();
		}

        enGiro = false;
    }
}
