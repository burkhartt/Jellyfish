namespace Utilities {
    public static class CastingHelper {
        public static T CastAs<T>(this object @object) {
            return (T) @object;
        }
    }
}