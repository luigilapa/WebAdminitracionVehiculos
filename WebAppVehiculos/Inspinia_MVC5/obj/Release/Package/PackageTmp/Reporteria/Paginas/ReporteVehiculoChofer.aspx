<%@ Page Title="" Language="C#" MasterPageFile="~/Reporteria/Paginas/Site.Master" AutoEventWireup="true" CodeBehind="ReporteVehiculoChofer.aspx.cs" Inherits="Inspinia_MVC5.Reporteria.Paginas.ReporteVehiculoChofer" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ObjectDataSource ID="ObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Inspinia_MVC5.Reporteria.DataSet.DsUsuariosTableAdapters.SP_Reporte_VehiculoChoferTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="TxtNombre" Name="nombre" PropertyName="Text" Type="String" />
<asp:ControlParameter ControlID="TxtCedula" PropertyName="Text" Name="cedula" Type="String"></asp:ControlParameter>
            <asp:ControlParameter ControlID="CmbMarca" Name="idMarca" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="CmbTipo" Name="idTipo" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <h4 class="h4">REPORTE DE VEHÍCULOS CHOFERES</h4>
    <div class="row">
         <div class="col-md-6 col-lg-4">
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Nombre" class = "control-label col-md-2"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="Cédula" class = "control-label col-md-2"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="TxtCedula" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[0-9]*$" ErrorMessage="Ingreso solo  números." ForeColor="Red" ControlToValidate="TxtCedula" />
                    </div>
                </div>
                </div>
             </div>
        <div class="col-md-6 col-lg-4">
            <div class="form-horizontal">
                 <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Marca" class = "control-label col-md-2"></asp:Label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="CmbMarca" runat="server" class = "form-control"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Tipo" class = "control-label col-md-2"></asp:Label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="CmbTipo" runat="server" class = "form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:Button ID="btnGenerar" runat="server" Text="Generar"  onclick="btnGenerar_Click" class="btn btn-primary" />
            </div>
        </div>
    </div>

    <div class="row" style="overflow-y: auto; overflow-y: auto;">
            <rsweb:ReportViewer ID="ReportViewer" runat="server" Width="1014px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <localreport reportpath="Reporteria\Rdlc\RptVehiculoChofer.rdlc">
                    <datasources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource" Name="DataSet" />
                    </datasources>
                </localreport>
            </rsweb:ReportViewer>
    </div>

</asp:Content>
