<%@ Page Title="" Language="C#" MasterPageFile="~/Reporteria/Paginas/Site.Master" AutoEventWireup="true" CodeBehind="ReporteChoferes.aspx.cs" Inherits="Inspinia_MVC5.Reporteria.Paginas.ReporteChoferes" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ObjectDataSource ID="ObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Inspinia_MVC5.Reporteria.DataSet.DsUsuariosTableAdapters.SP_Reporte_CheferesTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="cmbChofer" Name="IdChofer" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="TxtNombre" Name="nombre" PropertyName="Text" Type="String" />
            <asp:Parameter Name="idRol" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <h4 class="h4">REPORTE DE CHOFERES</h4>
    <div class="row">
         <div class="col-lg-6">
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Chofer" class = "control-label col-md-2"></asp:Label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="cmbChofer" runat="server" class = "form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Nombre" class = "control-label col-md-2"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <%--<div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Rol" class = "control-label col-md-2"></asp:Label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="CmbRol" runat="server" class = "form-control"></asp:DropDownList>
                    </div>
                </div>--%>
                 <div class="form-group">
                    <div class="col-md-10">
                        <asp:Button ID="btnGenerar" runat="server" Text="Generar"  onclick="btnGenerar_Click" class="btn btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row" style="overflow-y: auto; overflow-y: auto;">
            <rsweb:ReportViewer ID="ReportViewer" runat="server" Width="1264px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="Reporteria\Rdlc\RptChoferes.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource" Name="DataSet" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>                 
    </div>
   
</asp:Content>
