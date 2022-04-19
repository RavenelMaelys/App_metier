using System;
using System.Collections.Generic;
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
    /// Interaction logic for JobChoiceUC.xaml
    /// </summary>
    public partial class JobChoiceUC : UserControl , IClickable , IActivable
    {

        public bool IsAutoCheck { get => backPnl.IsAutoCheck; set => backPnl.IsAutoCheck = value; }
        public bool IsActiv { get => backPnl.IsActiv; set => backPnl.IsActiv = value; }
        public bool IsSelected { get => cBtn.IsActiv; set => cBtn.IsActiv = value; }

        public double ProgressPourcent { get => backPnl.ProgressPourcent; set { backPnl.ProgressPourcent = value; InvalidateVisual(); } }

        public event EventHandler OnClick;
        public event EventHandler OnActivStateChanged { add => backPnl.OnActivStateChanged += value; remove => backPnl.OnActivStateChanged -= value; }
        public event GuidSenderHandler OnPlayClick;
        public event GuidSenderHandler OnPauseClick;
        public event GuidSenderHandler OnStopClick;

        public string Title { get => backPnl.Text; set => backPnl.Text = value; }

        public JobChoiceUC(string name = "jobName")
        {
            InitializeComponent();
            this.
            Title = name;

            backPnl.OnClick -= BackPnl_OnClick;
            backPnl.OnClick += BackPnl_OnClick;

            rBtn_play.OnClick -= RBtn_play_OnClick;
            rBtn_play.OnClick += RBtn_play_OnClick;

            rBtn_pause.OnClick -= RBtn_pause_OnClick;
            rBtn_pause.OnClick += RBtn_pause_OnClick;

            rBtn_stop.OnClick -= RBtn_stop_OnClick;
            rBtn_stop.OnClick += RBtn_stop_OnClick;
        }

        private void RBtn_stop_OnClick(object sender, EventArgs e) => OnStopClick?.Invoke(this, new GuidSelecEventArg((Guid)this.Tag));

        private void RBtn_pause_OnClick(object sender, EventArgs e) => OnPauseClick?.Invoke(this, new GuidSelecEventArg((Guid)this.Tag));

        private void RBtn_play_OnClick(object sender, EventArgs e) => OnPlayClick?.Invoke(this, new GuidSelecEventArg((Guid)this.Tag));

        private void BackPnl_OnClick(object sender, EventArgs e)
        {
            OnClick?.Invoke(this, e);
        }


        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);


           // this.RenderSize = ;
        }

    }
}
