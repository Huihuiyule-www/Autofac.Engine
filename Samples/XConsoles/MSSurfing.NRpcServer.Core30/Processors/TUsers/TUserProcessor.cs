/**
 * Autogenerated by Thrift Compiler (0.11.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace MSSurfing.TClient.Processors.Users
{
  public partial class TUserProcessor {
    /// <summary>
    /// 用户处理器
    /// </summary>
    public interface ISync {
      /// <summary>
      /// 用户
      /// @return Dictionary<int,list<TUser.TUser>> <int:Code(1=Success,负数为不成功) ,string:成功时返回的Data>
      /// </summary>
      /// <param name="name"></param>
      Dictionary<int, List<MSSurfing.TClient.Domain.Users.TUser>> Search(string name);
    }

    /// <summary>
    /// 用户处理器
    /// </summary>
    public interface Iface : ISync {
      /// <summary>
      /// 用户
      /// @return Dictionary<int,list<TUser.TUser>> <int:Code(1=Success,负数为不成功) ,string:成功时返回的Data>
      /// </summary>
      /// <param name="name"></param>
      #if SILVERLIGHT
      IAsyncResult Begin_Search(AsyncCallback callback, object state, string name);
      Dictionary<int, List<MSSurfing.TClient.Domain.Users.TUser>> End_Search(IAsyncResult asyncResult);
      #endif
    }

    /// <summary>
    /// 用户处理器
    /// </summary>
    public class Client : IDisposable, Iface {
      public Client(TProtocol prot) : this(prot, prot)
      {
      }

      public Client(TProtocol iprot, TProtocol oprot)
      {
        iprot_ = iprot;
        oprot_ = oprot;
      }

      protected TProtocol iprot_;
      protected TProtocol oprot_;
      protected int seqid_;

      public TProtocol InputProtocol
      {
        get { return iprot_; }
      }
      public TProtocol OutputProtocol
      {
        get { return oprot_; }
      }


      #region " IDisposable Support "
      private bool _IsDisposed;

      // IDisposable
      public void Dispose()
      {
        Dispose(true);
      }
      

      protected virtual void Dispose(bool disposing)
      {
        if (!_IsDisposed)
        {
          if (disposing)
          {
            if (iprot_ != null)
            {
              ((IDisposable)iprot_).Dispose();
            }
            if (oprot_ != null)
            {
              ((IDisposable)oprot_).Dispose();
            }
          }
        }
        _IsDisposed = true;
      }
      #endregion


      
      #if SILVERLIGHT
      public IAsyncResult Begin_Search(AsyncCallback callback, object state, string name)
      {
        return send_Search(callback, state, name);
      }

      public Dictionary<int, List<MSSurfing.TClient.Domain.Users.TUser>> End_Search(IAsyncResult asyncResult)
      {
        oprot_.Transport.EndFlush(asyncResult);
        return recv_Search();
      }

      #endif

      /// <summary>
      /// 用户
      /// @return Dictionary<int,list<TUser.TUser>> <int:Code(1=Success,负数为不成功) ,string:成功时返回的Data>
      /// </summary>
      /// <param name="name"></param>
      public Dictionary<int, List<MSSurfing.TClient.Domain.Users.TUser>> Search(string name)
      {
        #if !SILVERLIGHT
        send_Search(name);
        return recv_Search();

        #else
        var asyncResult = Begin_Search(null, null, name);
        return End_Search(asyncResult);

        #endif
      }
      #if SILVERLIGHT
      public IAsyncResult send_Search(AsyncCallback callback, object state, string name)
      #else
      public void send_Search(string name)
      #endif
      {
        oprot_.WriteMessageBegin(new TMessage("Search", TMessageType.Call, seqid_));
        Search_args args = new Search_args();
        args.Name = name;
        args.Write(oprot_);
        oprot_.WriteMessageEnd();
        #if SILVERLIGHT
        return oprot_.Transport.BeginFlush(callback, state);
        #else
        oprot_.Transport.Flush();
        #endif
      }

      public Dictionary<int, List<MSSurfing.TClient.Domain.Users.TUser>> recv_Search()
      {
        TMessage msg = iprot_.ReadMessageBegin();
        if (msg.Type == TMessageType.Exception) {
          TApplicationException x = TApplicationException.Read(iprot_);
          iprot_.ReadMessageEnd();
          throw x;
        }
        Search_result result = new Search_result();
        result.Read(iprot_);
        iprot_.ReadMessageEnd();
        if (result.__isset.success) {
          return result.Success;
        }
        throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "Search failed: unknown result");
      }

    }
    public class Processor : TProcessor {
      public Processor(ISync iface)
      {
        iface_ = iface;
        processMap_["Search"] = Search_Process;
      }

      protected delegate void ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot);
      private ISync iface_;
      protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

      public bool Process(TProtocol iprot, TProtocol oprot)
      {
        try
        {
          TMessage msg = iprot.ReadMessageBegin();
          ProcessFunction fn;
          processMap_.TryGetValue(msg.Name, out fn);
          if (fn == null) {
            TProtocolUtil.Skip(iprot, TType.Struct);
            iprot.ReadMessageEnd();
            TApplicationException x = new TApplicationException (TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + msg.Name + "'");
            oprot.WriteMessageBegin(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID));
            x.Write(oprot);
            oprot.WriteMessageEnd();
            oprot.Transport.Flush();
            return true;
          }
          fn(msg.SeqID, iprot, oprot);
        }
        catch (IOException)
        {
          return false;
        }
        return true;
      }

      public void Search_Process(int seqid, TProtocol iprot, TProtocol oprot)
      {
        Search_args args = new Search_args();
        args.Read(iprot);
        iprot.ReadMessageEnd();
        Search_result result = new Search_result();
        try
        {
          result.Success = iface_.Search(args.Name);
          oprot.WriteMessageBegin(new TMessage("Search", TMessageType.Reply, seqid)); 
          result.Write(oprot);
        }
        catch (TTransportException)
        {
          throw;
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine("Error occurred in processor:");
          Console.Error.WriteLine(ex.ToString());
          TApplicationException x = new TApplicationException        (TApplicationException.ExceptionType.InternalError," Internal error.");
          oprot.WriteMessageBegin(new TMessage("Search", TMessageType.Exception, seqid));
          x.Write(oprot);
        }
        oprot.WriteMessageEnd();
        oprot.Transport.Flush();
      }

    }


    #if !SILVERLIGHT
    [Serializable]
    #endif
    public partial class Search_args : TBase
    {
      private string _name;

      public string Name
      {
        get
        {
          return _name;
        }
        set
        {
          __isset.name = true;
          this._name = value;
        }
      }


      public Isset __isset;
      #if !SILVERLIGHT
      [Serializable]
      #endif
      public struct Isset {
        public bool name;
      }

      public Search_args() {
      }

      public void Read (TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          TField field;
          iprot.ReadStructBegin();
          while (true)
          {
            field = iprot.ReadFieldBegin();
            if (field.Type == TType.Stop) { 
              break;
            }
            switch (field.ID)
            {
              case 5:
                if (field.Type == TType.String) {
                  Name = iprot.ReadString();
                } else { 
                  TProtocolUtil.Skip(iprot, field.Type);
                }
                break;
              default: 
                TProtocolUtil.Skip(iprot, field.Type);
                break;
            }
            iprot.ReadFieldEnd();
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot) {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct("Search_args");
          oprot.WriteStructBegin(struc);
          TField field = new TField();
          if (Name != null && __isset.name) {
            field.Name = "name";
            field.Type = TType.String;
            field.ID = 5;
            oprot.WriteFieldBegin(field);
            oprot.WriteString(Name);
            oprot.WriteFieldEnd();
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString() {
        StringBuilder __sb = new StringBuilder("Search_args(");
        bool __first = true;
        if (Name != null && __isset.name) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("Name: ");
          __sb.Append(Name);
        }
        __sb.Append(")");
        return __sb.ToString();
      }

    }


    #if !SILVERLIGHT
    [Serializable]
    #endif
    public partial class Search_result : TBase
    {
      private Dictionary<int, List<MSSurfing.TClient.Domain.Users.TUser>> _success;

      public Dictionary<int, List<MSSurfing.TClient.Domain.Users.TUser>> Success
      {
        get
        {
          return _success;
        }
        set
        {
          __isset.success = true;
          this._success = value;
        }
      }


      public Isset __isset;
      #if !SILVERLIGHT
      [Serializable]
      #endif
      public struct Isset {
        public bool success;
      }

      public Search_result() {
      }

      public void Read (TProtocol iprot)
      {
        iprot.IncrementRecursionDepth();
        try
        {
          TField field;
          iprot.ReadStructBegin();
          while (true)
          {
            field = iprot.ReadFieldBegin();
            if (field.Type == TType.Stop) { 
              break;
            }
            switch (field.ID)
            {
              case 0:
                if (field.Type == TType.Map) {
                  {
                    Success = new Dictionary<int, List<MSSurfing.TClient.Domain.Users.TUser>>();
                    TMap _map0 = iprot.ReadMapBegin();
                    for( int _i1 = 0; _i1 < _map0.Count; ++_i1)
                    {
                      int _key2;
                      List<MSSurfing.TClient.Domain.Users.TUser> _val3;
                      _key2 = iprot.ReadI32();
                      {
                        _val3 = new List<MSSurfing.TClient.Domain.Users.TUser>();
                        TList _list4 = iprot.ReadListBegin();
                        for( int _i5 = 0; _i5 < _list4.Count; ++_i5)
                        {
                          MSSurfing.TClient.Domain.Users.TUser _elem6;
                          _elem6 = new MSSurfing.TClient.Domain.Users.TUser();
                          _elem6.Read(iprot);
                          _val3.Add(_elem6);
                        }
                        iprot.ReadListEnd();
                      }
                      Success[_key2] = _val3;
                    }
                    iprot.ReadMapEnd();
                  }
                } else { 
                  TProtocolUtil.Skip(iprot, field.Type);
                }
                break;
              default: 
                TProtocolUtil.Skip(iprot, field.Type);
                break;
            }
            iprot.ReadFieldEnd();
          }
          iprot.ReadStructEnd();
        }
        finally
        {
          iprot.DecrementRecursionDepth();
        }
      }

      public void Write(TProtocol oprot) {
        oprot.IncrementRecursionDepth();
        try
        {
          TStruct struc = new TStruct("Search_result");
          oprot.WriteStructBegin(struc);
          TField field = new TField();

          if (this.__isset.success) {
            if (Success != null) {
              field.Name = "Success";
              field.Type = TType.Map;
              field.ID = 0;
              oprot.WriteFieldBegin(field);
              {
                oprot.WriteMapBegin(new TMap(TType.I32, TType.List, Success.Count));
                foreach (int _iter7 in Success.Keys)
                {
                  oprot.WriteI32(_iter7);
                  {
                    oprot.WriteListBegin(new TList(TType.Struct, Success[_iter7].Count));
                    foreach (MSSurfing.TClient.Domain.Users.TUser _iter8 in Success[_iter7])
                    {
                      _iter8.Write(oprot);
                    }
                    oprot.WriteListEnd();
                  }
                }
                oprot.WriteMapEnd();
              }
              oprot.WriteFieldEnd();
            }
          }
          oprot.WriteFieldStop();
          oprot.WriteStructEnd();
        }
        finally
        {
          oprot.DecrementRecursionDepth();
        }
      }

      public override string ToString() {
        StringBuilder __sb = new StringBuilder("Search_result(");
        bool __first = true;
        if (Success != null && __isset.success) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("Success: ");
          __sb.Append(Success);
        }
        __sb.Append(")");
        return __sb.ToString();
      }

    }

  }
}
