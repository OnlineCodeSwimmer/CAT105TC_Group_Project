using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Welcome to the Game Management System, which streamlines cross-object script referencing and enables various global control functionalities.
    public Player player;
    public PoolManager poolManager;
    public Rifle rifle;
    public static GameManager instance; //Static members can be accessed from other scripts without creating an instance.

    private void Awake()
    {
        instance = this; 
    }

}
