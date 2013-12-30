using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevelopmentWithADot.AspNetPicker
{
	public sealed class PopulateTreeNodeEventArgs : EventArgs
	{
		public PopulateTreeNodeEventArgs(TreeNode node)
		{
			this.Node = node;
			this.CanHaveChildren = true;
			this.CanSelect = true;
		}

		public TreeNode Node { get; private set; }

		public Boolean CanSelect { get; set; }

		public Boolean CanHaveChildren { get; set; }
	}

	/*class PickerTreeNode : TreeNode
	{
		public PickerTreeNode(String text, String value, String imageUrl) : base(text, value, imageUrl)
		{
		}

		public String OnNodeClientClick { get; set; }

		protected override void RenderPostText(HtmlTextWriter writer)
		{
			if (String.IsNullOrWhiteSpace(this.OnNodeClientClick) == false)
			{
				var attributesList = writer.GetType().GetField("_attrList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(writer) as IList;

				foreach (var attribute in attributesList)
				{
					var key = attribute.GetType().GetField("key").GetValue(attribute).ToString();
					var valueProperty = attribute.GetType().GetField("value");
					var value = valueProperty.GetValue(attribute) as String;

					if (key == HtmlTextWriterAttribute.Href.ToString())
					{
						value = String.Concat("javascript:", this.OnNodeClientClick, ";", value.Substring("javascript:".Length));
						//valueProperty.SetValue(attribute, value);
						writer.AddAttribute(HtmlTextWriterAttribute.Href.ToString().ToLower(), value);
						break;
					}
				}
			}

			base.RenderPostText(writer);
		}
	}*/

	public class Picker : WebControl, INamingContainer
	{
		private readonly Panel panel = new Panel();
		private readonly TreeView tree = new TreeView();
		private readonly System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
		private readonly HyperLink link = new HyperLink();

		public Picker()
		{
			//these are here so that they can be overwriten on markup
			this.RootValue = 0.ToString();

			this.tree.Style[HtmlTextWriterStyle.Position] = "absolute";
			this.tree.Style[HtmlTextWriterStyle.Overflow] = "auto";
			this.tree.Style[HtmlTextWriterStyle.Display] = "none";
			this.tree.Style["max-height"] = Unit.Pixel(200).ToString();
			this.tree.Style["max-width"] = Unit.Pixel(300).ToString();
			this.tree.Style[HtmlTextWriterStyle.Width] = this.tree.Style["max-height"];
		}

		public event EventHandler<PopulateTreeNodeEventArgs> PopulateTreeNode;

		[DefaultValue("")]
		public String OnNodeClientClick { get; set; }

		[DefaultValue("")]
		public String RootText { get; set; }

		[DefaultValue("")]
		[UrlProperty("*.png;*.gif;*.jpg")]
		public String RootImageUrl { get; set; }

		[DefaultValue("0")]
		protected String RootValue { get; set; }

		[DefaultValue(TreeNodeTypes.None)]
		public TreeNodeTypes ShowCheckBoxes
		{
			get
			{
				return (this.tree.ShowCheckBoxes);
			}
			set
			{
				this.tree.ShowCheckBoxes = value;
			}
		}

		[DefaultValue(true)]
		public Boolean ShowExpandCollapse
		{
			get
			{
				return (this.tree.ShowExpandCollapse);
			}
			set
			{
				this.tree.ShowExpandCollapse = value;
			}
		}

		[DefaultValue(false)]
		public Boolean ShowLines
		{
			get
			{
				return (this.tree.ShowLines);
			}
			set
			{
				this.tree.ShowLines = value;
			}
		}

		[DefaultValue('/')]
		public Char PathSeparator
		{
			get
			{
				return (this.tree.PathSeparator);
			}
			set
			{
				this.tree.PathSeparator = value;
			}
		}

		public CssStyleCollection PanelStyle
		{
			get
			{
				return (this.panel.Style);
			}
		}

		[CssClassProperty]
		[DefaultValue("")]
		public String PanelCssClass
		{
			get
			{
				return (this.panel.CssClass);
			}
			set
			{
				this.panel.CssClass = value;
			}
		}

		[DefaultValue("")]
		[CssClassProperty]
		public String ImageCssClass
		{
			get
			{
				return (this.image.CssClass);
			}
			set
			{
				this.image.CssClass = value;
			}
		}

		public CssStyleCollection ImageStyle
		{
			get
			{
				return (this.image.Style);
			}
		}

		public CssStyleCollection TreeStyle
		{
			get
			{
				return (this.tree.Style);
			}
		}

		[CssClassProperty]
		[DefaultValue("")]
		public String TreeCssClass
		{
			get
			{
				return (this.tree.CssClass);
			}
			set
			{
				this.tree.CssClass = value;
			}
		}

		public TreeNodeStyle NodeStyle
		{
			get
			{
				return (this.tree.NodeStyle);
			}
		}

		public TreeNodeStyle RootNodeStyle
		{
			get
			{
				return (this.tree.RootNodeStyle);
			}
		}

		public TreeNodeStyle LeafNodeStyle
		{
			get
			{
				return (this.tree.LeafNodeStyle);
			}
		}

		public Style HoverNodeStyle
		{
			get
			{
				return (this.tree.HoverNodeStyle);
			}
		}

		public TreeNodeStyle SelectedNodeStyle
		{
			get
			{
				return (this.tree.SelectedNodeStyle);
			}
		}

		public CssStyleCollection LinkStyle
		{
			get
			{
				return (this.link.Style);
			}
		}

		[DefaultValue("")]
		[CssClassProperty]
		public String LinkCssClass
		{
			get
			{
				return (this.link.CssClass);
			}
			set
			{
				this.link.CssClass = value;
			}
		}

		[DefaultValue("")]
		[UrlProperty("*.png;*.gif;*.jpg")]
		public String ExpandImageUrl
		{
			get
			{
				return (this.tree.ExpandImageUrl);
			}
			set
			{
				this.tree.ExpandImageUrl = value;
			}
		}

		[DefaultValue("")]
		[UrlProperty("*.png;*.gif;*.jpg")]
		public String NoExpandImageUrl
		{
			get
			{
				return (this.tree.NoExpandImageUrl);
			}
			set
			{
				this.tree.NoExpandImageUrl = value;
			}
		}

		[DefaultValue("")]
		[UrlProperty("*.png;*.gif;*.jpg")]
		public String CollapseImageUrl
		{
			get
			{
				return (this.tree.CollapseImageUrl);
			}
			set
			{
				this.tree.CollapseImageUrl = value;
			}
		}

		public String SelectedValue
		{
			get
			{
				var selectedNode = this.tree.SelectedNode;

				return ((selectedNode != null) ? selectedNode.Value : null);
			}
		}

		public TreeNodeCollection CheckedNodes
		{
			get
			{
				return (this.tree.CheckedNodes);
			}
		}

		public String SelectedText
		{
			get
			{
				var selectedNode = this.tree.SelectedNode;

				return ((selectedNode != null) ? selectedNode.Text : null);
			}
		}

		public String SelectedValuePath
		{
			get
			{
				var selectedNode = this.tree.SelectedNode;

				return ((selectedNode != null) ? selectedNode.ValuePath : null);
			}
		}

		protected TreeNode FindNode(String valuePath, TreeNode parent)
		{
			if (parent.ValuePath == valuePath)
			{
				return (parent);
			}

			if ((parent.Expanded != true) && (parent.SelectAction != TreeNodeSelectAction.None) && (parent.SelectAction != TreeNodeSelectAction.Select))
			{
				this.OnPopulateTreeNode(this, new TreeNodeEventArgs(parent));
			}

			TreeNode node = null;

			foreach (var child in parent.ChildNodes.OfType<TreeNode>())
			{
				node = this.FindNode(valuePath, child);

				if (node != null)
				{
					break;
				}
			}

			return (node);
		}

		public void SelectNode(String valuePath)
		{
			var node = this.FindNode(valuePath, this.tree.Nodes[0]);

			if (node != null)
			{
				for (var parent = node; parent != null; parent = parent.Parent)
				{
					parent.Expanded = true;
					parent.PopulateOnDemand = false;
				}

				this.image.ImageUrl = node.ImageUrl;
				this.image.ToolTip = node.ImageToolTip;
				this.link.Text = node.Text;

				node.Select();
			}
		}

		protected override void CreateChildControls()
		{
			//panel that stores the current image and text
			this.panel.ID = String.Concat(this.ID, "_Panel");
			this.panel.Style[HtmlTextWriterStyle.Display] = "inline";
			this.Controls.Add(this.panel);

			//selection tree			
			this.tree.ID = String.Concat(this.ID, "_TreeView");
			this.tree.TreeNodePopulate += this.OnPopulateTreeNode;
			this.tree.Nodes.Add(this.CreateRootNode());
			this.Controls.Add(this.tree);

			//current image
			this.image.ID = String.Concat(this.ID, "_Image");
			this.image.ImageUrl = this.tree.Nodes[0].ImageUrl;
			this.image.ToolTip = tree.Nodes[0].Text;
			this.image.AlternateText = image.ToolTip;
			this.image.ImageAlign = ImageAlign.AbsMiddle;
			this.panel.Controls.Add(this.image);

			//current text
			this.link.ID = String.Concat(this.ID, "_HyperLink");
			this.link.Text = this.RootText;
			this.link.NavigateUrl = "#";
			this.link.Attributes[HtmlTextWriterAttribute.Onclick.ToString().ToLower()] = String.Format("document.getElementById('{0}').style.display = (document.getElementById('{0}').style.display == 'none') ? '' : 'none';/* if (!window.pastFirstTime) {{ document.getElementsByTagName('body')[0].addEventListener('click', function(e) {{  }}, false); window.pastFirstTime = true; }};*/ return false", this.tree.ClientID);
			this.panel.Controls.Add(this.link);

			this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.UniqueID + "getSelectedValuePath", String.Format("Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function() {{ document.getElementById('{0}').getSelectedValuePath = function() {{ var h = document.getElementById(document.getElementById('{1}_SelectedNode').value); if (!h) {{ return null }} else {{ return h.href.split('\\'')[3].substr(1).replace(/\\\\/g, '/') }} }} }});\n", this.ClientID, this.tree.ClientID), true);
			this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.UniqueID + "getSelectedValue", String.Format("Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function() {{ document.getElementById('{0}').getSelectedValue = function() {{ var h = document.getElementById(document.getElementById('{1}_SelectedNode').value); if (!h) {{ return null }} else {{ var i = h.href.lastIndexOf('\\\\'); return h.href.substr(i + 1, h.href.length - i - 3) }} }} }});\n", this.ClientID, this.tree.ClientID), true);
			this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.UniqueID + "getSelectedText", String.Format("Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function() {{ document.getElementById('{0}').getSelectedText = function() {{ var h = document.getElementById(document.getElementById('{1}_SelectedNode').value); if (!h) {{ return null }} else {{ return h.innerHTML }}  }} }});\n", this.ClientID, this.tree.ClientID), true);
			this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.UniqueID + "getCheckedNodes", String.Format("Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function() {{ document.getElementById('{0}').getCheckedNodes = function() {{ return document.querySelectorAll('input[type=checkbox][id^={1}]:checked') }} }});\n", this.ClientID, this.tree.ClientID), true);
			this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.UniqueID + "selectNode", String.Format("Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function() {{ document.getElementById('{0}').selectNode = function(valuePath) {{ var arg = 's' + valuePath.replace(/\\//g, '\\\\\'); __doPostBack('{1}', arg) }} }});\n", this.ClientID, this.tree.UniqueID), true);
		
			base.CreateChildControls();
		}

		protected override void OnLoad(EventArgs e)
		{
			if (this.Page.IsPostBack == true)
			{
				var selectedNode = this.tree.SelectedNode;

				if (selectedNode != null)
				{
					this.image.ImageUrl = selectedNode.ImageUrl;
					this.image.ToolTip = selectedNode.ToolTip;
					this.link.Text = selectedNode.Text;
				}
			}

			base.OnLoad(e);
		}
		
		protected virtual TreeNode CreateRootNode()
		{
			var node = new TreeNode(this.RootText, this.RootValue);
			node.PopulateOnDemand = true;
			node.Expanded = false;
			node.SelectAction = TreeNodeSelectAction.Expand;
			node.ImageToolTip = node.Text;
			node.ImageUrl = this.RootImageUrl;

			return (node);
		}

		protected void OnPopulateTreeNode(object sender, TreeNodeEventArgs e)
		{
			var handler = this.PopulateTreeNode;
			var node = e.Node;

			if (handler != null)
			{
				var args = new PopulateTreeNodeEventArgs(node);

				handler(this, args);

				var count = node.ChildNodes.Count;

				for (var i = 0;i < count; ++i)
				{
					var child = node.ChildNodes[i];

					/*if (!(child is PickerTreeNode) && (String.IsNullOrWhiteSpace(this.OnNodeClientClick) == false))
					{
						child = new PickerTreeNode(child.Text, child.Value, child.ImageUrl);
						(child as PickerTreeNode).OnNodeClientClick = this.OnNodeClientClick;
						node.ChildNodes.RemoveAt(i);
						node.ChildNodes.AddAt(i, child);
					}*/

					child.Expanded = false;
					child.PopulateOnDemand = (args.CanHaveChildren == true) && (child.ChildNodes.Count == 0);

					if (args.CanHaveChildren == false)
					{
						if (args.CanSelect == true)
						{
							child.SelectAction = TreeNodeSelectAction.Select;
						}
						else
						{
							child.SelectAction = TreeNodeSelectAction.None;
						}
					}
					else
					{
						if (args.CanSelect == true)
						{
							child.SelectAction = TreeNodeSelectAction.SelectExpand;
						}
						else
						{
							child.SelectAction = TreeNodeSelectAction.Expand;
						}
					}
				}
			}
		}
	}
}