using Microsoft.Office.Tools.Ribbon;

namespace ExcelHelpTaskPane
{
    public partial class Help
    {
        private void Help_Load(object sender, RibbonUIEventArgs e)
        {
        }

        // 帮助选项卡中toggleButton的单击事件
        private void toggleHelpBtn_Click(object sender, RibbonControlEventArgs e)
        {
            // 通过toggleHelpButton的选中状态来控制帮助任务栏的显示和隐藏
            Globals.ThisAddIn.helpTaskPane.Visible = toggleHelpBtn.Checked;
        }
    }
}
