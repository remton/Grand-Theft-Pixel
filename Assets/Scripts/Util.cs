using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,Right
}
public static class Util
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static float GetClipLength(this Animator animator, string clipName)
    {
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == clipName)
                return ac.animationClips[i].length;
        }
        Debug.LogWarning("Animation clip: '" + clipName + "' was not found");
        return 0;
    }

    /// <summary> Chooses a random gameobj from a list (all objects must have the choosable script)</summary>
    public static GameObject ChooseRandom(List<Choosable> choosables)
    {
        if (!(choosables.Count > 0))
        {
            Debug.LogError("ChooseRandom was passed an empty or broken list!");
            Debug.Break();
            return null;
        }

        float sum = 0;
        foreach (Choosable choice in choosables)
        {
            sum += choice.chance;
        }

        float rand = UnityEngine.Random.Range(0, sum);
        float psum = 0;
        foreach (Choosable choice in choosables)
        {
            if ((choice.chance + psum) >= rand && psum <= rand)
            {
                return choice.obj;
            }
            psum += choice.chance;
        }
        Debug.LogError("Choose Random Failed!");
        Debug.Break();
        return null;
    }
}
