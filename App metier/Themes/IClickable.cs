using System;
using System.Collections.Generic;
using System.Text;

namespace WPFUI.Themes
{
    public interface IClickable
    {
        //public event GuidSenderHandler OnClickGuid;
        public event EventHandler OnClick;
    }

    public interface IActivable
    {
        public event EventHandler OnActivStateChanged;

        public bool IsActiv { get; set; }
        public bool IsAutoCheck { get; set; }
    }

    public delegate void GuidSenderHandler(object sender, GuidSelecEventArg e);

    public class GuidSelecEventArg : EventArgs
    {
        private Guid _guid;

        public Guid Guid => _guid;

        public GuidSelecEventArg(Guid guid)
        {
            _guid = guid;
        }
    }
}
