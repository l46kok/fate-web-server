namespace FateWebServer.Utility
{
    public static class CSSColorizer
    {
        public static string GetKDAColor(double kda)
        {
            if (kda >= 5.0)
            {
                return "kdagreen";
            }
            if (kda >= 4.0)
            {
                return "kdablue";
            }
            if (kda >= 3.0)
            {
                return "kdagreen";
            }
            if (kda >= 2.0)
            {
                return "kdanormal";
            }
            return "kdalow";
        }
    }
}
