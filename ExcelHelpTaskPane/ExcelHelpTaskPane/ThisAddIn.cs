using System;
using Microsoft.Office.Core;

namespace ExcelHelpTaskPane
{
    public partial class ThisAddIn
    {
        // 定义一个任务窗体
        internal Microsoft.Office.Tools.CustomTaskPane helpTaskPane;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            // 把自定义窗体添加到CustomTaskPanes集合中
            // ExcelHelp 是一个自定义控件类
            helpTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(new ExcelHelp(""), "Excel Help");
          
            // 使任务窗体可见
            //helpTaskPane.Visible = true;
            // 通过DockPosition属性来控制任务窗体的停靠位置，
            // 设置为 MsoCTPDockPosition.msoCTPDockPositionRight这个代表停靠到右边，这个值也是默认值
            //helpTaskPane.DockPosition = MsoCTPDockPosition.msoCTPDockPositionRight;
            
            // 当用户点击 “Excel help”右上角的X按钮关闭时，我们需要同步选项卡上button的状态 
            helpTaskPane.VisibleChanged += new EventHandler(helpTaskPane_VisibleChanged);

            // 添加上下文菜单
            AddToCellMenu();
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }
       
        // 用户点击  "Excel Help" 侧边栏右上角的X按钮关闭它时
        // 我们需要正确同步 “帮助”按钮的状态
        // 我们可以通过处理 “Excel Help”侧边栏的VisualChanged 事件完成
        private void helpTaskPane_VisibleChanged(object sender, EventArgs e)
        {
            // 获得Help Ribbon 对象
            Help helpRibbon = Globals.Ribbons.GetRibbon<Help>();
            // 同步Help Ribbon下的"帮助"按钮的状态
            helpRibbon.toggleHelpBtn.Checked = Globals.ThisAddIn.helpTaskPane.Visible;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion

        // 向Cell上下文菜单添加按钮
        private void AddToCellMenu()
        {
            CommandBar contextMenu = null;

            // 如果存在首先删除控件，避免重复
            DeleteFromCellMenu();

            // 添加一个自定义按钮到单元格上下文菜单中
            contextMenu =Application.CommandBars["Cell"];
            CommandBarButton commandBarbtn = (CommandBarButton)contextMenu.Controls.Add(MsoControlType.msoControlButton, Before: 1);
            commandBarbtn.Tag = "Help_Tag";
            commandBarbtn.Caption = "查看帮助";
            commandBarbtn.FaceId = 49;
            commandBarbtn.Click+=new _CommandBarButtonEvents_ClickEventHandler(commandBarbtn_Click);  
        }

        private void DeleteFromCellMenu()
        {
            // 设置为单元格上下文菜单
            CommandBar contextMenu=Application.CommandBars["Cell"];
            
            // 遍历单元格上下文菜单中所有控件
            // 如果存在我们自定义的控件,就删除它
            foreach (CommandBarControl ctrl in contextMenu.Controls)
            {
                if (ctrl.Tag == "Help_Tag"||ctrl.Caption=="查看帮助")
                {
                    ctrl.Delete();
                }
            }
        }

        private void commandBarbtn_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            if (Ctrl.Caption == "查看帮助")
            {
                // 使任务窗体显示出来
                Globals.ThisAddIn.helpTaskPane.Visible = true;
                Ctrl.Caption = "隐藏帮助";
            }
            else
            {
                Globals.ThisAddIn.helpTaskPane.Visible = false;
                Ctrl.Caption = "查看帮助";
            }
        }
    }
}
