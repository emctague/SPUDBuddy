namespace RuntimeInjectedCode
{
    /// <summary>
    /// Shorthand for setting and getting private field values via reflection.
    /// </summary>
    public class RefUtil
    {
        public static void Set<T>(string field, object target, T value)
        {
            var f = target.GetType().GetField(field);
            if (f == null) return;
            
            f.SetValue(target, value);
        }

        public static T Get<T>(string field, object target)
        {
            var f = target.GetType().GetField(field);
            if (f == null) return default;
            return (T)f.GetValue(target);
        }
    }
}