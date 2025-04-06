using UnityEngine;

public class WayPointUI : MonoBehaviour
{
    public Transform target;
    public RectTransform icon;
    float screen = 50f;
    void Update()
    {

        if (target != null)
        {
            Vector3 iconPos= Camera.main.WorldToScreenPoint(target.position);

            iconPos.x = Mathf.Clamp(iconPos.x, screen, Screen.width - screen);

            iconPos.y = Mathf.Clamp(iconPos.y, screen, Screen.height - screen);

            icon.position = iconPos;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
