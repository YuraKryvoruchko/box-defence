using System;

namespace BoxDefence
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EditorButtonAttribute : Attribute
    {
        /// <summary>
        /// Button text
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Add Button to Inspector
        /// </summary>
        /// <param Name="name">Button text</param>
        public EditorButtonAttribute(string name)
        {
            this.Name = name;
        }
    }
}
