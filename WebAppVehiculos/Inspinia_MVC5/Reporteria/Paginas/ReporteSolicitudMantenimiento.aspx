<%@ Page Title="" Language="C#" MasterPageFile="~/Reporteria/Paginas/Site.Master" AutoEventWireup="true" CodeBehind="ReporteSolicitudMantenimiento.aspx.cs" Inherits="Inspinia_MVC5.Reporteria.Paginas.ReporteSolicitudMantenimiento" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Inspinia_MVC5.Reporteria.DataSet.DsUsuariosTableAdapters.SP_Reporte_SolicitudMantenimientoTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="TxtFechaInicial" Name="fechaDesde" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="TxtFechaFinal" Name="fechaHasta" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="cmbChofer" Name="IdChofer" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="CmbVehiculo" Name="idVehiculo" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="CmbTipoMantenimiento" Name="TipoMantenimiento" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="CmbAprobado" Name="Aprobado" PropertyName="SelectedValue" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <div class="row">
         <div class="col-md-4 col-lg-6">
            <div class="form-horizontal">

                <div class="form-group" id="InputDateI">
                   <asp:Label ID="Label2" runat="server" Text="Fecha Inicial" class = "control-label col-md-2"></asp:Label>
                    <div class="input-group date col-md-6">
                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                        <asp:TextBox ID="TxtFechaInicial" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group" id="InputDateF">
                   <asp:Label ID="Label4" runat="server" Text="Fecha Final" class = "control-label col-md-2"></asp:Label>
                    <div class="input-group date col-md-6">
                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                        <asp:TextBox ID="TxtFechaFinal" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                 <div class="form-group">
                    <asp:Label ID="Label5" runat="server" Text="Tipo Mantenimeinto" class = "control-label col-md-2"></asp:Label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="CmbTipoMantenimiento" runat="server" class = "form-control"></asp:DropDownList>
                    </div>
                </div>

                </div>
             </div>
        <div class="col-md-4 col-lg-6">
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Chofer" class = "control-label col-md-2"></asp:Label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="cmbChofer" runat="server" class = "form-control"></asp:DropDownList>
                    </div>
                </div>
                
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Vehiculo" class = "control-label col-md-2"></asp:Label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="CmbVehiculo" runat="server" class = "form-control"></asp:DropDownList>
                    </div>
                </div>

                 <div class="form-group">
                    <asp:Label ID="Label6" runat="server" Text="Aprobado" class = "control-label col-md-2"></asp:Label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="CmbAprobado" runat="server" class = "form-control"></asp:DropDownList>
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
<rsweb:ReportViewer ID="ReportViewer" runat="server" Width="1274px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                        <LocalReport ReportPath="Reporteria\Rdlc\RptSolicitudMantenimiento.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource" Name="DataSet" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>

    <script type="text/javascript">

        $('#InputDateI .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy'
        });

        $('#InputDateF .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
    </script>
</asp:Content>
