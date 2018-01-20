using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetective
{
	string CurrentRoom { get; set ;}
	void AccuseSuspect (GameObject suspect);
}

