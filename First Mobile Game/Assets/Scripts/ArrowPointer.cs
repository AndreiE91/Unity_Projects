using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ArrowPointer : MonoBehaviour
{
    [SerializeField]
    private Camera uiCamera;
    [SerializeField]
    private Sprite arrowSprite;
    [SerializeField]
    private Sprite crossSprite;


    public Transform targetPosition;
    private RectTransform pointerRectTransform;
    private Image pointerImage;

    

    void Awake()
    {
        
        pointerRectTransform = gameObject.GetComponent<RectTransform>();
        pointerImage = gameObject.GetComponent<Image>();
    }

    void Update()
    {
            float borderSize = 100f;
            Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition.position);
            bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= (Screen.width - borderSize) || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= (Screen.height - borderSize);

            if (isOffScreen)
            {
                rotatePointTowardsTargetPosition();
                pointerImage.sprite = arrowSprite;
                Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
                if (cappedTargetScreenPosition.x <= borderSize)
                    cappedTargetScreenPosition.x = borderSize;
                if (cappedTargetScreenPosition.x >= Screen.width - borderSize)
                    cappedTargetScreenPosition.x = Screen.width - borderSize;
                if (cappedTargetScreenPosition.y <= borderSize)
                    cappedTargetScreenPosition.y = borderSize;
                if (cappedTargetScreenPosition.y >= Screen.height - borderSize)
                    cappedTargetScreenPosition.y = Screen.height - borderSize;

                Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
                pointerRectTransform.position = pointerWorldPosition;
                pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0);
            }
            else
            {
                pointerImage.sprite = crossSprite;
                Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
                pointerRectTransform.position = pointerWorldPosition;
                //pointerRectTransform.localPosition = new Vector3(pointerRectTransform.position.x, pointerRectTransform.position.y, 0);
                //pointerRectTransform.localEulerAngles = Vector3.zero;
            }
    }

    void rotatePointTowardsTargetPosition()
    {
        Vector3 toPosition = targetPosition.position;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    /*
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(Vector3 targetPosition)
    {
        gameObject.SetActive(true);
        this.targetPosition.position = targetPosition;
    }
    */
}
