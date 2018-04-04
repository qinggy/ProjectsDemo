using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace SmartReportTool
{
    public partial class Form1 : Form
    {
        private bool isMouseDown = false;
        private Point FormLocation;     //form的location
        private Point mouseOffset;      //鼠标的按下位置

        List<Mapping> MappingItems = new List<Mapping>();
        List<Mapping> CustomItems = new List<Mapping>();
        string FILEPATH = string.Empty;

        public Form1()
        {
            InitializeComponent();
            SetToolTip();
            DataGridViewSetStyle();
        }

        private void SetToolTip()
        {
            ToolTip prompt = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 500,
                ReshowDelay = 500,
                ShowAlways = true//,
                //ToolTipIcon = ToolTipIcon.Info,

            };

            prompt.SetToolTip(this.settingbtn, "加载数据源");
            //prompt.Show("加载数据源", this.settingbtn);

            ToolTip close = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 500,
                ReshowDelay = 500,
                ShowAlways = true//,
                //ToolTipIcon = ToolTipIcon.Warning
            };
            close.SetToolTip(this.closebtn, "退出");
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                FormLocation = this.Location;
                mouseOffset = Control.MousePosition;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            int _x = 0;
            int _y = 0;
            if (isMouseDown)
            {
                Point pt = Control.MousePosition;
                _x = mouseOffset.X - pt.X;
                _y = mouseOffset.Y - pt.Y;

                this.Location = new Point(FormLocation.X - _x, FormLocation.Y - _y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认退出当前程序？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void settingbtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog()
            {
                RestoreDirectory = true,
                Filter = "配置文件|*.xml"
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                FILEPATH = openDialog.FileName;
                this.filename.Text = FILEPATH;
                string error = "";
                try
                {
                    XmlDocument xmlDom = new XmlDocument();
                    xmlDom.Load(openDialog.FileName);
                    XmlNode node = xmlDom.SelectSingleNode("ReportInfo/CellMappings");
                    if (node != null && node.ChildNodes.Count > 0)
                    {
                        MappingItems.Clear();
                        foreach (XmlNode child in node.ChildNodes)
                        {
                            Mapping map = new Mapping
                            {
                                CellAddress = child.Attributes["CellAddress"].Value,
                                EndTime = DateTime.Parse(child.Attributes["EndTime"].Value),
                                StartTime = DateTime.Parse(child.Attributes["StartTime"].Value),
                                FieldTypeId = child.Attributes["FieldTypeId"].Value,
                                PriceType = child.Attributes["PriceType"].Value,
                                SourceType = child.Attributes["SourceType"].Value,
                                ServiceURL = child.Attributes["ServiceURL"].Value
                            };
                            error = map.CellAddress;
                            MappingItems.Add(map);
                        }

                        if (MappingItems.Count > 0)
                        {
                            LoadGridViewDataSource(MappingItems);
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemLogHelper.Logger.Error(ex + "\r\n错误数据前一个仪表：" + error, ex);
                }
            }
        }

        void LoadGridViewDataSource(List<Mapping> entities)
        {
            foreach (var entity in entities)
            {
                DataGridViewRow dgvr = new DataGridViewRow();
                DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                cell.Value = cellmappingGrid.RowCount + 1;
                dgvr.Cells.Add(cell);

                cell = new DataGridViewTextBoxCell();
                cell.Value = entity.CellAddress;
                dgvr.Cells.Add(cell);

                cell = new DataGridViewTextBoxCell();
                cell.Value = entity.FieldTypeId;
                dgvr.Cells.Add(cell);

                cell = new DataGridViewTextBoxCell();
                cell.Value = entity.StartTime;
                dgvr.Cells.Add(cell);

                cell = new DataGridViewTextBoxCell();
                cell.Value = entity.EndTime;
                dgvr.Cells.Add(cell);

                cell = new DataGridViewTextBoxCell();
                cell.Value = entity.PriceType;
                dgvr.Cells.Add(cell);

                cell = new DataGridViewTextBoxCell();
                cell.Value = entity.SourceType;
                dgvr.Cells.Add(cell);

                cellmappingGrid.Rows.Add(dgvr);
            }
        }

        void DataGridViewSetStyle()
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.ReadOnly = true;

            column.HeaderText = "编号";
            column.Width = 60;
            column.DisplayIndex = 0;
            column.Name = "Id";
            cellmappingGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.ReadOnly = true;
            column.HeaderText = "地址";
            column.DisplayIndex = 1;
            column.Name = "CellAddress";
            column.MinimumWidth = 160;
            cellmappingGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.ReadOnly = true;
            column.HeaderText = "参数编号";
            column.DisplayIndex = 2;
            column.Name = "FieldTypeId";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            cellmappingGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.ReadOnly = true;
            column.MinimumWidth = 140;
            column.HeaderText = "开始时间";
            column.DisplayIndex = 3;
            column.Name = "StartTime";
            column.DefaultCellStyle.Format = "yyyy-MM-dd";
            cellmappingGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.ReadOnly = true;
            column.MinimumWidth = 140;
            column.HeaderText = "结束时间";
            column.DisplayIndex = 4;
            column.Name = "EndTime";
            column.DefaultCellStyle.Format = "yyyy-MM-dd";
            cellmappingGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.ReadOnly = true;
            column.HeaderText = "峰谷平";
            column.DisplayIndex = 5;
            column.Name = "PriceType";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            cellmappingGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.ReadOnly = true;
            column.HeaderText = "报表类型";
            column.DisplayIndex = 6;
            column.Name = "SourceType";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            cellmappingGrid.Columns.Add(column);
        }

        private void seachbtn_Click(object sender, EventArgs e)
        {
            var txt = this.searchtxt.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(txt))
            {
                CustomItems = (from n in MappingItems where n.CellAddress.Contains(txt) select n).ToList();
                if (CustomItems != null)
                {
                    cellmappingGrid.Rows.Clear();
                    LoadGridViewDataSource(CustomItems);
                }
            }
            else
            {
                cellmappingGrid.Rows.Clear();
                LoadGridViewDataSource(MappingItems);
            }
        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认你所选择的所有填充项没有任何疑问，都是你想要修改的？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var sTime = this.sTime.Text;
                var eTime = this.eTime.Text;
                XmlDocument xmlDom = new XmlDocument();
                xmlDom.Load(FILEPATH);
                foreach (var item in CustomItems)
                {
                    item.StartTime = DateTime.Parse(sTime + " " + item.StartTime.Hour + ":" + item.StartTime.Minute + ":" + item.StartTime.Second);
                    item.EndTime = DateTime.Parse(eTime + " " + item.EndTime.Hour + ":" + item.EndTime.Minute + ":" + item.EndTime.Second);
                    XmlNode node = xmlDom.SelectSingleNode("ReportInfo/CellMappings/Mapping[@CellAddress='" + item.CellAddress + "']");
                    node.Attributes["StartTime"].Value = item.StartTime.ToString();
                    node.Attributes["EndTime"].Value = item.EndTime.ToString();
                }

                cellmappingGrid.Rows.Clear();
                LoadGridViewDataSource(CustomItems);
                //save to xml file
                xmlDom.Save(FILEPATH);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new about().ShowDialog();
        }
    }
}
