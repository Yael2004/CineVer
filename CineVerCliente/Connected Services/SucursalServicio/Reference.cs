﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CineVerCliente.SucursalServicio {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SucursalDTO", Namespace="http://schemas.datacontract.org/2004/07/CineVerServicios.DTO")]
    [System.SerializableAttribute()]
    public partial class SucursalDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CalleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CiudadField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodigoPostalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EstadoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EstadoSucursalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan HoraAperturaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan HoraCierreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdSucursalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NombreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NumeroEnLaCalleField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Calle {
            get {
                return this.CalleField;
            }
            set {
                if ((object.ReferenceEquals(this.CalleField, value) != true)) {
                    this.CalleField = value;
                    this.RaisePropertyChanged("Calle");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Ciudad {
            get {
                return this.CiudadField;
            }
            set {
                if ((object.ReferenceEquals(this.CiudadField, value) != true)) {
                    this.CiudadField = value;
                    this.RaisePropertyChanged("Ciudad");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodigoPostal {
            get {
                return this.CodigoPostalField;
            }
            set {
                if ((object.ReferenceEquals(this.CodigoPostalField, value) != true)) {
                    this.CodigoPostalField = value;
                    this.RaisePropertyChanged("CodigoPostal");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Estado {
            get {
                return this.EstadoField;
            }
            set {
                if ((object.ReferenceEquals(this.EstadoField, value) != true)) {
                    this.EstadoField = value;
                    this.RaisePropertyChanged("Estado");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EstadoSucursal {
            get {
                return this.EstadoSucursalField;
            }
            set {
                if ((object.ReferenceEquals(this.EstadoSucursalField, value) != true)) {
                    this.EstadoSucursalField = value;
                    this.RaisePropertyChanged("EstadoSucursal");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan HoraApertura {
            get {
                return this.HoraAperturaField;
            }
            set {
                if ((this.HoraAperturaField.Equals(value) != true)) {
                    this.HoraAperturaField = value;
                    this.RaisePropertyChanged("HoraApertura");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan HoraCierre {
            get {
                return this.HoraCierreField;
            }
            set {
                if ((this.HoraCierreField.Equals(value) != true)) {
                    this.HoraCierreField = value;
                    this.RaisePropertyChanged("HoraCierre");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdSucursal {
            get {
                return this.IdSucursalField;
            }
            set {
                if ((this.IdSucursalField.Equals(value) != true)) {
                    this.IdSucursalField = value;
                    this.RaisePropertyChanged("IdSucursal");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nombre {
            get {
                return this.NombreField;
            }
            set {
                if ((object.ReferenceEquals(this.NombreField, value) != true)) {
                    this.NombreField = value;
                    this.RaisePropertyChanged("Nombre");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NumeroEnLaCalle {
            get {
                return this.NumeroEnLaCalleField;
            }
            set {
                if ((object.ReferenceEquals(this.NumeroEnLaCalleField, value) != true)) {
                    this.NumeroEnLaCalleField = value;
                    this.RaisePropertyChanged("NumeroEnLaCalle");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResultDTO", Namespace="http://schemas.datacontract.org/2004/07/CineVerServicios.DTO")]
    [System.SerializableAttribute()]
    public partial class ResultDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool EsExitosoField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Error {
            get {
                return this.ErrorField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorField, value) != true)) {
                    this.ErrorField = value;
                    this.RaisePropertyChanged("Error");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool EsExitoso {
            get {
                return this.EsExitosoField;
            }
            set {
                if ((this.EsExitosoField.Equals(value) != true)) {
                    this.EsExitosoField = value;
                    this.RaisePropertyChanged("EsExitoso");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ListaSucursalesDTO", Namespace="http://schemas.datacontract.org/2004/07/CineVerServicios.DTO")]
    [System.SerializableAttribute()]
    public partial class ListaSucursalesDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private CineVerCliente.SucursalServicio.ResultDTO ResultField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private CineVerCliente.SucursalServicio.SucursalDTO[] SucursalesField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public CineVerCliente.SucursalServicio.ResultDTO Result {
            get {
                return this.ResultField;
            }
            set {
                if ((object.ReferenceEquals(this.ResultField, value) != true)) {
                    this.ResultField = value;
                    this.RaisePropertyChanged("Result");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public CineVerCliente.SucursalServicio.SucursalDTO[] Sucursales {
            get {
                return this.SucursalesField;
            }
            set {
                if ((object.ReferenceEquals(this.SucursalesField, value) != true)) {
                    this.SucursalesField = value;
                    this.RaisePropertyChanged("Sucursales");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ListaFilasAsientosDTO", Namespace="http://schemas.datacontract.org/2004/07/CineVerServicios.DTO")]
    [System.SerializableAttribute()]
    public partial class ListaFilasAsientosDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private CineVerCliente.SucursalServicio.FilasPruebaDTO[] FilasField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private CineVerCliente.SucursalServicio.ResultDTO ResultField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public CineVerCliente.SucursalServicio.FilasPruebaDTO[] Filas {
            get {
                return this.FilasField;
            }
            set {
                if ((object.ReferenceEquals(this.FilasField, value) != true)) {
                    this.FilasField = value;
                    this.RaisePropertyChanged("Filas");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public CineVerCliente.SucursalServicio.ResultDTO Result {
            get {
                return this.ResultField;
            }
            set {
                if ((object.ReferenceEquals(this.ResultField, value) != true)) {
                    this.ResultField = value;
                    this.RaisePropertyChanged("Result");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FilasPruebaDTO", Namespace="http://schemas.datacontract.org/2004/07/CineVerServicios.DTO")]
    [System.SerializableAttribute()]
    public partial class FilasPruebaDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private CineVerCliente.SucursalServicio.AsientoDTO[] AsientosField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CantidadAsientosField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumeroFilaField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public CineVerCliente.SucursalServicio.AsientoDTO[] Asientos {
            get {
                return this.AsientosField;
            }
            set {
                if ((object.ReferenceEquals(this.AsientosField, value) != true)) {
                    this.AsientosField = value;
                    this.RaisePropertyChanged("Asientos");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CantidadAsientos {
            get {
                return this.CantidadAsientosField;
            }
            set {
                if ((this.CantidadAsientosField.Equals(value) != true)) {
                    this.CantidadAsientosField = value;
                    this.RaisePropertyChanged("CantidadAsientos");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumeroFila {
            get {
                return this.NumeroFilaField;
            }
            set {
                if ((this.NumeroFilaField.Equals(value) != true)) {
                    this.NumeroFilaField = value;
                    this.RaisePropertyChanged("NumeroFila");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AsientoDTO", Namespace="http://schemas.datacontract.org/2004/07/CineVerServicios.DTO")]
    [System.SerializableAttribute()]
    public partial class AsientoDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string estadoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idAsientoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> idFilaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string letraColumnaField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string estado {
            get {
                return this.estadoField;
            }
            set {
                if ((object.ReferenceEquals(this.estadoField, value) != true)) {
                    this.estadoField = value;
                    this.RaisePropertyChanged("estado");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idAsiento {
            get {
                return this.idAsientoField;
            }
            set {
                if ((this.idAsientoField.Equals(value) != true)) {
                    this.idAsientoField = value;
                    this.RaisePropertyChanged("idAsiento");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> idFila {
            get {
                return this.idFilaField;
            }
            set {
                if ((this.idFilaField.Equals(value) != true)) {
                    this.idFilaField = value;
                    this.RaisePropertyChanged("idFila");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string letraColumna {
            get {
                return this.letraColumnaField;
            }
            set {
                if ((object.ReferenceEquals(this.letraColumnaField, value) != true)) {
                    this.letraColumnaField = value;
                    this.RaisePropertyChanged("letraColumna");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SucursalServicio.ISucursalServicio")]
    public interface ISucursalServicio {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISucursalServicio/GuardarSucursal", ReplyAction="http://tempuri.org/ISucursalServicio/GuardarSucursalResponse")]
        CineVerCliente.SucursalServicio.ResultDTO GuardarSucursal(CineVerCliente.SucursalServicio.SucursalDTO sucursalDTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISucursalServicio/GuardarSucursal", ReplyAction="http://tempuri.org/ISucursalServicio/GuardarSucursalResponse")]
        System.Threading.Tasks.Task<CineVerCliente.SucursalServicio.ResultDTO> GuardarSucursalAsync(CineVerCliente.SucursalServicio.SucursalDTO sucursalDTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISucursalServicio/ActualizarSucursal", ReplyAction="http://tempuri.org/ISucursalServicio/ActualizarSucursalResponse")]
        CineVerCliente.SucursalServicio.ResultDTO ActualizarSucursal(int idSucursal, CineVerCliente.SucursalServicio.SucursalDTO sucursalDTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISucursalServicio/ActualizarSucursal", ReplyAction="http://tempuri.org/ISucursalServicio/ActualizarSucursalResponse")]
        System.Threading.Tasks.Task<CineVerCliente.SucursalServicio.ResultDTO> ActualizarSucursalAsync(int idSucursal, CineVerCliente.SucursalServicio.SucursalDTO sucursalDTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISucursalServicio/CerrarSucursal", ReplyAction="http://tempuri.org/ISucursalServicio/CerrarSucursalResponse")]
        CineVerCliente.SucursalServicio.ResultDTO CerrarSucursal(int idSucursal);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISucursalServicio/CerrarSucursal", ReplyAction="http://tempuri.org/ISucursalServicio/CerrarSucursalResponse")]
        System.Threading.Tasks.Task<CineVerCliente.SucursalServicio.ResultDTO> CerrarSucursalAsync(int idSucursal);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISucursalServicio/ObtenerSucursales", ReplyAction="http://tempuri.org/ISucursalServicio/ObtenerSucursalesResponse")]
        CineVerCliente.SucursalServicio.ListaSucursalesDTO ObtenerSucursales();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISucursalServicio/ObtenerSucursales", ReplyAction="http://tempuri.org/ISucursalServicio/ObtenerSucursalesResponse")]
        System.Threading.Tasks.Task<CineVerCliente.SucursalServicio.ListaSucursalesDTO> ObtenerSucursalesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISucursalServicio/ObtenerAsientosPorFila", ReplyAction="http://tempuri.org/ISucursalServicio/ObtenerAsientosPorFilaResponse")]
        CineVerCliente.SucursalServicio.ListaFilasAsientosDTO ObtenerAsientosPorFila(int idSala);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISucursalServicio/ObtenerAsientosPorFila", ReplyAction="http://tempuri.org/ISucursalServicio/ObtenerAsientosPorFilaResponse")]
        System.Threading.Tasks.Task<CineVerCliente.SucursalServicio.ListaFilasAsientosDTO> ObtenerAsientosPorFilaAsync(int idSala);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISucursalServicio/ObtenerAsientosPorFuncion", ReplyAction="http://tempuri.org/ISucursalServicio/ObtenerAsientosPorFuncionResponse")]
        CineVerCliente.SucursalServicio.ListaFilasAsientosDTO ObtenerAsientosPorFuncion(int idSala, int idFuncion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISucursalServicio/ObtenerAsientosPorFuncion", ReplyAction="http://tempuri.org/ISucursalServicio/ObtenerAsientosPorFuncionResponse")]
        System.Threading.Tasks.Task<CineVerCliente.SucursalServicio.ListaFilasAsientosDTO> ObtenerAsientosPorFuncionAsync(int idSala, int idFuncion);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISucursalServicioChannel : CineVerCliente.SucursalServicio.ISucursalServicio, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SucursalServicioClient : System.ServiceModel.ClientBase<CineVerCliente.SucursalServicio.ISucursalServicio>, CineVerCliente.SucursalServicio.ISucursalServicio {
        
        public SucursalServicioClient() {
        }
        
        public SucursalServicioClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SucursalServicioClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SucursalServicioClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SucursalServicioClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public CineVerCliente.SucursalServicio.ResultDTO GuardarSucursal(CineVerCliente.SucursalServicio.SucursalDTO sucursalDTO) {
            return base.Channel.GuardarSucursal(sucursalDTO);
        }
        
        public System.Threading.Tasks.Task<CineVerCliente.SucursalServicio.ResultDTO> GuardarSucursalAsync(CineVerCliente.SucursalServicio.SucursalDTO sucursalDTO) {
            return base.Channel.GuardarSucursalAsync(sucursalDTO);
        }
        
        public CineVerCliente.SucursalServicio.ResultDTO ActualizarSucursal(int idSucursal, CineVerCliente.SucursalServicio.SucursalDTO sucursalDTO) {
            return base.Channel.ActualizarSucursal(idSucursal, sucursalDTO);
        }
        
        public System.Threading.Tasks.Task<CineVerCliente.SucursalServicio.ResultDTO> ActualizarSucursalAsync(int idSucursal, CineVerCliente.SucursalServicio.SucursalDTO sucursalDTO) {
            return base.Channel.ActualizarSucursalAsync(idSucursal, sucursalDTO);
        }
        
        public CineVerCliente.SucursalServicio.ResultDTO CerrarSucursal(int idSucursal) {
            return base.Channel.CerrarSucursal(idSucursal);
        }
        
        public System.Threading.Tasks.Task<CineVerCliente.SucursalServicio.ResultDTO> CerrarSucursalAsync(int idSucursal) {
            return base.Channel.CerrarSucursalAsync(idSucursal);
        }
        
        public CineVerCliente.SucursalServicio.ListaSucursalesDTO ObtenerSucursales() {
            return base.Channel.ObtenerSucursales();
        }
        
        public System.Threading.Tasks.Task<CineVerCliente.SucursalServicio.ListaSucursalesDTO> ObtenerSucursalesAsync() {
            return base.Channel.ObtenerSucursalesAsync();
        }
        
        public CineVerCliente.SucursalServicio.ListaFilasAsientosDTO ObtenerAsientosPorFila(int idSala) {
            return base.Channel.ObtenerAsientosPorFila(idSala);
        }
        
        public System.Threading.Tasks.Task<CineVerCliente.SucursalServicio.ListaFilasAsientosDTO> ObtenerAsientosPorFilaAsync(int idSala) {
            return base.Channel.ObtenerAsientosPorFilaAsync(idSala);
        }
        
        public CineVerCliente.SucursalServicio.ListaFilasAsientosDTO ObtenerAsientosPorFuncion(int idSala, int idFuncion) {
            return base.Channel.ObtenerAsientosPorFuncion(idSala, idFuncion);
        }
        
        public System.Threading.Tasks.Task<CineVerCliente.SucursalServicio.ListaFilasAsientosDTO> ObtenerAsientosPorFuncionAsync(int idSala, int idFuncion) {
            return base.Channel.ObtenerAsientosPorFuncionAsync(idSala, idFuncion);
        }
    }
}
