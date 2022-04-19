using AyoControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFUI.Themes
{
    /// <summary>
    /// Interaction logic for ScrollPanel.xaml
    /// </summary>
    public partial class ScrollPanel : UserControl
    {
        // event
        public event GuidSenderHandler OnItemSelected;

        // const
        private const int DEFAULT_GAP = 3;


        #region prop dependcy

        #endregion


        #region private member

        private int _gap = DEFAULT_GAP;

        private Guid? _activGuid = null;

        private StackPanel _stack = new StackPanel();
        #endregion

        #region public attribute


        public int CornerRadius { get => Back.CornerRadius; set { Back.CornerRadius = value; InvalidateVisual(); } }
        public int BorderSize { get => Back.BorderSize; set { Back.BorderSize = value; InvalidateVisual(); } }

        public Guid? SelectedGuid => _activGuid;


        public ERoundedType RoundedType { get => Back.RoundedType; set { Back.RoundedType = value; InvalidateVisual(); } }
        public ERoundedFlag RoundedFlag { get => Back.RoundedFlag; set { Back.RoundedFlag = value; InvalidateVisual(); } }


        public Color? ColorBackEnable { get => Back.ColorBackEnable; set { Back.ColorBackEnable = value; InvalidateVisual(); } }
        public Color? ColorBackDisable { get => Back.ColorBackDisable; set { Back.ColorBackDisable = value; InvalidateVisual(); } }
        public Color? ColorBorderEnable { get => Back.ColorBorderEnable; set { Back.ColorBorderEnable = value; InvalidateVisual(); } }
        public Color? ColorBorderDisable { get => Back.ColorBorderEnable; set { Back.ColorBorderEnable = value; InvalidateVisual(); } }


        public Control this[Guid g]
        {
            get => GetControl(g);
            set => SetControl(g, value);
        }

        public Control[] Controls => GetControls();

        #endregion

        // constructort
        public ScrollPanel()
        {
            InitializeComponent();
            (Back.Content as ScrollViewer).Content = _stack;
        }

        #region METHODS

        #region basic manipulation
        public void Remove(Control ctrl)
        {
            for (int i = 0; i < _stack.Children.Count; i++)
            {
                if ((_stack.Children[i] as Control) == ctrl)
                {
                    _stack.Children.RemoveAt(i);
                    return;
                }
            }
        }

        public void Remove(Guid guid)
        {
            for (int i = 0; i < _stack.Children.Count; i++)
            {
                if ( (Guid)(_stack.Children[i] as Control).Tag == guid)
                {
                    _stack.Children.RemoveAt(i);
                    return;
                }
            }
        }

        public void Add(UserControl ctrl,  Guid? g = null)
        {   
            Guid guid = g.HasValue? g.Value : Guid.NewGuid();
            _stack.Children.Add(ctrl);
            ctrl.Padding = new Thickness(_gap);
            ctrl.Width = _stack.Width;
            ctrl.Height += 2*_gap;
            ctrl.Tag = guid;

            if (ctrl is IClickable)
            {
                (ctrl as IClickable).OnClick -= Item_OnClick;
                (ctrl as IClickable).OnClick += Item_OnClick;
            }
            _stack.InvalidateVisual();
        }

        public void Clear()
        {
            _stack.Children.Clear();
        }

        public bool Contains(Control ctrl)
        {
            foreach (var item in _stack.Children)
            {
                if (!(item is Control))
                    continue;

                if ((item as Control).Tag.Equals(ctrl))
                    return true;
            }
            return false;
        }

        public bool Contains(Guid guid)
        {
            foreach (var item in _stack.Children)
            {
                if (!(item is Control))
                    continue;

                if ((item as Control).Tag.Equals(guid))
                    return true;
            }
            return false;
        }

        #endregion



        #region advanced manipulation
        public void SelecteItem(Guid guid)
        {
            foreach (var item in _stack.Children)
            {
                if (!(item is IActivable))
                    continue;

                if ((Guid)(item as Control).Tag == guid)
                {
                    (item as IActivable).IsActiv = true;
                    _activGuid = guid;
                }
                else

                    (item as IActivable).IsActiv = false;
            }
        }
        public void ActivAll(bool state)
        {
            foreach (var item in _stack.Children)
            {
                if (!(item is IActivable))
                    continue;

                (item as IActivable).IsActiv = state;
            }
        }
        public void UpSelection(Guid? guid = null)
        {
            if (guid != null)
                SelecteItem(guid.Value);


            if (_activGuid == null)
                return;

            if (!Contains(_activGuid.Value))
                return;

            if ((Guid)(_stack.Children[0] as Control).Tag == _activGuid.Value)
                return;

            int iSelected = -1;
            int iUpper = -1;
            for (int i = 1; i < _stack.Children.Count; i++)
            {
                if ((Guid)(_stack.Children[i] as Control).Tag == _activGuid)
                {
                    iSelected = i;
                    iUpper = i - 1;
                    break;
                }
            }
            if (iSelected == -1 || iUpper == -1)
                return;

            var tempUpp = _stack.Children[iUpper];
            var tempSel = _stack.Children[iSelected];

            _stack.Children.RemoveAt(iSelected);
            _stack.Children.RemoveAt(iUpper);

            if (_stack.Children.Count > iSelected)
                _stack.Children.Insert(iSelected, tempUpp);
            else
                _stack.Children.Add(tempUpp);


            if (_stack.Children.Count > iUpper)
                _stack.Children.Insert(iUpper, tempSel);
            else
                _stack.Children.Add(tempSel);

        }
        public void DownSelection(Guid? guid = null)
        {
            if (guid != null)
                SelecteItem(guid.Value);

            if (_activGuid == null)
                return;

            if (!Contains(_activGuid.Value))
                return;

            if ((Guid)(_stack.Children[_stack.Children.Count - 1] as Control).Tag == _activGuid.Value)
                return;


            int iSelected = -1;
            int iLower = -1;
            for (int i = 0; i < _stack.Children.Count - 1; i++)
            {
                if ((Guid)(_stack.Children[i] as Control).Tag == _activGuid)
                {
                    iSelected = i;
                    iLower = i + 1;
                    break;
                }
            }
            if (iSelected == -1 || iLower == -1)
                return;

            var tempLow = _stack.Children[iLower];
            var tempSel = _stack.Children[iSelected];

            _stack.Children.RemoveAt(iLower);
            _stack.Children.RemoveAt(iSelected);


            if (_stack.Children.Count > iSelected)
                _stack.Children.Insert(iSelected, tempLow);
            else
                _stack.Children.Add(tempLow);

            if (_stack.Children.Count > iLower)
                _stack.Children.Insert(iLower, tempSel);
            else
                _stack.Children.Add(tempSel);
        }

        private void Item_OnClick(object sender, EventArgs e)
        {
            if (!(sender is Control))
                return;

            if (!((sender as Control).Tag is Guid))
                return;

            Guid g = (Guid)(sender as Control).Tag;
            _activGuid = g;
            SelecteItem(g);


            OnItemSelected?.Invoke(this, new GuidSelecEventArg(g));
        }

        #endregion




        #region utility

        private Control GetControl(Guid guid)
        {
            foreach (var item in _stack.Children)
            {
                if ((Guid)(item as Control).Tag == guid)
                    return item as Control;
            }
            return null;
        }

        private void SetControl(Guid guid, Control value)
        {
            for (int i = 0; i < _stack.Children.Count; i++)
            {
                if ((Guid)((_stack.Children[i] as Control).Tag) == guid)
                {
                    _stack.Children[i] = value;
                    return;
                }
            }
        }

        private Control[] GetControls()
        {
            List<Control> output = new List<Control>();

            foreach (var item in _stack.Children)
            {
                output.Add(item as Control);
            }

            return output.ToArray();
        }

        #endregion
        
        
        #endregion

    }


}
