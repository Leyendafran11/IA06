using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerebro : MonoBehaviour
{
    private Ojos ojos;
    private Musculos musculos;
	private float retardo;


	private void Awake()
	{
		ojos = GetComponent<Ojos>();
		musculos = GetComponent<Musculos>();
		
	}

	private void Start()
	{
		retardo = 0.1f;
		StartCoroutine("patrullar");
	}


	private IEnumerator patrullar()
	{
		Debug.Log("PATRULLA");

		bool encontrado = false;

		Vector3 posDestino = new Vector3(Random.Range(-24.0f,24.0f),0,Random.Range(-24.0f,24.0f));
		musculos.mover(posDestino);

		while (musculos.seMueve())
		{
			if (ojos.esEncontrado())
			{
				encontrado = true;
				break;
			}
			yield return new WaitForSeconds(retardo);
		}
		musculos.parar();

		if (encontrado)
		{
			StartCoroutine("perseguir");
		}
		else
		{
			StartCoroutine("buscar");
		}
	}

	private IEnumerator buscar()
	{
		Debug.Log("BUSCAR");

		bool encontrado = false;

		musculos.rotar();

		while (musculos.seGira())
		{
			if (ojos.esEncontrado())
			{
				encontrado = true;
				break;
			}
			yield return new WaitForSeconds(retardo);
		}

		musculos.parar();

		if (encontrado)
		{
			StartCoroutine("perseguir");
		}
		else
		{
			StartCoroutine("patrullar");
		}

	}
	
	private IEnumerator perseguir()
	{
		Debug.Log("PERSEGUIR");

		Vector3 posObjetivo;
		bool puedoAtacar = false;

		while (ojos.esEncontrado())
		{
			posObjetivo = ojos.getObjetivo();

			musculos.mover(ojos.getObjetivo());

			if (Vector3.Distance(transform.position, posObjetivo) < 3.0f)
			{
				puedoAtacar = true;
				break;
			}

			yield return new WaitForSeconds(retardo);
		}

		musculos.parar();

		if (puedoAtacar)
		{
			StartCoroutine("atacar");
		}
		else
		{
			StartCoroutine("buscar");
		}

	}

	private IEnumerator atacar()
	{

		Debug.Log("ATACAR");

		musculos.darZarpazo();

		yield return new WaitForSeconds(2.0f);

		musculos.parar();

		StartCoroutine("perseguir");

	}
}
