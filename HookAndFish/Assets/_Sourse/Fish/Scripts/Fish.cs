using System;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public event Action<int> LevelSet;

    public int Level { get; private set; }

    public void SetLevel(int level)
    {
        Level = level;
        LevelSet?.Invoke(Level);
    }
}