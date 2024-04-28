using UnityEngine;

public static class ExtensionMethods
{
    public static bool Overlaps(this RectTransform rt1, RectTransform rt2)
    {
        Rect rect1 = new Rect(rt1.localPosition.x, rt1.localPosition.y, rt1.rect.width, rt1.rect.height);
        Rect rect2 = new Rect(rt2.localPosition.x, rt2.localPosition.y, rt2.rect.width, rt2.rect.height);
        return rect1.Overlaps(rect2);
    } 
}
