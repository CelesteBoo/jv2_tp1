using UnityEngine;

public static class ArrayExtensions
{
    public static T Random<T>(this T[] clips)
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
