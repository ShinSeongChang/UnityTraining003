using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static partial class GFunc
{
    //                              디파인 심볼
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object mesaage)
    {
#if DEBUUG_MODE
        Debug.Log(mesaage);
#endif
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition)
    {
#if DEBUUG_MODE
        Debug.Assert(condition);
#endif
    }

    //! GameObject 받아서 Text 컴포넌트 찾아서 text 필드 값 수정하는 함수
    public static void SetText(this GameObject target, string text)
    {
        Text textComponent = target.GetComponent<Text>();
        if (textComponent == null || textComponent == default) { return; }

        textComponent.text = text;
    }

    public static Vector2 AddVector(this Vector3 origin, Vector2 addVector)
    {
        Vector2 result = new Vector2(origin.x, origin. y);
        result += addVector;
        return result;
    }

    public static bool IsValid<T>(this T target) where T : Component
    {
        if(target == null || target == default) {  return false; }
        else { return true; }
    }

    public static bool IsValid<T>(this List<T> target) where T : Component
    {
        bool isInvalid = (target == null || target == default);
        isInvalid = isInvalid || target.Count == 0;

        if (isInvalid == true) { return false; }
        else { return true; }
    }


}
