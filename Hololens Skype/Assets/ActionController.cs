using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour {
	public NotificationController m_notificationController;
	public GameObject m_actionPanel;

	private RectTransform m_actionRect;

	void Start () {
		m_notificationController.OnNotificationSelected += HandleNoitificationSelected;
		m_actionRect = (RectTransform)m_actionPanel.transform;
	}

	private void HandleNoitificationSelected(InfoCardController notification)
	{
		if (notification == null)
			m_actionPanel.SetActive(false);
		else
		{
			m_actionPanel.SetActive(true);
			RectTransform notificationRect = (RectTransform)notification.transform;
			m_actionRect.position = new Vector3(notificationRect.position.x, m_actionRect.position.y, m_actionRect.position.z);
		}
	}
}
