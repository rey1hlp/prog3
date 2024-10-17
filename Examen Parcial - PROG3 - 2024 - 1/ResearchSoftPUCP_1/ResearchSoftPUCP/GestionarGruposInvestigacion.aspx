<%@ Page Title="" Language="C#" MasterPageFile="~/ResearchSoft.Master" AutoEventWireup="true" CodeBehind="GestionarGruposInvestigacion.aspx.cs" Inherits="ResearchSoftPUCP.GestionarGruposInvestigacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitulo" runat="server">
    Gestionar Grupos de Investigación
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphScripts" runat="server">
    <script src="Scripts/ResearchSoft/gestionarGruposInvestigacion.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContenido" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="card">
            <div class="card-header">
                <h2>
                    <!-- Cambiar el titulo dependiendo de si se registran o muestran datos -->
                    <asp:Label ID="lblTitulo" runat="server" Text="lblTitulo"></asp:Label>
                </h2>
            </div>
            <div class="card-body">
                <div class="card border">
                    <div class="card-header bg-light">
                        <h5 class="card-title mb-0">Datos Generales del Grupo</h5>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6 row">
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="lblIdGrupo" CssClass="col-form-label" runat="server" Text="ID del Grupo:"></asp:Label>
                                    <asp:TextBox ID="txtIdGrupo" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="lblAcronimo" runat="server" Text="Acrónimo:" CssClass="col-form-label" />
                                    <asp:TextBox ID="txtAcronimo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-12 mb-3">
                                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="col-form-label" />
                                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-12 mb-3">
                                    <asp:Label ID="lblDepartamento" runat="server" Text="Pertenencia al Dpto:" CssClass="col-form-label" />
                                    <asp:DropDownList ID="ddlDepartamento" CssClass="form-select" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="lblFechaFundacion" runat="server" Text="Fecha de Fundación:" CssClass="col-form-label" />
                                    <asp:TextBox ID="dtpFechaFundacion" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="lblPresupuestoAnual" runat="server" Text="Presupuesto Anual:" CssClass="col-form-label" />
                                    <asp:TextBox ID="txtPresupuestoAnual" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-12 mb-3">
                                    <asp:Label ID="lblTipoInvestigacion" runat="server" Text="Tipo de Investigación:" CssClass="col-form-label" />
                                    <div class="form-control">
                                        <div class="form-check form-check-inline">
                                            <input id="rbBasica" class="form-check-input" type="radio" runat="server" />
                                            <label id="lblBasica" class="form-check-label" for="cphContenido_rbBasica">BÁSICA</label>
                                        </div>
                                        <div class="form-check form-check-inline">
                                            <input id="rbAplicada" class="form-check-input" type="radio" runat="server" />
                                            <label id="lblAplicada" class="form-check-label" for="cphContenido_rbAplicada">APLICADA</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 mb-3">
                                 <asp:Label ID="lblInfraestructura" runat="server" Text="Infraestructura del Grupo:" CssClass="col-form-label" />
                                 <div class="form-control">
                                     <div class="form-check form-check-inline">
                                         <input id="cbLaboratorio" class="form-check-input" type="checkbox" runat="server" />
                                         <label id="lblLaboratorio" class="form-check-label" for="cphContenido_cbLaboratorio">LABORATORIO</label>
                                     </div>
                                     <div class="form-check form-check-inline">
                                         <input id="cbEquipamiento" class="form-check-input" type="checkbox" runat="server" />
                                         <label id="lblEquipamiento" class="form-check-label" for="cphContenido_cbEquipamiento">EQUIPO ESPECIALIZADO</label>
                                     </div>
                                     <div class="form-check form-check-inline">
                                         <input id="cbAmbienteTrabajo" class="form-check-input" type="checkbox" runat="server" />
                                         <label id="lblAmbienteTrabajo" class="form-check-label" for="cphContenido_cbAmbienteTrabajo">ESPACIO DE TRABAJO</label>
                                     </div>
                                 </div>
                             </div>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblFotoGrupo" CssClass="form-label" runat="server" Text="Foto del Grupo:"></asp:Label><br />
                                <div class="text-center">
                                    <asp:Image ID="imgFotoGrupo" alt="Foto del cliente" runat="server" CssClass="img-fluid img-thumbnail mb-2" ImageUrl="/Images/grupo.jpg"/>
                                </div>
                                <asp:FileUpload ID="fileUploadFotoGrupo" CssClass="form-control mb-2" runat="server"  />
                                <asp:Button ID="btnSubirFotoGrupo" CssClass="btn btn-primary" runat="server" Text="Cargar Foto" OnClick="btnSubirFotoGrupo_Click" />                               
                            </div>
                        </div>
                        <div class="row">
                           
                            <div class="col-md-12 mb-3">
                                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción del Grupo:" CssClass="col-sm-3 col-form-label" />
                                <textarea id="txtDescripcion" class="form-control" rows="3" cols="20" runat="server"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Lista de Integrantes -->
                <div class="card border">
                <div class="card-header bg-light">
                    <h5 class="card-title mb-0">Lista de Integrantes</h5>
                </div>
                <div class="card-body">
                    <div class="row align-items-center pb-3">
                        <div class="col-auto">
                            <asp:Label ID="lblIntegrante" CssClass="form-label" runat="server" Text="Integrante:"></asp:Label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtNombreIntegrante" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="lbBuscarIntegrante" runat="server"
                                CssClass="btn btn-info" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscarIntegrante_Click" />
                        </div>
                        <div class="col text-end">
                            <asp:LinkButton ID="lbAgregarIntegrante" runat="server" 
                                CssClass="btn btn-success" Text="<i class='fa-solid fa-plus pe-2'></i> Agregar" OnClick="lbAgregarIntegrante_Click"/>
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <asp:GridView ID="gvIntegrantes" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped" 
                                    OnPageIndexChanging="gvIntegrantes_PageIndexChanging" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:BoundField HeaderText="Tipo" DataField="Tipo"/>
                                        <asp:BoundField HeaderText="Codigo PUCP" DataField="CodigoPUCP"/>
                                        <asp:BoundField HeaderText="Nombre Completo" DataField="NombreCompleto"/>
                                        <asp:BoundField HeaderText="Dedicacion/CRAEST" DataField="DedicacionCRAEST"/>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton id="lbEliminar" runat="server" Text="<i class='fa-solid fa-trash'></i>" OnClick="lbEliminar_click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            </div>
            <div class="card-footer clearfix">
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="float-start btn btn-secondary" OnClick="btnRegresar_Click" />
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                CssClass="float-end btn btn-primary" OnClick="btnGuardar_Click"/>
        </div>
        </div>
        <div class="modal" id="form-modal">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Búsqueda de Miembros PUCP</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="container pb-3">
                                <div class="row align-items-center">
                                    <div class="col-auto">
                                        <asp:Label CssClass="col-form-label" runat="server" Text="Ingresar nombre o código PUCP:"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox CssClass="form-control" ID="txtNombreCodigoPUCP" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lbBuscarMiembroPUCPModal" runat="server" CssClass="btn btn-info" Text="<i class='fa-solid fa-magnifying-glass pe-2'></i> Buscar" OnClick="lbBuscarMiembroPUCPModal_Click"/>
                                    </div>
                                </div>
                            </div>
                            <div class="container">
                                <asp:GridView ID="gvMiembrosPUCP" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" CssClass="table table-hover table-responsive table-striped" OnPageIndexChanging="gvMiembrosPUCP_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField HeaderText="Tipo" DataField="Tipo"/>
                                        <asp:BoundField HeaderText="Codigo PUCP" DataField="CodigoPUCP"/>
                                        <asp:BoundField HeaderText="Nombre Completo" DataField="NombreCompleto"/>
                                        <asp:BoundField HeaderText="Dedicacion/CRAEST" DataField="DedicacionCRAEST"/>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbSeleccionar" CssClass="btn btn-success" runat="server" Text="<i class='fa-solid fa-check'></i> Seleccionar" OnClick="lbSeleccionar_click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>