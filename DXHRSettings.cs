using System;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.DXHR
{
    public partial class DXHRSettings : UserControl
    {
        public bool AutoReset { get; set; }
        public bool AutoStart { get; set; }
        public bool Prologue { get; set; }
        public bool Sarif { get; set; }
        public bool Detroit1 { get; set; }
        public bool FEMA { get; set; }
        public bool Hengsha1 { get; set; }
        public bool TaiYong1 { get; set; }
        public bool TaiYong2 { get; set; }
        public bool Picus { get; set; }
        public bool Detroit2 { get; set; }
        //Tongs Mission
        public bool TongsEnd { get; set; }
        //Normal Part
        public bool Hengsha2 { get; set; }
        //TML missions
        public bool TML_LeavingBoat { get; set; }
        public bool TML_UnderwaterElevator { get; set; }
        public bool TML_FinishedTML { get; set; }
        //Normal
        public bool Singapore { get; set; }
        public bool Panchaea { get; set; }

		public bool DC_Option_DisableFocusLossBehaviour { get; set; }


		private const bool DEFAULT_AUTORESET = false;
        private const bool DEFAULT_AUTOSTART = true;
        private const bool DEFAULT_PROLOGUE = true;
        private const bool DEFAULT_SARIF = true;
        private const bool DEFAULT_DETROIT1 = true;
        private const bool DEFAULT_FEMA = true;
        private const bool DEFAULT_HENGSHA1 = true;
        private const bool DEFAULT_TAIYONG1 = true;
        private const bool DEFAULT_TAIYONG2 = true;
        private const bool DEFAULT_PICUS = true;
        private const bool DEFAULT_DETROIT2 = true;
        private const bool DEFAULT_TONGSEND = true;            //Tongs Mission
        private const bool DEFAULT_HENGSHA2 = true;
        private const bool DEFAULT_TML_LEAVINGBOAT = true;     //TML
        private const bool DEFAULT_TML_UNDERWATERELEVATOR = true;
        private const bool DEFAULT_TML_FINISHED = true;
        private const bool DEFAULT_SINGAPORE = true;
        private const bool DEFAULT_PANCHAEA = true;

		private const bool DEFAULT_DC_OPTION_DISABLEFOCUSLOSSBEHAVIOUR = false;

		public DXHRSettings()
        {
            InitializeComponent();

            this.chkAutoReset.DataBindings.Add("Checked", this, nameof(AutoReset), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAutoStart.DataBindings.Add("Checked", this, nameof(AutoStart), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkPrologue.DataBindings.Add("Checked", this, nameof(Prologue), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkSarif.DataBindings.Add("Checked", this, nameof(Sarif), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkFEMA.DataBindings.Add("Checked", this, nameof(FEMA), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkDetroit1.DataBindings.Add("Checked", this, nameof(Detroit1), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHengsha1.DataBindings.Add("Checked", this, nameof(Hengsha1), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkTaiYong1.DataBindings.Add("Checked", this, nameof(TaiYong1), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkTaiYong2.DataBindings.Add("Checked", this, nameof(TaiYong2), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkPicus.DataBindings.Add("Checked", this, nameof(Picus), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkDetroit2.DataBindings.Add("Checked", this, nameof(Detroit2), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHengsha2.DataBindings.Add("Checked", this, nameof(Hengsha2), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkSingapore.DataBindings.Add("Checked", this, nameof(Singapore), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkPanchaea.DataBindings.Add("Checked", this, nameof(Panchaea), false, DataSourceUpdateMode.OnPropertyChanged);

            //Directors Cut
            this.chkDCTongsEnd.DataBindings.Add("Checked", this, nameof(TongsEnd), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkDC_TML_LeavingBoat.DataBindings.Add("Checked", this, nameof(TML_LeavingBoat), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkDC_TML_UnderwaterElevator.DataBindings.Add("Checked", this, nameof(TML_UnderwaterElevator), false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkDC_TML_EndOfDLC.DataBindings.Add("Checked", this, nameof(TML_FinishedTML), false, DataSourceUpdateMode.OnPropertyChanged);

            this.chkDC_Option_DisableFocusLoss.DataBindings.Add("Checked", this, nameof(DC_Option_DisableFocusLossBehaviour), false, DataSourceUpdateMode.OnPropertyChanged);

            // defaults
            this.AutoReset = DEFAULT_AUTORESET;
            this.AutoStart = DEFAULT_AUTOSTART;
            this.Prologue = DEFAULT_PROLOGUE;
            this.Sarif = DEFAULT_SARIF;
            this.FEMA = DEFAULT_FEMA;
            this.Detroit1 = DEFAULT_DETROIT1;
            this.Hengsha1 = DEFAULT_HENGSHA1;
            this.TaiYong1 = DEFAULT_TAIYONG1;
            this.TaiYong2 = DEFAULT_TAIYONG2;
            this.Picus = DEFAULT_PICUS;
            this.Detroit2 = DEFAULT_DETROIT2;
            this.TongsEnd = DEFAULT_TONGSEND;   //TongsEnd
            this.Hengsha2 = DEFAULT_HENGSHA2;
            this.TML_LeavingBoat = DEFAULT_TML_LEAVINGBOAT;
            this.TML_UnderwaterElevator = DEFAULT_TML_UNDERWATERELEVATOR;
            this.TML_FinishedTML = DEFAULT_TML_FINISHED;
            this.Singapore = DEFAULT_SINGAPORE;
            this.Panchaea = DEFAULT_PANCHAEA;
            this.DC_Option_DisableFocusLossBehaviour = DEFAULT_DC_OPTION_DISABLEFOCUSLOSSBEHAVIOUR;
        }

        public XmlNode GetSettings(XmlDocument doc)
        {
            XmlElement settingsNode = doc.CreateElement("Settings");

            settingsNode.AppendChild(ToElement(doc, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));

            settingsNode.AppendChild(ToElement(doc, nameof(AutoReset), this.AutoReset));
            settingsNode.AppendChild(ToElement(doc, nameof(AutoStart), this.AutoStart));
            settingsNode.AppendChild(ToElement(doc, nameof(Prologue), this.Prologue));
            settingsNode.AppendChild(ToElement(doc, nameof(Sarif), this.Sarif));
            settingsNode.AppendChild(ToElement(doc, nameof(Detroit1), this.Detroit1));
            settingsNode.AppendChild(ToElement(doc, nameof(FEMA), this.FEMA));
            settingsNode.AppendChild(ToElement(doc, nameof(Hengsha1), this.Hengsha1));
            settingsNode.AppendChild(ToElement(doc, nameof(TaiYong1), this.TaiYong1));
            settingsNode.AppendChild(ToElement(doc, nameof(TaiYong2), this.TaiYong2));
            settingsNode.AppendChild(ToElement(doc, nameof(Picus), this.Picus));
            settingsNode.AppendChild(ToElement(doc, nameof(Detroit2), this.Detroit2));
            settingsNode.AppendChild(ToElement(doc, nameof(TongsEnd), this.TongsEnd));
            settingsNode.AppendChild(ToElement(doc, nameof(Hengsha2), this.Hengsha2));
            settingsNode.AppendChild(ToElement(doc, nameof(TML_LeavingBoat), this.TML_LeavingBoat));
            settingsNode.AppendChild(ToElement(doc, nameof(TML_UnderwaterElevator), this.TML_UnderwaterElevator));
            settingsNode.AppendChild(ToElement(doc, nameof(TML_FinishedTML), this.TML_FinishedTML));
            settingsNode.AppendChild(ToElement(doc, nameof(Singapore), this.Singapore));
            settingsNode.AppendChild(ToElement(doc, nameof(Panchaea), this.Panchaea));
            settingsNode.AppendChild(ToElement(doc, nameof(DC_Option_DisableFocusLossBehaviour), this.DC_Option_DisableFocusLossBehaviour));

            return settingsNode;
        }

        public void SetSettings(XmlNode settings)
        {
            this.AutoReset = ParseBool(settings, nameof(AutoReset), DEFAULT_AUTORESET);
            this.AutoStart = ParseBool(settings, nameof(AutoStart), DEFAULT_AUTOSTART);
            this.Prologue = ParseBool(settings, nameof(Prologue), DEFAULT_PROLOGUE);
            this.Sarif = ParseBool(settings, nameof(Sarif), DEFAULT_SARIF);
            this.Detroit1 = ParseBool(settings, nameof(Detroit1), DEFAULT_DETROIT1);
            this.FEMA = ParseBool(settings, nameof(FEMA), DEFAULT_FEMA);
            this.Hengsha1 = ParseBool(settings, nameof(Hengsha1), DEFAULT_HENGSHA1);
            this.TaiYong1 = ParseBool(settings, nameof(TaiYong1), DEFAULT_TAIYONG1);
            this.TaiYong2 = ParseBool(settings, nameof(TaiYong2), DEFAULT_TAIYONG2);
            this.Picus = ParseBool(settings, nameof(Picus), DEFAULT_PICUS);
            this.Detroit2 = ParseBool(settings, nameof(Detroit2), DEFAULT_DETROIT2);
            this.TongsEnd = ParseBool(settings, nameof(TongsEnd), DEFAULT_TONGSEND);
            this.Hengsha2 = ParseBool(settings, nameof(Hengsha2), DEFAULT_HENGSHA2);
            this.TML_LeavingBoat = ParseBool(settings, nameof(TML_LeavingBoat), DEFAULT_TML_LEAVINGBOAT);
            this.TML_UnderwaterElevator = ParseBool(settings, nameof(TML_UnderwaterElevator), DEFAULT_TML_UNDERWATERELEVATOR);
            this.TML_FinishedTML = ParseBool(settings, nameof(TML_FinishedTML), DEFAULT_TML_FINISHED);
            this.Singapore = ParseBool(settings, nameof(Singapore), DEFAULT_SINGAPORE);
            this.Panchaea = ParseBool(settings, nameof(Panchaea), DEFAULT_PANCHAEA);
			this.DC_Option_DisableFocusLossBehaviour = ParseBool(settings, nameof(DC_Option_DisableFocusLossBehaviour), DEFAULT_DC_OPTION_DISABLEFOCUSLOSSBEHAVIOUR);
		}

		static bool ParseBool(XmlNode settings, string setting, bool default_ = false)
        {
			return settings[setting] != null ?
				(Boolean.TryParse(settings[setting].InnerText, out bool val) ? val : default_)
				: default_;
		}

        static XmlElement ToElement<T>(XmlDocument document, string name, T value)
        {
            XmlElement str = document.CreateElement(name);
            str.InnerText = value.ToString();
            return str;
        }
    }
}
