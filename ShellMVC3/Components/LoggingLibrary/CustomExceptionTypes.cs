using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;


namespace LoggingLibrary
{
    public static class CustomExceptionTypes
    {

        


       

        [Serializable]
        public   class JavaScriptException : Exception
        {
            public JavaScriptException(string message)
                : base(message)
            {

            }
        }

       [Serializable]
        internal  class ValidationException : ApplicationException
        {

            public ValidationException(string message)
                : base(message)
            {
            }

        } 
      

        public enum eReason { CouldNotAccess, ParseError }



        [Serializable]
        public  class AccountException : Exception, ISerializable
        {

            private MembersViewModel Model;
            private AccountException()
            {

            }

            public AccountException(MembersViewModel model)
                : base()
            {
                Model = model;
            }

            public AccountException(MembersViewModel model, string message)
                : base(message)
            {
                Model = model;
            }

            public AccountException(MembersViewModel model, string message, Exception inner)
                : base(message, inner)
            {
                Model = model;
            }

            protected AccountException(SerializationInfo info, StreamingContext context)
            {
                if (info == null)
                    throw new System.ArgumentNullException("info");

                Model = (MembersViewModel)info.GetValue("Model", typeof(MembersViewModel));
            }

            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
            void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
            {
                if (info == null)
                    throw new System.ArgumentNullException("info"); info.AddValue("Model", Model, typeof(MembersViewModel));
            }
        }


        public  class CacheingException : ApplicationException
        {
            private MembersViewModel _DS;


            private string _Message;
            public CacheingException(string message, MembersViewModel ds)
            {
                _Message = message;
                _DS = ds;
            }

            public new string Message
            {
                get { return _Message; }
            }

            public new MembersViewModel Data
            {
                get { return _DS; }
            }
        }

     

        //#region "Sample exceptions"
        //[Serializable]
        //public class AccountException : Exception, ISerializable
        //{

        //    private MembersViewModel Model;
        //    private AccountException()
        //    {

        //    }

        //    public AccountException(MembersViewModel model)
        //        : base()
        //    {
        //        Model = model;
        //    }

        //    public AccountException(MembersViewModel model, string message)
        //        : base(message)
        //    {
        //        Model = model;
        //    }

        //    public AccountException(MembersViewModel model, string message, Exception inner)
        //        : base(message, inner)
        //    {
        //        Model = model;
        //    }

        //    protected AccountException(SerializationInfo info, StreamingContext context)
        //    {
        //        if (info == null)
        //            throw new System.ArgumentNullException("info");

        //        Model = (MembersViewModel)info.GetValue("Model", typeof(MembersViewModel));
        //    }

        //    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        //    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        //    {
        //        if (info == null)
        //            throw new System.ArgumentNullException("info"); info.AddValue("Model", Model, typeof(MembersViewModel));
        //    }
        //}
        //public class LoginException : Exception
        //{
        //    private LogonViewModel _DS;
        //    private string _Message;

        //    public LoginException(string message, LogonViewModel ds)
        //    {
        //        _Message = message;
        //        _DS = ds;
        //    }

        //    public new string Message
        //    {
        //        get { return _Message; }
        //    }

        //    public new LogonViewModel Data
        //    {
        //        get { return _DS; }
        //    }
        //}
        //public class CacheingException : ApplicationException
        //{
        //    private MembersViewModel _DS;


        //    private string _Message;
        //    public CacheingException(string message, MembersViewModel ds)
        //    {
        //        _Message = message;
        //        _DS = ds;
        //    }

        //    public new string Message
        //    {
        //        get { return _Message; }
        //    }

        //    public new MembersViewModel Data
        //    {
        //        get { return _DS; }
        //    }
        //}
        //#endregion

        //[Serializable]
        //public class ReaderException : Exception, ISerializable
        //{

        //    public eReason Reason { get; private set; }
        //    private ReaderException()
        //    {

        //    }

        //    public ReaderException(eReason reason)
        //        : base()
        //    {
        //        Reason = reason;
        //    }

        //    public ReaderException(eReason reason, string message)
        //        : base(message)
        //    {
        //        Reason = reason;
        //    }

        //    public ReaderException(eReason reason, string message, Exception inner)
        //        : base(message, inner)
        //    {
        //        Reason = reason;
        //    }

        //    protected ReaderException(SerializationInfo info, StreamingContext context)
        //    {
        //        if (info == null)
        //            throw new System.ArgumentNullException("info");

        //        Reason = (eReason)info.GetValue("Reason", typeof(eReason));
        //    }

        //    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        //    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        //    {
        //        if (info == null)
        //            throw new System.ArgumentNullException("info"); info.AddValue("Reason", Reason, typeof(eReason));
        //    }
        //}

    }
}
