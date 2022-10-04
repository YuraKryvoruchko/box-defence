using System;

namespace BoxDefence
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EditorButtonAttribute : Attribute
    {
        #region Properties

        public string ButtonText { get; private set; }

        #endregion

        #region Counstructor

        public EditorButtonAttribute(string buttonText)
        {
            ButtonText = buttonText;
        }

        #endregion
    }
}
