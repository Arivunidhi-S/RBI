<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentMaster.aspx.cs"
    Inherits="EquipmentMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="Images/BoilerIcon.png" />
    <title>Equipment Master</title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="css/menu_core.css" type="text/css" />
    <link rel="stylesheet" href="css/modal.css" type="text/css" />
    <link rel="stylesheet" href="css/styles_green.css" type="text/css" />
    <link rel="stylesheet" href="css/skins/menu_simpleAnimated.css" type="text/css" />
    <%--Menu Script--%>
    <link type="text/css" rel="stylesheet" href="css/Upper.css" />
    <script type="text/javascript" src="js/Upperjs.js"></script>
    <script type="text/javascript">
        history.go(1); /* undo user navigation (ex: IE Back Button) */
    </script>
    <style>
        .mycss
        {
            text-shadow: 1px 1px 3px rgba(48,47,46,1);
            font-weight: bold;
            color: #291517;
            letter-spacing: 1pt;
            word-spacing: 0pt;
            font-size: 25px;
            text-align: center;
            font-family: times new roman, times, serif;
        }
        body
        {
            background-image: url(images/Bg2.jpg); /*You will specify your image path here.*/
            -moz-background-size: cover;
            -webkit-background-size: cover;
            background-size: cover;
            background-position: top center !important;
            background-repeat: no-repeat !important;
            background-attachment: fixed;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                    border-bottom: blue thin solid; border-width: 0px" align="center">
                    <table border="0" width="100%">
                        <tr>
                            <td id="Td1" align="center" runat="server" colspan="2">
                                <div id="sse1">
                                    <div id="sses1">
                                        <ul>
                                             <li><a href="RBIHome.aspx">HOME</a></li>
                                            <li><a href="RBIMaster.aspx">MASTER</a></li>
                                            <li><a href="RBITransaction.aspx">TRANSACTION</a></li>
                                            <li><a href="login.aspx">LOGOUT</a></li>
                                        </ul>
                                    </div>
                                <asp:Label class="labelstyle" ID="lblName" runat="server" Font-Size="Large" ForeColor="White" Bold="true" style="text-align: right; vertical-align: middle; font-family: 'Aclonica', serif;
                                        color: #fff; text-shadow: 0px 1px 0px #999, 0px 2px 0px #888, 0px 3px 0px #777, 0px 4px 0px #666, 0px 5px 0px #555, 0px 6px 0px #444, 0px 7px 0px #333, 0px 8px 7px #001135;"/>
                                </div>
                            </td>
                        </tr>
                        <%--                        <tr>
                            <td align="center" colspan="2">
                                
                                
                            </td>
                        </tr>--%>
                        <%--<asp:FileUpload ID="EquipUpload" runat="server" /><asp:Button runat="server" OnClick="btnPreview_Click" ID="btnPhotoPreview" Text="Preview" /><asp:Image runat="server" ID="ImagePreview" Height="164px" Width="125px" />--%>
                        <tr>
                            <td align="center" style="width: 100%;">
                                <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
                                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                                    <AjaxSettings>
                                        <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                                <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                                                <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                <telerik:AjaxUpdatedControl ControlID="chkWTM" />
                                                <telerik:AjaxUpdatedControl ControlID="RadToolTipManager1" />
                                                <telerik:AjaxUpdatedControl ControlID="RadAjaxLoadingPanel1" />
                                                <telerik:AjaxUpdatedControl ControlID="cboIPLAYER" />
                                                <telerik:AjaxUpdatedControl ControlID="cboIntrusive" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                    </AjaxSettings>
                                </telerik:RadAjaxManager>
                                <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                                        Validation-IsRequired="true" ErrorMessage="Mandatory Fields">
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="cboProcessArea" />
                                            <telerik:TargetInput ControlID="txtequpID" />
                                        </TargetControls>
                                    </telerik:TextBoxSetting>
                                </telerik:RadInputManager>
                                <telerik:RadToolTipManager ID="RadToolTipManager1" OffsetY="-1" HideEvent="ManualClose"
                                    Width="300" Height="305" runat="server" RelativeTo="Element" Position="MiddleRight">
                                </telerik:RadToolTipManager>
                                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                                <br />
                                <div class="mycss">
                                    EQUIPMENT MASTER DETAILS
                                </div>
                                <br />
                                <div>
                                    <asp:Label class="labelstyle" ID="lblStatus" runat="server" Font-Bold="true" />
                                </div>
                                <br />
                                <div id="Div10" runat="server" style="width: 1250px; height: 600px; background-color: transparent;
                                    overflow: auto;" align="center">
                                    <telerik:RadGrid ID="RadGrid1" runat="server" Width="95%" AllowMultiRowEdit="false"
                                        OnNeedDataSource="RadGrid1_NeedDataSource" GridLines="Vertical" Skin="Telerik"
                                        AllowPaging="True" PageSize="8" OnItemCreated="RadGrid1_ItemCreated" PagerStyle-AlwaysVisible="true"
                                        PagerStyle-Position="Bottom" OnItemDataBound="RadGrid1_ItemDataBound" OnDeleteCommand="RadGrid1_DeleteCommand"
                                        AllowAutomaticUpdates="false" AllowAutomaticInserts="false" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                        AllowAutomaticDeletes="false" OnInsertCommand="RadGrid1_InsertCommand" AllowSorting="false"
                                        AllowFilteringByColumn="true" OnUpdateCommand="RadGrid1_UpdateCommand">
                                        <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true"
                                            EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                                            <Resizing AllowColumnResize="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="EquAutoID" CommandItemDisplay="Top"
                                            CommandItemSettings-AddNewRecordText="Add New Equipment Details" InsertItemPageIndexAction="ShowItemOnFirstPage">
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                    <HeaderStyle Width="5%" />
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn DataField="EquAutoID" DataType="System.Int64" HeaderText="EquAutoID"
                                                    ReadOnly="True" SortExpression="EquAutoID" UniqueName="EquAutoID" AllowFiltering="false"
                                                    AllowSorting="false" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ProcessArea" DataType="System.String" HeaderText="Process Area"
                                                    ReadOnly="True" SortExpression="ProcessArea" UniqueName="ProcessArea" AllowFiltering="true"
                                                    AllowSorting="false" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EquPID" DataType="System.String" HeaderText="EquPID"
                                                    SortExpression="EquPID" UniqueName="EquPID">
                                                    <HeaderStyle Width="20%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EqupType" DataType="System.String" HeaderText="EqupType"
                                                    SortExpression="EqupType" UniqueName="EqupType">
                                                    <HeaderStyle Width="15%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DoshNo" DataType="System.String" HeaderText="DoshNo"
                                                    SortExpression="DoshNo" UniqueName="DoshNo">
                                                    <HeaderStyle Width="15%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PID" DataType="System.String" HeaderText="PID"
                                                    SortExpression="PID" UniqueName="PID">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="YearInstalled" DataType="System.String" HeaderText="YearInstalled"
                                                    SortExpression="YearInstalled" UniqueName="YearInstalled">
                                                    <HeaderStyle Width="15%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                    ConfirmText="Are you sure want to delete?">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                            <EditFormSettings EditFormType="Template">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                                <FormTemplate>
                                                    <table cellspacing="5" cellpadding="5" style="background-color: transparent" width="100%"
                                                        style="height: 280px" border="0">
                                                        <tr>
                                                            <td colspan="2" align="left" style="width: 100px">
                                                                <b>ID:
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("EquAutoID")%>' />
                                                                </b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px" align="left">
                                                                <b>Process Area:
                                                                    <asp:Label ID="Label11" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                        width: 1px;" /></b>
                                                            </td>
                                                            <td style="width: 200px" align="left">
                                                                <telerik:RadComboBox ID="cboProcessArea" runat="server" Height="60px" Width="200px"
                                                                    DataSourceID="SqlDataSourcePA" DataTextField="ProcessArea" DropDownWidth="200px"
                                                                    EmptyMessage="Select" DataValueField="ProcessAreaID" AppendDataBoundItems="True"
                                                                    Text='<%# Bind("ProcessArea") %>'>
                                                                    <Items>
                                                                        <%--<telerik:RadComboBoxItem Text="--Select--" Value="--Select--" ForeColor="Silver" />--%>
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="LblProcessArea" runat="server" Text='<%# Bind("ProcessAreaID") %>'
                                                                    Visible="false" />
                                                            </td>
                                                            <td align="left" style="width: 100px">
                                                                <b>Equipment ID:
                                                                    <asp:Label ID="Label4" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                        width: 1px;" /></b>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <telerik:RadTextBox ID="txtequpID" Text='<%# Bind("EquPID") %>' Filter="StartsWith"
                                                                    MaxLength="20" Width="200px" EmptyMessage="Enter Equipment ID" ToolTip="Max Length 20"
                                                                    Type="Number" runat="server">
                                                                </telerik:RadTextBox>
                                                                <asp:Label ID="lblequpid" runat="server" Visible="false" Text='<%# Bind("EquPID") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 100px">
                                                                <b>Equipment Type :</b>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <telerik:RadTextBox ID="txtequipmenttype" Text='<%# Bind("EqupType") %>' MaxLength="20"
                                                                    Width="200px" EmptyMessage="Enter Equipment Type" ToolTip="Max Length 20" Type="Number"
                                                                    runat="server">
                                                                </telerik:RadTextBox>
                                                                <asp:Label ID="lblequipmenttype" runat="server" Visible="false" Text='<%# Bind("EqupType") %>' />
                                                            </td>
                                                            <td align="left">
                                                                <b>Equipment Description :</b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox ID="txtdescription" Text='<%# Bind("EqupDescription") %>' Width="200px"
                                                                    Height="40px" EmptyMessage="Enter Equipment Description" ToolTip="Enter Equipment Description"
                                                                    TextMode="MultiLine" runat="server">
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Dosh No:</b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox ID="txtdoshno" Text='<%# Bind("DoshNo") %>' Width="200px" EmptyMessage="Enter DoshNo"
                                                                    MaxLength="20" ToolTip="Max Length 20" runat="server">
                                                                </telerik:RadTextBox>
                                                            </td>
                                                            <td align="left">
                                                                <b>P&ID: </b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadTextBox ID="txtPID" Text='<%# Bind("PID") %>' MaxLength="50" Width="200px"
                                                                    EmptyMessage="Enter PID" ToolTip="Max Length 20" runat="server">
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Internal protective Layer :</b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadComboBox ID="cboIPLAYER" runat="server" DropDownWidth="200px" Height="100px"
                                                                    Width="200px" AppendDataBoundItems="True" Text='<%# Bind("IPLayer") %>'>
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="None" Value="None" />
                                                                        <telerik:RadComboBoxItem Text="Coating" Value="Coating" />
                                                                        <telerik:RadComboBoxItem Text="Metalic Lining" Value="Metalic Lining" />
                                                                        <telerik:RadComboBoxItem Text="Rubber Lining" Value="Rubber Lining" />
                                                                        <telerik:RadComboBoxItem Text="Weld Lining" Value="Weld Lining" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td align="left">
                                                                <b>Intrusive :</b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadComboBox ID="cboIntrusive" runat="server" DropDownWidth="200px" Height="60px"
                                                                    Width="200px" AppendDataBoundItems="True" Text='<%# Bind("Instructive") %>'>
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="None" Value="None" />
                                                                        <telerik:RadComboBoxItem Text="Intrusive None Entry" Value="Intrusive None Entry" />
                                                                        <telerik:RadComboBoxItem Text="Intrusive Entry" Value="Intrusive Entry" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <b>Wall Thickness Measurment : </b>
                                                            </td>
                                                            <td align="left">
                                                                <asp:CheckBox ID="chkWTM" runat="server" Height="10px" />
                                                            </td>
                                                            <td align="left">
                                                                <b>Year Installed: </b>
                                                            </td>
                                                            <td align="left">
                                                                <telerik:RadComboBox ID="cboyearinatall" runat="server" Filter="StartsWith" Height="100px"
                                                                    Width="200px" EmptyMessage="Select Year" Text='<%# Bind("YearInstalled") %>'>
                                                                    <Items>
                                                                        <%--<telerik:RadComboBoxItem Text="--Select--" Value="--Select--" ForeColor="Silver" />--%>
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="Table1" runat="server" width="100%">
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <b>Design Code :</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDesignCode" runat="server" Text='<%# Bind("DesignCode") %>' Width="200px" />
                                                            </td>
                                                            <td align="left">
                                                                <%-- <telerik:RadUpload ID="EquipUpload" runat="server"> </telerik:RadUpload> --%>
                                                                <%--<telerik:RadAsyncUpload runat="server" ID="AsyncUpload1" OnClientFileUploaded="OnClientFileUploaded"
                                                            AllowedFileExtensions="jpg,jpeg,png,gif,bmp" MaxFileSize="1048576" OnValidatingFile="RadAsyncUpload1_ValidatingFile">
                                                        </telerik:RadAsyncUpload>--%>
                                                                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="EquipUpload" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="EquipUpload" runat="server" Visible="false"/>
                                                                        <input type ="file" />
                                                                        <asp:Label ID="lblhide" runat="server"></asp:Label>
                                                                        &nbsp;
                                                                        <asp:Button runat="server" CommandName="custom" ID="btnPhotoPreview" Text="Preview" />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>--%>
                                                                <%--  <asp:FileUpload ID="EquipUpload" runat="server" />
                                                                                                            &nbsp;
                                                                        <asp:Button runat="server" CommandName="custom" ID="Button3" Text="Preview" />--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <b>History Description :</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txthistdescrip" runat="server" Text='<%# Bind("historydescription") %>'
                                                                    TextMode="MultiLine" Width="500px" Height="30px" />
                                                            </td>
                                                            <%-- <td rowspan="5">
                                                                <asp:Image ID="ImagePreview" Visible="true" runat="server" Height="175px" Width="300px" />
                                                            </td>--%>
                                                        </tr>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <b>Inspection Techniques :</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtinspectech" runat="server" TextMode="MultiLine" Text='<%# Bind("InspectionTechniques") %>'
                                                                    Width="500px" Height="30px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <b>Inspection Scope :</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtinspecscope" runat="server" TextMode="MultiLine" Text='<%# Bind("Inspectionscope") %>'
                                                                    Width="500px" Height="30px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <b>RBI Observation :</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRBIObserv" runat="server" TextMode="MultiLine" Text='<%# Bind("RBIobservation") %>'
                                                                    Width="500px" Height="30px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <b>DOSH Observation :</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDOSHobserv" runat="server" TextMode="MultiLine" Text='<%# Bind("DOSHobservation") %>'
                                                                    Width="500px" Height="30px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="Table2" runat="server" width="100%">
                                                        <tr>
                                                            <td colspan="2" align="left">
                                                                <asp:Button ID="Button1" Height="25px" runat="server" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                    CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                                </asp:Button>
                                                                <asp:Button ID="Button2" Height="25px" runat="server" Text="Cancel" CausesValidation="false"
                                                                    CommandName="Cancel"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FormTemplate>
                                            </EditFormSettings>
                                        </MasterTableView>
                                        <PagerStyle Mode="NumericPages"></PagerStyle>
                                    </telerik:RadGrid>
                                    <asp:SqlDataSource ID="SqlDataSourcePA" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                                        SelectCommand="SELECT  [processareaid], [processarea] FROM [Tbl_ProcessArea] where deleted=0 ORDER BY [processareaid]">
                                    </asp:SqlDataSource>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
