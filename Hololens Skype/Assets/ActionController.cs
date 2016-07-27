using UnityEngine;
using System.Collections;
using System;

public class ActionController : MonoBehaviour {
	public NotificationController m_notificationController;
	public GesturePanelController m_gesturePanel;

	private RectTransform m_actionRect;

	void Start () {
		m_notificationController.OnNotificationSelected += HandleNoitificationSelected;
		m_actionRect = (RectTransform)m_gesturePanel.transform;
	}

	void Awake()
	{
		m_gesturePanel.gameObject.SetActive(false);
	}
	
	private void HandleNoitificationSelected(InfoCardController notification)
	{
		if (notification == null)
		{
			m_gesturePanel.gameObject.SetActive(false);
		}
		else
		{
			m_gesturePanel.gameObject.SetActive(true);
			RectTransform notificationRect = (RectTransform)notification.transform;
			m_actionRect.position = new Vector3(notificationRect.position.x, m_actionRect.position.y, m_actionRect.position.z);
		}
	}

	public void HandleActionRemove()
	{
		m_notificationController.RemoveNotification(m_notificationController.ActiveCard);
	}
}
