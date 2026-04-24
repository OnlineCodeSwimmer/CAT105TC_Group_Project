using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelBuff : MonoBehaviour
{
    public  enum BuffName
    {
        BulletPlus, 
        DamagePlus,
    }
    public BuffName buffName;
    private Text levelText;
    private int level;

    private void Awake()
    {
        level = 1;
        levelText = GetComponentInChildren<Text>();
    }

    private void LateUpdate()
    {
        levelText.text = string.Format("Lv.{0:D2}", level);
    }
    public void OnClick()
    {
        switch(buffName)
        {
            case BuffName.BulletPlus:
                GameManager.instance.rifle.maxBulletNumber += 5;
                level++;
                break;
        }

        if (level == 6)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
