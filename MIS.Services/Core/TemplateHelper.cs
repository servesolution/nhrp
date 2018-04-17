using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.StringTemplate;

namespace MIS.Services.Core
{
    class TemplateHelper
    {
    }

    public class LatterFormatTemplate : IDisposable
    {
        StringTemplate st = null;
        private Boolean disposed = false;

        public LatterFormatTemplate(String template)
        {
            st = new StringTemplate(template);
        }

        /// <summary>
        /// It is used to push our object to template to be rendered.
        /// </summary>
        /// <param name="name">Name of object</param>
        /// <param name="value">Object to be passed</param>
        public void AddAttribute(String name, Object value)
        {
            st.SetAttribute(name, value);
        }

        /// <summary>
        /// It is used to format or renderer specified object in required format.
        /// </summary>
        /// <param name="renderer">Specific renderer which is created separately inheriting AttributeRenderer</param>
        public void AddRenderer(AttributeRenderer renderer)
        {
            st.RegisterAttributeRenderer(renderer.AttrType, renderer);
        }

        /// <summary>
        /// Generates the report to template with values to be filled in the placeholders.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return st.ToString();
        }

        #region IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    st = null;
                }
                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.

                // Note disposing has been done.
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~LatterFormatTemplate()
        {
            Dispose(false);
        }

        #endregion
    }

     public abstract class AttributeRenderer : IAttributeRenderer
    {
        public abstract Type AttrType { get; }
        public abstract string ToString(object o, string formatName);
        public abstract string ToString(object o);
    }
}
