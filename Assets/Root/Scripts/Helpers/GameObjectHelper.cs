using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectHelper
{
    public static bool MatchTag(GameObject gameObject, IEnumerable<string> tags)
    {
        foreach (var tag in tags)
        {
            if (gameObject.CompareTag(tag))
            {
                return true;
            }
        }

        return false;
    }
}
