namespace Web.Helpers
{
    public static class CastingHelper
    {
        public static T CastAs<T>(this object @object)
        {
            return (T) @object;
        }
    }
}