using UnityEngine.EventSystems;

// helper for last cursor data
public class CustomStandaloneInputModule : StandaloneInputModule
{
    public PointerEventData GetPointerData()
    {
        return m_PointerData[kMouseLeftId];
    }
}