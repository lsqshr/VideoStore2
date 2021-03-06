﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bank.Business.Components.BankNotificationService {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DeliveryInfoStatus", Namespace="http://schemas.datacontract.org/2004/07/VideoStore.Services.Interfaces")]
    public enum DeliveryInfoStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Submitted = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Delivered = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Failed = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BankNotificationService.IBankNotificationService")]
    public interface IBankNotificationService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBankNotificationService/NotifyOperationOutcome")]
        void NotifyOperationOutcome(System.Guid OrderId, Bank.Business.Components.BankNotificationService.DeliveryInfoStatus Status, string Message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBankNotificationServiceChannel : Bank.Business.Components.BankNotificationService.IBankNotificationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BankNotificationServiceClient : System.ServiceModel.ClientBase<Bank.Business.Components.BankNotificationService.IBankNotificationService>, Bank.Business.Components.BankNotificationService.IBankNotificationService {
        
        public BankNotificationServiceClient() {
        }
        
        public BankNotificationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BankNotificationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BankNotificationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BankNotificationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void NotifyOperationOutcome(System.Guid OrderId, Bank.Business.Components.BankNotificationService.DeliveryInfoStatus Status, string Message) {
            base.Channel.NotifyOperationOutcome(OrderId, Status, Message);
        }
    }
}
