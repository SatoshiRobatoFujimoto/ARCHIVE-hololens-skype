using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotificationController : MonoBehaviour {
	public GameObject m_notificationPrefab;
	public GameObject m_notificationGrid;
	public Sprite[] m_avatars;
	public string[] m_notificationText;

	private InfoCardController m_activeCard;
	private float m_endOfGrid;
	
	void Start () {
		if (m_avatars.Length != m_notificationText.Length)
			Debug.LogError("Amount of avatars not consistent with notifications texts");
		else
		{
			float spacing = m_notificationGrid.GetComponent<HorizontalLayoutGroup>().spacing;
			float totalWidth = (m_avatars.Length - 1) * spacing;
			GameObject[] cards = new GameObject[m_avatars.Length];

			for (int i = 0; i < m_avatars.Length; i++)
			{
				cards[i] = Instantiate(m_notificationPrefab);
				cards[i].transform.SetParent(m_notificationGrid.transform, false);
			}

			Canvas.ForceUpdateCanvases();

			for (int i = 0; i < m_avatars.Length; i++)
			{
				GameObject newNotification = cards[i];
				RectTransform newTransform = (RectTransform)newNotification.transform;
				totalWidth += newTransform.sizeDelta.x;
				InfoCardController controller = newNotification.GetComponent<InfoCardController>();
				controller.m_avatarImage.sprite = m_avatars[i];
				controller.m_notificationText.text = m_notificationText[i];
				controller.OnInfoCardClicked += handleInfoCardPress;
			}
			RectTransform gridRect = (RectTransform)m_notificationGrid.transform;
			Vector2 newSize = new Vector2(totalWidth, gridRect.sizeDelta.y);
			gridRect.sizeDelta = newSize;
		}
	}

	private void handleInfoCardPress(InfoCardController pressed)
	{
		if (m_activeCard == pressed)
		{
			pressed.SetState(false);
			m_activeCard = null;
		}
		else
		{
			if (m_activeCard != null)
				m_activeCard.SetState(false);
			pressed.SetState(true);
			m_activeCard = pressed;
		}
	}
}
