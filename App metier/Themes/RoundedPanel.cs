using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WPFUI.Themes
{
    class RoundedPanel : RoundedControl
    {
        private const int DEFAULT_GAP = 3;

        private int _gap = DEFAULT_GAP;

        private Size _originalScrollSize = new Size();


        private Dictionary<Guid, Control> _controls = new Dictionary<Guid, Control>();

        private ScrollViewer scrollViewer = new ScrollViewer();
        private StackPanel panel = new StackPanel();
        

        // accessor
        public Control this[Guid g]
        { 
            get { if (_controls.ContainsKey(g)) return _controls[g]; else return null; }
            set { if (_controls.ContainsKey(g)) _controls[g] = value; }
        }


        // constructor
        public RoundedPanel() : base()
        {
            this.Canvas.Children.Add(scrollViewer);
            scrollViewer.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            scrollViewer.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            this.Canvas.Margin = new Thickness(this.BorderSize);
            scrollViewer.Content = panel;
            scrollViewer.CanContentScroll = true;
            scrollViewer.Focusable = true;
            scrollViewer.IsEnabled = true;
            scrollViewer.IsManipulationEnabled = true;

            for (int i = 0; i < 10; i++)
            {
                var temp = new JobChoiceUC();
                temp.Padding = new Thickness(_gap);
                temp.Width = panel.Width - 40 * _gap - panel.Margin.Right;
                panel.Children.Add(temp);
            }


            
        }


        public bool Contains(Guid guid) => _controls.ContainsKey(guid);

        public Guid Add(Control ctrl)
        {
            Guid g = Guid.NewGuid();

            _controls.Add(g, ctrl);

            ctrl.VerticalContentAlignment = VerticalAlignment.Top;
            ctrl.VerticalAlignment = VerticalAlignment.Top;

            return g;
        }

        public void Remove(Control ctrl)
        {
            Guid g = Guid.Empty;
            foreach (var item in _controls)
            {
                if(item.Value == ctrl)
                {
                    g = item.Key;
                    break;
                }
            }

            if (g != Guid.Empty)
            {
               // scrollViewer.Children.Remove(_controls[g]);
                _controls.Remove(g);
            }
        }

        public void Remove(Guid guid)
        {
            if (_controls.ContainsKey(guid))
            {
                //scrollViewer.Children.Remove(_controls[guid]);
                _controls.Remove(guid);
            }
        }

    }
}
