
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Collections.ObjectModel;


[DataContract()]
public class ServiceFault
{
    private string report;

    private string _detail;
    
  
    public ServiceFault(string message, string Detail = "")
    {
        this.report = message;
        this.Detail = Detail;
    }

    [DataMember()]
    public string Message
    {
        get { return this.report; }
        set { this.report = value; }
    }

    [DataMember()]
    public string Detail
    {
        get { return this._detail; }
        set { this._detail = value; }
    }
}
