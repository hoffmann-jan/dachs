namespace dachsXll
{
    /// <summary>
    /// Global static variables.
    /// </summary>
    public static class Global
    {
        private static Languages _Language;
        /// <summary>
        /// System language for localization.
        /// </summary>
        public static Languages Language
        {
            get { return _Language; }
            set { _Language = value; }
        }

        /// <summary>
        /// Available Languages.
        /// </summary>
        public enum Languages
        {
            /// <summary>
            /// English
            /// </summary>
            English = 0,
            /// <summary>
            /// German
            /// </summary>
            German = 1
        }
    }
}
