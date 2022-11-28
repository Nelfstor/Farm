using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
	[SerializeField] Animator animator;

	public void IdleAnimation()
	{
		animator.SetBool("IdleToMove", false);

	}
	public void RunAnimation()
	{
		animator.SetBool("IdleToMove", true);
		animator.SetBool("RunToPutting", false);
		animator.SetBool("RunToCollect", false);
		animator.SetBool("RunToHarvest", false);

	}
	public void Put()
    {
		animator.SetBool("RunToPutting", true);
		animator.SetBool("IdleToMove", false);
	}
	public void RunToHarvest()
	{
		animator.SetBool("RunToHarvest", true);
		animator.SetBool("IdleToMove", false);
	}
	public void RunToCollect()
    {
		animator.SetBool("RunToCollect", true);
		animator.SetBool("IdleToMove", false);
	}


}
