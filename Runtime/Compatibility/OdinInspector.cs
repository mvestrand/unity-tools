//
// Defines dummy attributes for code using attributes defined in Odin Inspector
//
#if ODIN_INSPECTOR

// Do nothing

#else

namespace MVest.Unity.OdinInspector {
    /// <summary>
    /// Dummy Odin Inspector attribute
    /// </summary>
    public class DrawWithUnityAttribute : System.Attribute { }
}

#endif


