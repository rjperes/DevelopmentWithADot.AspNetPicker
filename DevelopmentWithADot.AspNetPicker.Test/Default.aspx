<%@ Page Language="C#" CodeBehind="Default.aspx.cs" Inherits="DevelopmentWithADot.AspNetPicker.Test._Default" %>
<%@ Register TagPrefix="test" Namespace="DevelopmentWithADot.AspNetPicker" Assembly="DevelopmentWithADot.AspNetPicker" %><!DOCTYPE html>
<html>
	<head runat="server">
		<title></title>
		<style type="text/css">

			.picker
			{
			}

			.tree
			{
				background-color: cyan;
				border: solid 1px;
			}

		</style>
		<script type="text/javascript">

			function test()
			{
				var value = document.getElementById('picker').getSelectedValue();
				var text = document.getElementById('picker').getSelectedText();
				var valuePath = document.getElementById('picker').getSelectedValuePath();
				var checkedNodes = document.getElementById('picker').getCheckedNodes();

				debugger;
				document.getElementById('picker').selectNode('0/0.0/1.0/2.0/3.0');
			}

		</script>
	</head>
	<body>
		<form runat="server">
			<asp:ScriptManager runat="server" />
			<asp:UpdatePanel runat="server" RenderMode="Inline">
				<ContentTemplate>
					<test:Picker runat="server" ID="picker" ShowCheckBoxes="Leaf" SelectedNodeStyle-BackColor="Yellow" CssClass="picker" TreeCssClass="tree" RootText="World" RootImageUrl="~/0.png" OnPopulateTreeNode="OnPopulateNode" BorderStyle="Solid" BorderColor="Blue" BorderWidth="1px" Height="22px" Width="200px" />
					<asp:Button runat="server" Text="Test" OnClick="OnClick" />
					<asp:TextBox runat="server" ID="path"/>
					<asp:Button runat="server" Text="Select" OnClick="OnSelectClick" />
					<button onclick="test(); return false;">Get Selected Value</button>
				</ContentTemplate>
			</asp:UpdatePanel>
		</form>
	</body>
</html>

