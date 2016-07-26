using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InfoCardController : MonoBehaviour {
	public event Action<InfoCardController> OnInfoCardClicked;
	public event Action<InfoCardController> OnRemoveAnimationFinished;
	public Image m_avatarImage;
	public Text m_notificationText;
	[HideInInspector()]
	public int id;

	private Animator m_myAnimator;
	private bool m_currentlyIsSmall = false;

	public void StartRemove()
	{
		m_myAnimator.SetTrigger("Remove");
	}

	public void SetState(bool isSmall)
	{
		if (isSmall != m_currentlyIsSmall)
		{
			if (isSmall)
				m_myAnimator.SetTrigger("Hide");
			else
				m_myAnimator.SetTrigger("Show");
			m_currentlyIsSmall = isSmall;
		}
	}

	public void Start()
	{
		m_myAnimator = GetComponent<Animator>();
	}

	public void HandleRemoveAnimationFinished()
	{
		if (OnRemoveAnimationFinished != null)
			OnRemoveAnimationFinished(this);
	}

	public void HandleCardButtonClicked()
	{
		if (OnInfoCardClicked != null)
			OnInfoCardClicked(this);
	}
}
