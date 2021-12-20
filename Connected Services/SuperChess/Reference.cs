﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cliente.SuperChess {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SuperChess.IRegisterService", CallbackContract=typeof(Cliente.SuperChess.IRegisterServiceCallback))]
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
        void ValidateCode(bool codeStatus, int message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRegisterServiceChannel : Cliente.SuperChess.IRegisterService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RegisterServiceClient : System.ServiceModel.DuplexClientBase<Cliente.SuperChess.IRegisterService>, Cliente.SuperChess.IRegisterService {
        
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SuperChess.ILoginService", CallbackContract=typeof(Cliente.SuperChess.ILoginServiceCallback))]
    public interface ILoginService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoginService/Login")]
        void Login(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoginService/Login")]
        System.Threading.Tasks.Task LoginAsync(string username, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoginServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoginService/LoginStatus")]
        void LoginStatus(bool status, string message, int idUser);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoginServiceChannel : Cliente.SuperChess.ILoginService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginServiceClient : System.ServiceModel.DuplexClientBase<Cliente.SuperChess.ILoginService>, Cliente.SuperChess.ILoginService {
        
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SuperChess.IConnectionService", CallbackContract=typeof(Cliente.SuperChess.IConnectionServiceCallback))]
    public interface IConnectionService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IConnectionService/check")]
        void check();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IConnectionService/check")]
        System.Threading.Tasks.Task checkAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IConnectionServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IConnectionService/isConnected")]
        void isConnected(bool status);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IConnectionServiceChannel : Cliente.SuperChess.IConnectionService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ConnectionServiceClient : System.ServiceModel.DuplexClientBase<Cliente.SuperChess.IConnectionService>, Cliente.SuperChess.IConnectionService {
        
        public ConnectionServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ConnectionServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ConnectionServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ConnectionServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ConnectionServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void check() {
            base.Channel.check();
        }
        
        public System.Threading.Tasks.Task checkAsync() {
            return base.Channel.checkAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SuperChess.IRequestService", CallbackContract=typeof(Cliente.SuperChess.IRequestServiceCallback))]
    public interface IRequestService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRequestService/sendRequest")]
        void sendRequest(string username, int idUser);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRequestService/sendRequest")]
        System.Threading.Tasks.Task sendRequestAsync(string username, int idUser);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRequestServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRequestService/sendRequestStatus")]
        void sendRequestStatus(bool status, string msg);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRequestServiceChannel : Cliente.SuperChess.IRequestService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RequestServiceClient : System.ServiceModel.DuplexClientBase<Cliente.SuperChess.IRequestService>, Cliente.SuperChess.IRequestService {
        
        public RequestServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public RequestServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public RequestServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public RequestServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public RequestServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void sendRequest(string username, int idUser) {
            base.Channel.sendRequest(username, idUser);
        }
        
        public System.Threading.Tasks.Task sendRequestAsync(string username, int idUser) {
            return base.Channel.sendRequestAsync(username, idUser);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SuperChess.IRespondService", CallbackContract=typeof(Cliente.SuperChess.IRespondServiceCallback))]
    public interface IRespondService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRespondService/getRequests")]
        void getRequests(int idUser);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRespondService/getRequests")]
        System.Threading.Tasks.Task getRequestsAsync(int idUser);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRespondService/confirmRequest")]
        void confirmRequest(bool accept, int idUserSend, int idUserRecive);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRespondService/confirmRequest")]
        System.Threading.Tasks.Task confirmRequestAsync(bool accept, int idUserSend, int idUserRecive);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRespondServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRespondService/ReciveRequest")]
        void ReciveRequest(System.Collections.Generic.Dictionary<int, string> users);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRespondServiceChannel : Cliente.SuperChess.IRespondService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RespondServiceClient : System.ServiceModel.DuplexClientBase<Cliente.SuperChess.IRespondService>, Cliente.SuperChess.IRespondService {
        
        public RespondServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public RespondServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public RespondServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public RespondServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public RespondServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void getRequests(int idUser) {
            base.Channel.getRequests(idUser);
        }
        
        public System.Threading.Tasks.Task getRequestsAsync(int idUser) {
            return base.Channel.getRequestsAsync(idUser);
        }
        
        public void confirmRequest(bool accept, int idUserSend, int idUserRecive) {
            base.Channel.confirmRequest(accept, idUserSend, idUserRecive);
        }
        
        public System.Threading.Tasks.Task confirmRequestAsync(bool accept, int idUserSend, int idUserRecive) {
            return base.Channel.confirmRequestAsync(accept, idUserSend, idUserRecive);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SuperChess.IFriendService", CallbackContract=typeof(Cliente.SuperChess.IFriendServiceCallback))]
    public interface IFriendService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFriendService/Connected")]
        void Connected(int id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFriendService/Connected")]
        System.Threading.Tasks.Task ConnectedAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFriendService/Disconnected")]
        void Disconnected(int id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFriendService/Disconnected")]
        System.Threading.Tasks.Task DisconnectedAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFriendServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFriendService/getUsers")]
        void getUsers(string[] usernamesConnected, string[] usernamesDisconnected);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFriendService/newConecction")]
        void newConecction(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IFriendService/newDisconecction")]
        void newDisconecction(string username);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFriendServiceChannel : Cliente.SuperChess.IFriendService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FriendServiceClient : System.ServiceModel.DuplexClientBase<Cliente.SuperChess.IFriendService>, Cliente.SuperChess.IFriendService {
        
        public FriendServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public FriendServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public FriendServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public FriendServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public FriendServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Connected(int id) {
            base.Channel.Connected(id);
        }
        
        public System.Threading.Tasks.Task ConnectedAsync(int id) {
            return base.Channel.ConnectedAsync(id);
        }
        
        public void Disconnected(int id) {
            base.Channel.Disconnected(id);
        }
        
        public System.Threading.Tasks.Task DisconnectedAsync(int id) {
            return base.Channel.DisconnectedAsync(id);
        }
    }
}
