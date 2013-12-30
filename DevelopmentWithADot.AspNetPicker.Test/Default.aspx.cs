using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevelopmentWithADot.AspNetPicker.Test
{
	public partial class _Default : Page
	{
		protected void OnClick(object sender, EventArgs e)
		{
			var valuePath = this.picker.SelectedValuePath;

			this.path.Text = valuePath;
		}

		protected void OnPopulateNode(object sender, PopulateTreeNodeEventArgs e)
		{
			for (var i = 0; i < 3; ++i)
			{
				var child = new TreeNode(((Char)('A' + i)).ToString(), String.Concat(e.Node.Depth, ".", i));
				child.ImageToolTip = child.Text;
				child.ImageUrl = String.Format("~/{0}.png", e.Node.Depth + 1);
				e.Node.ChildNodes.Add(child);
				e.CanSelect = (e.Node.Depth == 3);
				e.CanHaveChildren = (e.Node.Depth < 3);
			}
		}

		protected void OnSelectClick(object sender, EventArgs e)
		{
			this.picker.SelectNode(this.path.Text);
		}
	}
}