namespace ExcelHelpTaskPane
{
    partial class Help : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Help()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.HelpTab = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.toggleHelpBtn = this.Factory.CreateRibbonToggleButton();
            this.HelpTab.SuspendLayout();
            this.group1.SuspendLayout();
            // 
            // HelpTab
            // 
            this.HelpTab.Groups.Add(this.group1);
            this.HelpTab.Label = "帮助";
            this.HelpTab.Name = "HelpTab";
            // 
            // group1
            // 
            this.group1.Items.Add(this.toggleHelpBtn);
            this.group1.Label = "获得使用Excel的帮助";
            this.group1.Name = "group1";
            // 
            // toggleHelpBtn
            // 
            this.toggleHelpBtn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.toggleHelpBtn.Image = global::ExcelHelpTaskPane.Properties.Resources.help;
            this.toggleHelpBtn.Label = "Help";
            this.toggleHelpBtn.Name = "toggleHelpBtn";
            this.toggleHelpBtn.ShowImage = true;
            this.toggleHelpBtn.Tag = "选中来显示帮助文档";
            this.toggleHelpBtn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleHelpBtn_Click);
            // 
            // Help
            // 
            this.Name = "Help";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.HelpTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Help_Load);
            this.HelpTab.ResumeLayout(false);
            this.HelpTab.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab HelpTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleHelpBtn;
    }

    partial class ThisRibbonCollection
    {
        internal Help Help
        {
            get { return this.GetRibbon<Help>(); }
        }
    }
}
