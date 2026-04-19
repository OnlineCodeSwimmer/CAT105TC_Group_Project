using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfomationType
    {
        Reload
    }
    public InfomationType type;
    private Text uiText;

    private void Awake()
    {
       uiText = GetComponent<Text>();
    }
    private void LateUpdate()
    {
        switch (type)
        {
            case InfomationType.Reload:
                int currentBullet = GameManager.instance.rifle.currentbulletNumber;
                int maxBullet = GameManager.instance.rifle.maxBulletNumber;
                uiText.text = string.Format("{0}/{1}", currentBullet, maxBullet);
                break;
        }

    }
}
