using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimatedObject
{
    delegate void onAnimationEnd();
    void Animate(onAnimationEnd callback = null);
}
