﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cliente.ChessService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ChessService.IRegisterService", CallbackContract=typeof(Cliente.ChessService.IRegisterServiceCallback))]
    public interface IRegisterService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegisterService/generateCode", ReplyAction="http://tempuri.org/IRegisterService/generateCodeResponse")]
        bool generateCode(string username, string password, string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegisterService/generateCode", ReplyAction="http://tempuri.org/IRegisterService/generateCodeResponse")]
        System.Threading.Tasks.Task<bool> generateCodeAsync(string username, string password, string email);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRegisterService/verificateCode")]
        void verificateCode(string codeuser);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRegisterService/verificateCode")]
        System.Threading.Tasks.Task verificateCodeAsync(string codeuser);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRegisterServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRegisterService/ValidateCode")]
        void ValidateCode(bool codeStatus, string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRegisterServiceChannel : Cliente.ChessService.IRegisterService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RegisterServiceClient : System.ServiceModel.DuplexClientBase<Cliente.ChessService.IRegisterService>, Cliente.ChessService.IRegisterService {
        
        public RegisterServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public RegisterServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public RegisterServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public RegisterServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public RegisterServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public bool generateCode(string username, string password, string email) {
            return base.Channel.generateCode(username, password, email);
        }
        
        public System.Threading.Tasks.Task<bool> generateCodeAsync(string username, string password, string email) {
            return base.Channel.generateCodeAsync(username, password, email);
        }
        
        public void verificateCode(string codeuser) {
            base.Channel.verificateCode(codeuser);
        }
        
        public System.Threading.Tasks.Task verificateCodeAsync(string codeuser) {
            return base.Channel.verificateCodeAsync(codeuser);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ChessService.ILoginService", CallbackContract=typeof(Cliente.ChessService.ILoginServiceCallback))]
    public interface ILoginService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoginService/Login")]
        void Login(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoginService/Login")]
        System.Threading.Tasks.Task LoginAsync(string username, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoginServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoginService/LoginStatus")]
        void LoginStatus(bool status, string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoginServiceChannel : Cliente.ChessService.ILoginService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginServiceClient : System.ServiceModel.DuplexClientBase<Cliente.ChessService.ILoginService>, Cliente.ChessService.ILoginService {
        
        public LoginServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public LoginServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public LoginServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LoginServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LoginServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Login(string username, string password) {
            base.Channel.Login(username, password);
        }
        
        public System.Threading.Tasks.Task LoginAsync(string username, string password) {
            return base.Channel.LoginAsync(username, password);
        }
    }
}
