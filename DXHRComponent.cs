using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using LiveSplit.UI.Components;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;

namespace LiveSplit.DXHR
{
    class DXHRComponent : LogicComponent
    {
        public override string ComponentName
        {
            get { return "DXHR"; }
        }

        public DXHRSettings Settings { get; set; }

        public bool Disposed { get; private set; }
        public bool IsLayoutComponent { get; private set; }

        private TimerModel _timer;
        private GameMemory _gameMemory;
        private LiveSplitState _state;

        public DXHRComponent(LiveSplitState state, bool isLayoutComponent)
        {
            _state = state;
            this.IsLayoutComponent = isLayoutComponent;

            this.Settings = new DXHRSettings();

            _timer = new TimerModel { CurrentState = state };
            _timer.CurrentState.OnStart += timer_OnStart;

            _gameMemory = new GameMemory(this.Settings);
            _gameMemory.OnFirstLevelLoading += gameMemory_OnFirstLevelLoading;
            _gameMemory.OnPlayerGainedControl += gameMemory_OnPlayerGainedControl;
            _gameMemory.OnLoadStarted += gameMemory_OnLoadStarted;
            _gameMemory.OnLoadFinished += gameMemory_OnLoadFinished;
            _gameMemory.OnSplitCompleted += gameMemory_OnSplitCompleted;
            state.OnStart += State_OnStart;
            _gameMemory.StartMonitoring();
        }

        public override void Dispose()
        {
            this.Disposed = true;

            _state.OnStart -= State_OnStart;
            _timer.CurrentState.OnStart -= timer_OnStart;

            if (_gameMemory != null)
            {
                _gameMemory.Stop();
            }

        }

        void State_OnStart(object sender, EventArgs e)
        {
            _gameMemory.resetSplitStates();
        }

        void timer_OnStart(object sender, EventArgs e)
        {
            _timer.InitializeGameTime();
        }

    void gameMemory_OnFirstLevelLoading(object sender, EventArgs e)
        {
            if (this.Settings.AutoReset)
            {
                _timer.Reset();
            }
        }

        void gameMemory_OnPlayerGainedControl(object sender, EventArgs e)
        {
            if (this.Settings.AutoStart)
            {
                _timer.Start();
            }
        }

        void gameMemory_OnLoadStarted(object sender, EventArgs e)
        {
            _state.IsGameTimePaused = true;
        }

        void gameMemory_OnLoadFinished(object sender, EventArgs e)
        {
            _state.IsGameTimePaused = false;
        }

        void gameMemory_OnSplitCompleted(object sender, GameMemory.SplitArea split, uint frame)
        {
            Debug.WriteLineIf(split != GameMemory.SplitArea.None, String.Format("[NoLoads] Trying to split {0}, State: {1} - {2}", split, _gameMemory.splitStates[(int)split], frame));
            if (_state.CurrentPhase == TimerPhase.Running && !_gameMemory.splitStates[(int)split] &&
                ((split == GameMemory.SplitArea.Prologue && this.Settings.Prologue) ||
                (split == GameMemory.SplitArea.Sarif && this.Settings.Sarif) ||
                (split == GameMemory.SplitArea.Detroit1 && this.Settings.Detroit1) ||
                (split == GameMemory.SplitArea.FEMA && this.Settings.FEMA) ||
                (split == GameMemory.SplitArea.Hengsha1 && this.Settings.Hengsha1) ||
                (split == GameMemory.SplitArea.TaiYong1 && this.Settings.TaiYong1) ||
                (split == GameMemory.SplitArea.TaiYong2 && this.Settings.TaiYong2) ||
                (split == GameMemory.SplitArea.Picus && this.Settings.Picus) ||
                (split == GameMemory.SplitArea.Detroit2 && this.Settings.Detroit2) ||
                (split == GameMemory.SplitArea.TongsEnd && this.Settings.TongsEnd) ||
                (split == GameMemory.SplitArea.Hengsha2 && this.Settings.Hengsha2) ||
                (split == GameMemory.SplitArea.DLCBoat && this.Settings.TML_LeavingBoat) ||
                (split == GameMemory.SplitArea.DLCUnderwater && this.Settings.TML_UnderwaterElevator) ||
                (split == GameMemory.SplitArea.DLCEnd && this.Settings.TML_FinishedTML) ||
                (split == GameMemory.SplitArea.Singapore && this.Settings.Singapore) ||
                (split == GameMemory.SplitArea.Panchaea && this.Settings.Panchaea)))
            {
                Trace.WriteLine(String.Format("[NoLoads] {0} Split - {1}", split, frame));
                _timer.Split();
                _gameMemory.splitStates[(int)split] = true;
            }
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            return this.Settings.GetSettings(document);
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return this.Settings;
        }

        public override void SetSettings(XmlNode settings)
        {
            this.Settings.SetSettings(settings);
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }
        //public override void RenameComparison(string oldName, string newName) { }
    }
}
