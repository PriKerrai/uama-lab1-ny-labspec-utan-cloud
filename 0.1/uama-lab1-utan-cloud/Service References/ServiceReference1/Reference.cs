﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.Phone.ServiceReference, version 3.7.0.0
// 
namespace uama_lab1_utan_cloud.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IService1/Subscribe", ReplyAction="http://tempuri.org/IService1/SubscribeResponse")]
        System.IAsyncResult BeginSubscribe(string uri, System.AsyncCallback callback, object asyncState);
        
        void EndSubscribe(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IService1/SendToast", ReplyAction="http://tempuri.org/IService1/SendToastResponse")]
        System.IAsyncResult BeginSendToast(string title, string message, System.AsyncCallback callback, object asyncState);
        
        void EndSendToast(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : uama_lab1_utan_cloud.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<uama_lab1_utan_cloud.ServiceReference1.IService1>, uama_lab1_utan_cloud.ServiceReference1.IService1 {
        
        private BeginOperationDelegate onBeginSubscribeDelegate;
        
        private EndOperationDelegate onEndSubscribeDelegate;
        
        private System.Threading.SendOrPostCallback onSubscribeCompletedDelegate;
        
        private BeginOperationDelegate onBeginSendToastDelegate;
        
        private EndOperationDelegate onEndSendToastDelegate;
        
        private System.Threading.SendOrPostCallback onSendToastCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> SubscribeCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> SendToastCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult uama_lab1_utan_cloud.ServiceReference1.IService1.BeginSubscribe(string uri, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSubscribe(uri, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void uama_lab1_utan_cloud.ServiceReference1.IService1.EndSubscribe(System.IAsyncResult result) {
            base.Channel.EndSubscribe(result);
        }
        
        private System.IAsyncResult OnBeginSubscribe(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string uri = ((string)(inValues[0]));
            return ((uama_lab1_utan_cloud.ServiceReference1.IService1)(this)).BeginSubscribe(uri, callback, asyncState);
        }
        
        private object[] OnEndSubscribe(System.IAsyncResult result) {
            ((uama_lab1_utan_cloud.ServiceReference1.IService1)(this)).EndSubscribe(result);
            return null;
        }
        
        private void OnSubscribeCompleted(object state) {
            if ((this.SubscribeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SubscribeCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SubscribeAsync(string uri) {
            this.SubscribeAsync(uri, null);
        }
        
        public void SubscribeAsync(string uri, object userState) {
            if ((this.onBeginSubscribeDelegate == null)) {
                this.onBeginSubscribeDelegate = new BeginOperationDelegate(this.OnBeginSubscribe);
            }
            if ((this.onEndSubscribeDelegate == null)) {
                this.onEndSubscribeDelegate = new EndOperationDelegate(this.OnEndSubscribe);
            }
            if ((this.onSubscribeCompletedDelegate == null)) {
                this.onSubscribeCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSubscribeCompleted);
            }
            base.InvokeAsync(this.onBeginSubscribeDelegate, new object[] {
                        uri}, this.onEndSubscribeDelegate, this.onSubscribeCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult uama_lab1_utan_cloud.ServiceReference1.IService1.BeginSendToast(string title, string message, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSendToast(title, message, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void uama_lab1_utan_cloud.ServiceReference1.IService1.EndSendToast(System.IAsyncResult result) {
            base.Channel.EndSendToast(result);
        }
        
        private System.IAsyncResult OnBeginSendToast(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string title = ((string)(inValues[0]));
            string message = ((string)(inValues[1]));
            return ((uama_lab1_utan_cloud.ServiceReference1.IService1)(this)).BeginSendToast(title, message, callback, asyncState);
        }
        
        private object[] OnEndSendToast(System.IAsyncResult result) {
            ((uama_lab1_utan_cloud.ServiceReference1.IService1)(this)).EndSendToast(result);
            return null;
        }
        
        private void OnSendToastCompleted(object state) {
            if ((this.SendToastCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SendToastCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SendToastAsync(string title, string message) {
            this.SendToastAsync(title, message, null);
        }
        
        public void SendToastAsync(string title, string message, object userState) {
            if ((this.onBeginSendToastDelegate == null)) {
                this.onBeginSendToastDelegate = new BeginOperationDelegate(this.OnBeginSendToast);
            }
            if ((this.onEndSendToastDelegate == null)) {
                this.onEndSendToastDelegate = new EndOperationDelegate(this.OnEndSendToast);
            }
            if ((this.onSendToastCompletedDelegate == null)) {
                this.onSendToastCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSendToastCompleted);
            }
            base.InvokeAsync(this.onBeginSendToastDelegate, new object[] {
                        title,
                        message}, this.onEndSendToastDelegate, this.onSendToastCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override uama_lab1_utan_cloud.ServiceReference1.IService1 CreateChannel() {
            return new Service1ClientChannel(this);
        }
        
        private class Service1ClientChannel : ChannelBase<uama_lab1_utan_cloud.ServiceReference1.IService1>, uama_lab1_utan_cloud.ServiceReference1.IService1 {
            
            public Service1ClientChannel(System.ServiceModel.ClientBase<uama_lab1_utan_cloud.ServiceReference1.IService1> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginSubscribe(string uri, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = uri;
                System.IAsyncResult _result = base.BeginInvoke("Subscribe", _args, callback, asyncState);
                return _result;
            }
            
            public void EndSubscribe(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("Subscribe", _args, result);
            }
            
            public System.IAsyncResult BeginSendToast(string title, string message, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[2];
                _args[0] = title;
                _args[1] = message;
                System.IAsyncResult _result = base.BeginInvoke("SendToast", _args, callback, asyncState);
                return _result;
            }
            
            public void EndSendToast(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("SendToast", _args, result);
            }
        }
    }
}
