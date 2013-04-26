﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VideoStore.Business.Components.TransferService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TransferService.ITransferService")]
    public interface ITransferService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITransferService/Transfer")]
        void Transfer(System.Guid OrderNumber, double pAmount, int pFromAcctNumber, int pToAcctNumber, string pReturnAddress);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITransferServiceChannel : VideoStore.Business.Components.TransferService.ITransferService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TransferServiceClient : System.ServiceModel.ClientBase<VideoStore.Business.Components.TransferService.ITransferService>, VideoStore.Business.Components.TransferService.ITransferService {
        
        public TransferServiceClient() {
        }
        
        public TransferServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TransferServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransferServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransferServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Transfer(System.Guid OrderNumber, double pAmount, int pFromAcctNumber, int pToAcctNumber, string pReturnAddress) {
            base.Channel.Transfer(OrderNumber, pAmount, pFromAcctNumber, pToAcctNumber, pReturnAddress);
        }
    }
}
