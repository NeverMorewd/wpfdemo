using System;

namespace Thunisoft.Framework.UI.Controls
{
    sealed class SerialDisposable
            : IDisposable
    {
        IDisposable content;

        public IDisposable Content
        {
            get { return content; }
            set
            {
                if (content != null)
                {
                    content.Dispose();
                }

                content = value;
            }
        }

        public void Dispose()
        {
            Content = null;
        }
    }
}
