using UnityEngine;
using System.Collections;
using System;

public class Instrument : MonoBehaviour
{
    private AudioSource[] notes = new AudioSource[Enum.GetValues(typeof(Note)).Length];

	void Start()
    {
	
	}
	
	void Update()
    {
	
	}
}
