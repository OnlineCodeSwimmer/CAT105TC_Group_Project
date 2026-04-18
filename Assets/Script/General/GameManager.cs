using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //欢迎来到游戏管理系统，主要提高跨物体调用脚本的便利性以及用于实现一些控制全局的功能
    public Player player;
    public PoolManager poolManager;
    public static GameManager instance; //静态声明可以在其他脚本中不用实例化就可以调用

    private void Awake()
    {
        instance = this; 
    }

}
