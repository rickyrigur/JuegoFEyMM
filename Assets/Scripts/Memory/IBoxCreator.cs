using System.Collections.Generic;

public interface IBoxCreator
{
    void CreateBoxes(int amount, MemoryBox box);
    void HideElements(List<MemoryObjects> objects);
}
