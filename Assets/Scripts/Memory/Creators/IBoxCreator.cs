using System.Collections.Generic;
using UnityEngine;

public interface IBoxCreator
{
    void CreateBoxes(int amount, MemoryBox box, Transform parent);
    void SetValidator(IValidator validator);
    void HideElements(List<MemoryObjects> objects, ToyFactory toyFactory);
    void CleanBoxes();
    void DestroyBoxes();
}
