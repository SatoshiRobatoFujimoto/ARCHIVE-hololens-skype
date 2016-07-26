using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotificationController : MonoBehaviour {
	public GameObject m_notificationPrefab;
	public GameObject m_notificationGrid;
	public Sprite[] m_avatars;
	public string[] m_notificationText;

	private InfoCardController m_activeCard;
	
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
				controller.OnInfoCardClicked += handleInfoCardPress;
			}
			RectTransform gridRect = (RectTransform)m_notificationGrid.transform;
			Vector2 newSize = new Vector2(totalWidth, gridRect.sizeDelta.y);
			gridRect.sizeDelta = newSize;
			Vector3 newPos = new Vector3(totalWidth / 2000, gridRect.position.y, gridRect.position.z);
			gridRect.position = newPos;
		}
	}

	private void handleInfoCardPress(InfoCardController pressed)
	{
		if (m_activeCard != null)
			m_activeCard.SetState(false);
		pressed.SetState(true);
		m_activeCard = pressed;
	}
}
