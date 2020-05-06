using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.NetCoreServer.Util
{/// <summary>
 /// 分页数据
 /// </summary>
    [Serializable()]
    public class PagerHelper
    {
        private int _pageSize;
        private int _dataCount;

        public PagerHelper()
        {
            _pageSize = 1;
            _dataCount = 0;
        }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value;
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public int DataCount
        {
            get => _dataCount;
            set => _dataCount = value;
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount => (DataCount / PageSize) + (DataCount % PageSize == 0 ? 0 : 1);
    }

    /// <summary>
    /// 非泛型
    /// </summary>
    public class ResponseDataHelper
    {
        public ResponseDataHelper()
        {
            ResponseCode = 0;
            ResponseMessage = "操作成功";
        }

        /// <summary>
        /// 操作响应码
        /// </summary>
        public int ResponseCode { get; set; }

        /// <summary>
        /// 操作响应消息
        /// </summary>
        public string ResponseMessage { get; set; }
    }


    /// <summary>
    /// 泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("ResponseData")]
    public class ResponseDataHelper<T>
    {
        private int _resposeCode;
        private string _responseMessage;

        public ResponseDataHelper()
        {
            _resposeCode = 0;
            _responseMessage = "操作成功";
        }

        /// <summary>
        /// 操作响应码
        /// </summary>
        public int ResponseCode
        {
            get => _resposeCode;
            set => _resposeCode = value;
        }

        /// <summary>
        /// 操作响应消息
        /// </summary>
        public string ResponseMessage
        {
            get => _responseMessage;
            set => _responseMessage = value;
        }

        /// <summary>
        /// 分页相关数据
        /// </summary>
        public PagerHelper PagerData
        {
            get;
            set;
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        public T ResponseData
        {
            get;
            set;
        }
    }
}
